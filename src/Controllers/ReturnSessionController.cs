using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    /// <summary>
    /// Manages return sessions — the bridge between the web UI and the ESP32 return station.
    ///
    /// Web flow:
    ///   1. POST   /api/v1/return-sessions              → create session (triggers ESP32)
    ///   2. GET    /api/v1/return-sessions/{id}         → poll for status
    ///   3. DELETE /api/v1/return-sessions/{id}         → cancel session
    ///
    /// ESP32 flow:
    ///   1. GET    /api/v1/return-sessions/pending      → fetch oldest pending session (AllowAnonymous)
    ///   2. Student taps item NFC tag
    ///   3. POST   /api/v1/return-sessions/{id}/complete → submit result (AllowAnonymous)
    /// </summary>
    [ApiController]
    [Route("api/v1/return-sessions")]
    public class ReturnSessionController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ReturnSessionController(AppDbContext db)
        {
            _db = db;
        }

        // ── Web endpoints ─────────────────────────────────────────────────────────

        /// <summary>Web: Create a new return session. Cancels any existing Pending session first.</summary>
        [HttpPost]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<ReturnSessionDto>>> CreateSession()
        {
            // Cancel any existing pending sessions — only one active at a time
            var existing = await _db.ReturnSessions
                .Where(s => s.Status == "Pending")
                .ToListAsync();
            foreach (var old in existing)
                old.Status = "Cancelled";

            var session = new ReturnSession
            {
                Status    = "Pending",
                ExpiresAt = DateTime.UtcNow.AddMinutes(5)
            };

            _db.ReturnSessions.Add(session);
            await _db.SaveChangesAsync();

            return Ok(ApiResponse<ReturnSessionDto>.SuccessResponse(
                MapToDto(session),
                "Return session created. Ask the student to tap the item tag on the scanner."));
        }

        /// <summary>Web: Poll the status of a return session.</summary>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<ReturnSessionDto>>> GetSession(Guid id)
        {
            var session = await _db.ReturnSessions.FindAsync(id);
            if (session == null)
                return NotFound(ApiResponse<ReturnSessionDto>.FailResponse("Session not found."));

            // Auto-expire stale sessions
            if (session.Status == "Pending" && DateTime.UtcNow > session.ExpiresAt)
            {
                session.Status       = "Failed";
                session.ErrorMessage = "Session expired. No scan was completed in time.";
                await _db.SaveChangesAsync();
            }

            return Ok(ApiResponse<ReturnSessionDto>.SuccessResponse(MapToDto(session), "Session retrieved."));
        }

        /// <summary>Web: Cancel a pending session.</summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> CancelSession(Guid id)
        {
            var session = await _db.ReturnSessions.FindAsync(id);
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
        /// ESP32: Fetch the oldest pending return session.
        /// Returns 204 No Content when nothing is pending.
        /// </summary>
        [HttpGet("pending")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<ReturnSessionDto>>> GetPendingSession()
        {
            var session = await _db.ReturnSessions
                .Where(s => s.Status == "Pending" && s.ExpiresAt > DateTime.UtcNow)
                .OrderBy(s => s.CreatedAt)
                .FirstOrDefaultAsync();

            if (session == null)
                return NoContent(); // 204 — ESP32 keeps polling

            return Ok(ApiResponse<ReturnSessionDto>.SuccessResponse(
                MapToDto(session), "Pending return session found."));
        }

        /// <summary>
        /// ESP32: Complete a return session after scanning the item tag.
        /// The ESP32 has already patched the LentItem to Returned — this records the result.
        /// </summary>
        [HttpPost("{id}/complete")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<object>>> CompleteSession(
            Guid id, [FromBody] CompleteReturnSessionDto dto)
        {
            var session = await _db.ReturnSessions.FindAsync(id);
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
            session.ItemName     = dto.ItemName;
            session.BorrowerName = dto.BorrowerName;
            session.LentItemId   = dto.LentItemId;
            session.ErrorMessage = dto.Success ? null : dto.ErrorMessage;
            await _db.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(null,
                dto.Success ? "Return session completed successfully." : "Return session failed."));
        }

        // ── Helper ────────────────────────────────────────────────────────────────

        private static ReturnSessionDto MapToDto(ReturnSession s) => new()
        {
            Id           = s.Id,
            Status       = s.Status,
            ItemName     = s.ItemName,
            BorrowerName = s.BorrowerName,
            LentItemId   = s.LentItemId,
            ErrorMessage = s.ErrorMessage,
            CreatedAt    = s.CreatedAt,
            ExpiresAt    = s.ExpiresAt
        };
    }

    // ── DTOs ──────────────────────────────────────────────────────────────────────

    public class ReturnSessionDto
    {
        public Guid     Id           { get; set; }
        public string   Status       { get; set; } = string.Empty;
        public string?  ItemName     { get; set; }
        public string?  BorrowerName { get; set; }
        public Guid?    LentItemId   { get; set; }
        public string?  ErrorMessage { get; set; }
        public DateTime CreatedAt    { get; set; }
        public DateTime ExpiresAt    { get; set; }
    }

    public class CompleteReturnSessionDto
    {
        public bool    Success      { get; set; }
        public string? ItemName     { get; set; }
        public string? BorrowerName { get; set; }
        public Guid?   LentItemId   { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
