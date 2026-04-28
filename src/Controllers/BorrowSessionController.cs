using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    /// <summary>
    /// Manages borrow sessions — the bridge between the web UI and the ESP32 borrow station.
    ///
    /// Web flow:
    ///   1. POST   /api/v1/borrow-sessions              → create session (triggers ESP32)
    ///   2. GET    /api/v1/borrow-sessions/{id}         → poll for status
    ///   3. DELETE /api/v1/borrow-sessions/{id}         → cancel session
    ///
    /// ESP32 flow:
    ///   1. GET    /api/v1/borrow-sessions/pending      → fetch oldest pending session (AllowAnonymous)
    ///   2. Scan student card + item tag
    ///   3. POST   /api/v1/borrow-sessions/{id}/complete → submit result (AllowAnonymous)
    /// </summary>
    [ApiController]
    [Route("api/v1/borrow-sessions")]
    public class BorrowSessionController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BorrowSessionController(AppDbContext db)
        {
            _db = db;
        }

        // ── Web endpoints ─────────────────────────────────────────────────────────

        /// <summary>Web: Create a new borrow session. Cancels any existing Pending session first.</summary>
        [HttpPost]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<BorrowSessionDto>>> CreateSession()
        {
            // Cancel any existing pending sessions — only one active at a time
            var existing = await _db.BorrowSessions
                .Where(s => s.Status == "Pending")
                .ToListAsync();
            foreach (var old in existing)
                old.Status = "Cancelled";

            var session = new BorrowSession
            {
                Status    = "Pending",
                ExpiresAt = DateTime.UtcNow.AddMinutes(5)
            };

            _db.BorrowSessions.Add(session);
            await _db.SaveChangesAsync();

            return Ok(ApiResponse<BorrowSessionDto>.SuccessResponse(
                MapToDto(session),
                "Borrow session created. Ask the student to scan their ID card and item tag."));
        }

        /// <summary>Web: Poll the status of a borrow session.</summary>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<BorrowSessionDto>>> GetSession(Guid id)
        {
            var session = await _db.BorrowSessions.FindAsync(id);
            if (session == null)
                return NotFound(ApiResponse<BorrowSessionDto>.FailResponse("Session not found."));

            // Auto-expire stale sessions
            if (session.Status == "Pending" && DateTime.UtcNow > session.ExpiresAt)
            {
                session.Status       = "Failed";
                session.ErrorMessage = "Session expired. No scan was completed in time.";
                await _db.SaveChangesAsync();
            }

            return Ok(ApiResponse<BorrowSessionDto>.SuccessResponse(MapToDto(session), "Session retrieved."));
        }

        /// <summary>Web: Cancel a pending session.</summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> CancelSession(Guid id)
        {
            var session = await _db.BorrowSessions.FindAsync(id);
            if (session == null)
                return NotFound(ApiResponse<object>.FailResponse("Session not found."));

            if (session.Status != "Pending")
                return BadRequest(ApiResponse<object>.FailResponse(
                    $"Cannot cancel a session with status '{session.Status}'."));

            session.Status = "Cancelled";
            await _db.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(null, "Session cancelled."));
        }

        // ── ESP32 endpoints ───────────────────────────────────────────────────────

        /// <summary>
        /// ESP32: Fetch the oldest pending borrow session.
        /// Returns 204 No Content when nothing is pending.
        /// </summary>
        [HttpGet("pending")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<BorrowSessionDto>>> GetPendingSession()
        {
            var session = await _db.BorrowSessions
                .Where(s => s.Status == "Pending" && s.ExpiresAt > DateTime.UtcNow)
                .OrderBy(s => s.CreatedAt)
                .FirstOrDefaultAsync();

            if (session == null)
                return NoContent(); // 204 — ESP32 keeps polling

            return Ok(ApiResponse<BorrowSessionDto>.SuccessResponse(
                MapToDto(session), "Pending borrow session found."));
        }

        /// <summary>
        /// ESP32: Complete a borrow session after scanning student + item.
        /// The ESP32 has already created the LentItem — this records the result and closes the session.
        /// </summary>
        [HttpPost("{id}/complete")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<object>>> CompleteSession(
            Guid id, [FromBody] CompleteBorrowSessionDto dto)
        {
            var session = await _db.BorrowSessions.FindAsync(id);
            if (session == null)
                return NotFound(ApiResponse<object>.FailResponse("Session not found."));

            if (session.Status != "Pending")
                return BadRequest(ApiResponse<object>.FailResponse(
                    $"Session is already '{session.Status}'."));

            if (DateTime.UtcNow > session.ExpiresAt)
            {
                session.Status       = "Failed";
                session.ErrorMessage = "Session expired.";
                await _db.SaveChangesAsync();
                return BadRequest(ApiResponse<object>.FailResponse("Session expired."));
            }

            session.Status       = dto.Success ? "Completed" : "Failed";
            session.StudentName  = dto.StudentName;
            session.ItemName     = dto.ItemName;
            session.LentItemId   = dto.LentItemId;
            session.ErrorMessage = dto.Success ? null : dto.ErrorMessage;
            await _db.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(null,
                dto.Success ? "Borrow session completed successfully." : "Borrow session failed."));
        }

        // ── Helper ────────────────────────────────────────────────────────────────

        private static BorrowSessionDto MapToDto(BorrowSession s) => new()
        {
            Id           = s.Id,
            Status       = s.Status,
            StudentName  = s.StudentName,
            ItemName     = s.ItemName,
            LentItemId   = s.LentItemId,
            ErrorMessage = s.ErrorMessage,
            CreatedAt    = s.CreatedAt,
            ExpiresAt    = s.ExpiresAt
        };
    }

    // ── DTOs ──────────────────────────────────────────────────────────────────────

    public class BorrowSessionDto
    {
        public Guid     Id           { get; set; }
        public string   Status       { get; set; } = string.Empty;
        public string?  StudentName  { get; set; }
        public string?  ItemName     { get; set; }
        public Guid?    LentItemId   { get; set; }
        public string?  ErrorMessage { get; set; }
        public DateTime CreatedAt    { get; set; }
        public DateTime ExpiresAt    { get; set; }
    }

    public class CompleteBorrowSessionDto
    {
        public bool    Success      { get; set; }
        public string? StudentName  { get; set; }
        public string? ItemName     { get; set; }
        public Guid?   LentItemId   { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
