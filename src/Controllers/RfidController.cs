using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/v1/rfids")]
    public class RfidController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RfidController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Register a new RFID card to the system
        /// Called by ESP32 RFID registration station
        /// Requires admin authentication
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> RegisterRfid([FromBody] RegisterRfidCardDto dto)
        {
            // Check if RFID UID already exists
            var existingRfid = await _context.Rfids
                .FirstOrDefaultAsync(r => r.RfidUid == dto.RfidUid);

            if (existingRfid != null)
            {
                return Conflict(ApiResponse<object>.FailResponse($"RFID UID '{dto.RfidUid}' is already registered."));
            }

            // Check if RFID Code already exists
            var existingCode = await _context.Rfids
                .FirstOrDefaultAsync(r => r.RfidCode == dto.RfidCode);

            if (existingCode != null)
            {
                return Conflict(ApiResponse<object>.FailResponse($"RFID Code '{dto.RfidCode}' is already in use."));
            }

            // Create new RFID card
            var rfid = new Rfid
            {
                RfidUid = dto.RfidUid,
                RfidCode = dto.RfidCode
            };

            _context.Rfids.Add(rfid);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(null, $"RFID card registered successfully. Code: {dto.RfidCode}"));
        }

        /// <summary>
        /// Get all registered RFID cards
        /// </summary>
        [HttpGet]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Rfid>>>> GetAllRfids()
        {
            var rfids = await _context.Rfids.ToListAsync();
            return Ok(ApiResponse<IEnumerable<Rfid>>.SuccessResponse(rfids, "RFID cards retrieved successfully."));
        }

        /// <summary>
        /// Get RFID card by UID
        /// </summary>
        [HttpGet("{rfidUid}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<Rfid>>> GetRfidByUid(string rfidUid)
        {
            var rfid = await _context.Rfids.FindAsync(rfidUid);
            
            if (rfid == null)
            {
                return NotFound(ApiResponse<Rfid>.FailResponse("RFID card not found."));
            }

            return Ok(ApiResponse<Rfid>.SuccessResponse(rfid, "RFID card retrieved successfully."));
        }

        /// <summary>
        /// Delete RFID card
        /// </summary>
        [HttpDelete("{rfidUid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteRfid(string rfidUid)
        {
            var rfid = await _context.Rfids.FindAsync(rfidUid);
            
            if (rfid == null)
            {
                return NotFound(ApiResponse<object>.FailResponse("RFID card not found."));
            }

            // Check if RFID is assigned to any student
            var assignedToStudent = await _context.Students
                .AnyAsync(s => s.RfidUid == rfidUid);

            if (assignedToStudent)
            {
                return BadRequest(ApiResponse<object>.FailResponse("Cannot delete RFID card that is assigned to a student."));
            }

            _context.Rfids.Remove(rfid);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(null, "RFID card deleted successfully."));
        }
    }

    public class RegisterRfidCardDto
    {
        [Required]
        public string RfidUid { get; set; } = string.Empty;

        [Required]
        public string RfidCode { get; set; } = string.Empty;
    }
}
