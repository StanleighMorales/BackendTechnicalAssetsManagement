using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    /// <summary>
    /// Manages item scan sessions — used for guest borrow/reserve flows.
    ///
    /// The ESP32 scans the item tag and returns itemId + itemName to the web.
    /// The web then opens the guest form pre-filled with the scanned item.
    /// Staff fills in guest details and submits the borrow/reserve.
    ///
    /// Web flow:
    ///   1. POST   /api/v1/item-scan-sessions              → create session (triggers ESP32)
    ///   2. GET    /api/v1/item-scan-sessions/{id}         → poll for status + itemId
    ///   3. DELETE /api/v1/item-scan-sessions/{id}         → cancel session
    ///
    /// ESP32 flow:
    ///   1. GET    /api/v1/item-scan-sessions/pending      → fetch oldest pending session
    ///   2. Scan item NFC tag
    ///   3. POST   /api/v1/item-scan-sessions/{id}/complete → submit itemId + itemName
    /// </summary>
    [ApiController]
    [Route("api/v1/item-scan-sessions")]
    public class ItemScanSessionController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ItemScanSessionController(AppDbContext db)
        {
            _db = db;
        }

        // ── Web endpoints ─────────────────────────────────────────────────────────

        /// <summary>Web: Create a new item scan session. Cancels any existing Pending session first.</summary>
        [HttpPost]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<ItemScanSessionDto>>> CreateSession()
        {
            // Cancel any existing pending sessions — only one active at a time
            var existing = await _db.ItemScanSessions
                .Where(s => s.Status == "Pending")
                .ToListAsync();
            foreach (var old in existing)
                old.Status = "Cancelled";

            var session = new ItemScanSession
            {
                Status    = "Pending",
                ExpiresAt = DateTime.UtcNow.AddMinutes(5)
            };

            _db.ItemScanSessions.Add(session);
            await _db.SaveChangesAsync();

            return Ok(ApiResponse<ItemScanSessionDto>.SuccessResponse(
                MapToDto(session),
                "Item scan session created. Ask the student to tap the item tag on the scanner."));
        }

        /// <summary>Web: Poll the status of an item scan session.</summary>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<ItemScanSessionDto>>> GetSession(Guid id)
        {
            var session = await _db.ItemScanSessions.FindAsync(id);
            if (session == null)
                return NotFound(ApiResponse<ItemScanSessionDto>.FailResponse("Session not found."));

            // Auto-expire stale sessions
            if (session.Status == "Pending" && DateTime.UtcNow > session.ExpiresAt)
            {
                session.Status       = "Failed";
                session.ErrorMessage = "Session expired. No item was scanned in time.";
                await _db.SaveChangesAsync();
            }

            return Ok(ApiResponse<ItemScanSessionDto>.SuccessResponse(MapToDto(session), "Session retrieved."));
        }

        /// <summary>Web: Cancel a pending session.</summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> CancelSession(Guid id)
        {
            var session = await _db.ItemScanSessions.FindAsync(id);
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
        /// ESP32: Fetch the oldest pending item scan session.
        /// Returns 204 No Content when nothing is pending.
        /// </summary>
        [HttpGet("pending")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<ItemScanSessionDto>>> GetPendingSession()
        {
            var session = await _db.ItemScanSessions
                .Where(s => s.Status == "Pending" && s.ExpiresAt > DateTime.UtcNow)
                .OrderBy(s => s.CreatedAt)
                .FirstOrDefaultAsync();

            if (session == null)
                return NoContent(); // 204 — ESP32 keeps polling

            return Ok(ApiResponse<ItemScanSessionDto>.SuccessResponse(
                MapToDto(session), "Pending item scan session found."));
        }

        /// <summary>
        /// ESP32: Complete an item scan session by submitting the scanned item details.
        /// The web will use itemId to pre-fill the guest borrow/reserve form.
        /// </summary>
        [HttpPost("{id}/complete")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<object>>> CompleteSession(
            Guid id, [FromBody] CompleteItemScanSessionDto dto)
        {
            var session = await _db.ItemScanSessions.FindAsync(id);
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
            session.ItemId       = dto.ItemId;
            session.ItemName     = dto.ItemName;
            session.RfidUid      = dto.RfidUid;
            session.ErrorMessage = dto.Success ? null : dto.ErrorMessage;
            await _db.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(null,
                dto.Success ? "Item scan completed successfully." : "Item scan failed."));
        }

        // ── Helper ────────────────────────────────────────────────────────────────

        private static ItemScanSessionDto MapToDto(ItemScanSession s) => new()
        {
            Id           = s.Id,
            Status       = s.Status,
            ItemId       = s.ItemId,
            ItemName     = s.ItemName,
            RfidUid      = s.RfidUid,
            ErrorMessage = s.ErrorMessage,
            CreatedAt    = s.CreatedAt,
            ExpiresAt    = s.ExpiresAt
        };
    }

    // ── DTOs ──────────────────────────────────────────────────────────────────────

    public class ItemScanSessionDto
    {
        public Guid     Id           { get; set; }
        public string   Status       { get; set; } = string.Empty;
        public Guid?    ItemId       { get; set; }
        public string?  ItemName     { get; set; }
        public string?  RfidUid      { get; set; }   // pass this as TagUid to POST /api/v1/lentItems/guests
        public string?  ErrorMessage { get; set; }
        public DateTime CreatedAt    { get; set; }
        public DateTime ExpiresAt    { get; set; }
    }

    public class CompleteItemScanSessionDto
    {
        public bool    Success      { get; set; }
        public Guid?   ItemId       { get; set; }
        public string? ItemName     { get; set; }
        public string? RfidUid      { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
