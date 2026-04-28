using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    /// <summary>
    /// Manages RFID registration sessions — the bridge between the web UI and the ESP32 device.
    ///
    /// Web flow:
    ///   1. POST   /api/v1/rfid-sessions          → create session for an item
    ///   2. GET    /api/v1/rfid-sessions/{id}      → poll for status (Pending → Completed/Failed)
    ///   3. DELETE /api/v1/rfid-sessions/{id}      → cancel if user closes the dialog
    ///
    /// ESP32 flow:
    ///   1. GET    /api/v1/rfid-sessions/pending   → fetch the oldest pending session
    ///   2. Scan the physical RFID tag
    ///   3. POST   /api/v1/rfid-sessions/{id}/complete  → submit scanned UID (triggers register-rfid)
    /// </summary>
    [ApiController]
    [Route("api/v1/rfid-sessions")]
    public class RfidRegistrationSessionController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IItemService _itemService;

        public RfidRegistrationSessionController(AppDbContext db, IItemService itemService)
        {
            _db = db;
            _itemService = itemService;
        }

        // ── Web endpoints ─────────────────────────────────────────────────────────

        /// <summary>
        /// Web: Create a new RFID registration session for the given item.
        /// Any existing Pending session for the same item is cancelled first.
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<RfidRegistrationSessionDto>>> CreateSession(
            [FromBody] CreateRfidSessionDto dto)
        {
            // Verify item exists
            var item = await _db.Items.FindAsync(dto.ItemId);
            if (item == null)
                return NotFound(ApiResponse<RfidRegistrationSessionDto>.FailResponse("Item not found."));

            // Cancel any existing pending session for this item
            var existing = await _db.RfidRegistrationSessions
                .Where(s => s.ItemId == dto.ItemId && s.Status == "Pending")
                .ToListAsync();
            foreach (var old in existing)
                old.Status = "Cancelled";

            var session = new RfidRegistrationSession
            {
                ItemId = dto.ItemId,
                Status = "Pending",
                ExpiresAt = DateTime.UtcNow.AddMinutes(5)
            };

            _db.RfidRegistrationSessions.Add(session);
            await _db.SaveChangesAsync();

            return Ok(ApiResponse<RfidRegistrationSessionDto>.SuccessResponse(
                MapToDto(session),
                "RFID registration session created. Place the RFID tag on the scanner."));
        }

        /// <summary>
        /// Web: Poll the status of a session.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<RfidRegistrationSessionDto>>> GetSession(Guid id)
        {
            var session = await _db.RfidRegistrationSessions.FindAsync(id);
            if (session == null)
                return NotFound(ApiResponse<RfidRegistrationSessionDto>.FailResponse("Session not found."));

            // Auto-expire stale sessions
            if (session.Status == "Pending" && DateTime.UtcNow > session.ExpiresAt)
            {
                session.Status = "Failed";
                session.ErrorMessage = "Session expired. No RFID tag was scanned in time.";
                await _db.SaveChangesAsync();
            }

            return Ok(ApiResponse<RfidRegistrationSessionDto>.SuccessResponse(
                MapToDto(session), "Session retrieved."));
        }

        /// <summary>
        /// Web: Cancel a pending session (user closed the dialog).
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> CancelSession(Guid id)
        {
            var session = await _db.RfidRegistrationSessions.FindAsync(id);
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
        /// ESP32: Fetch the oldest active pending session.
        /// Returns 204 No Content when there is nothing to process.
        /// </summary>
        [HttpGet("pending")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<RfidRegistrationSessionDto>>> GetPendingSession()
        {
            var session = await _db.RfidRegistrationSessions
                .Where(s => s.Status == "Pending" && s.ExpiresAt > DateTime.UtcNow)
                .OrderBy(s => s.CreatedAt)
                .FirstOrDefaultAsync();

            if (session == null)
                return NoContent(); // 204 — ESP32 keeps polling

            return Ok(ApiResponse<RfidRegistrationSessionDto>.SuccessResponse(
                MapToDto(session), "Pending session found."));
        }

        /// <summary>
        /// ESP32: Complete a session by submitting the scanned RFID UID.
        /// This calls the existing register-rfid logic and marks the session accordingly.
        /// </summary>
        [HttpPost("{id}/complete")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<object>>> CompleteSession(
            Guid id, [FromBody] RegisterRfidDto dto)
        {
            var session = await _db.RfidRegistrationSessions.FindAsync(id);
            if (session == null)
                return NotFound(ApiResponse<object>.FailResponse("Session not found."));

            if (session.Status != "Pending")
                return BadRequest(ApiResponse<object>.FailResponse(
                    $"Session is already '{session.Status}'."));

            if (DateTime.UtcNow > session.ExpiresAt)
            {
                session.Status = "Failed";
                session.ErrorMessage = "Session expired.";
                await _db.SaveChangesAsync();
                return BadRequest(ApiResponse<object>.FailResponse("Session expired."));
            }

            // Reuse the existing register-rfid service logic
            var (success, errorMessage) = await _itemService.RegisterRfidToItemAsync(session.ItemId, dto.RfidUid);

            session.ScannedRfidUid = dto.RfidUid;
            session.Status = success ? "Completed" : "Failed";
            session.ErrorMessage = success ? null : errorMessage;
            await _db.SaveChangesAsync();

            if (!success)
                return Conflict(ApiResponse<object>.FailResponse(errorMessage));

            return Ok(ApiResponse<object>.SuccessResponse(null,
                $"RFID '{dto.RfidUid}' registered to item successfully."));
        }

        // ── Helper ────────────────────────────────────────────────────────────────

        private static RfidRegistrationSessionDto MapToDto(RfidRegistrationSession s) => new()
        {
            Id = s.Id,
            ItemId = s.ItemId,
            Status = s.Status,
            ScannedRfidUid = s.ScannedRfidUid,
            ErrorMessage = s.ErrorMessage,
            CreatedAt = s.CreatedAt,
            ExpiresAt = s.ExpiresAt
        };
    }
}
