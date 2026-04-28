using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    /// <summary>
    /// Manages student RFID registration sessions — the bridge between the web/mobile UI
    /// and the ESP32 device for assigning physical RFID cards to students.
    ///
    /// Web/Mobile flow:
    ///   1. POST   /api/v1/rfid-sessions/student              → create session for a student
    ///   2. GET    /api/v1/rfid-sessions/student/{id}         → poll for status (Pending → Completed/Failed)
    ///   3. DELETE /api/v1/rfid-sessions/student/{id}         → cancel if user closes the dialog
    ///
    /// ESP32 flow:
    ///   1. GET    /api/v1/rfid-sessions/pending/student      → fetch the oldest pending session
    ///   2. Scan the student's physical RFID card
    ///   3. POST   /api/v1/rfid-sessions/{id}/complete/student → submit scanned UID
    ///      Backend assigns UID directly to student
    /// </summary>
    [ApiController]
    [Route("api/v1/rfid-sessions")]
    public class StudentRfidSessionController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IUserService _userService;

        public StudentRfidSessionController(AppDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        // ── Web / Mobile endpoints ────────────────────────────────────────────────

        /// <summary>
        /// Web/Mobile: Create a new RFID registration session for the given student.
        /// Any existing Pending session for the same student is cancelled first.
        /// </summary>
        [HttpPost("student")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<StudentRfidRegistrationSessionDto>>> CreateStudentSession(
            [FromBody] CreateStudentRfidSessionDto dto)
        {
            // Verify student exists
            var student = await _db.Students.FindAsync(dto.StudentId);
            if (student == null)
                return NotFound(ApiResponse<StudentRfidRegistrationSessionDto>.FailResponse("Student not found."));

            // Prevent creating a session if student already has an RFID
            if (!string.IsNullOrEmpty(student.RfidUid))
                return Conflict(ApiResponse<StudentRfidRegistrationSessionDto>.FailResponse(
                    $"Student already has an RFID card registered. Remove it first."));

            // Cancel any existing pending session for this student
            var existing = await _db.StudentRfidRegistrationSessions
                .Where(s => s.StudentId == dto.StudentId && s.Status == "Pending")
                .ToListAsync();
            foreach (var old in existing)
                old.Status = "Cancelled";

            var session = new StudentRfidRegistrationSession
            {
                StudentId = dto.StudentId,
                Status = "Pending",
                ExpiresAt = DateTime.UtcNow.AddMinutes(5)
            };

            _db.StudentRfidRegistrationSessions.Add(session);
            await _db.SaveChangesAsync();

            return Ok(ApiResponse<StudentRfidRegistrationSessionDto>.SuccessResponse(
                MapToDto(session, student),
                "Student RFID registration session created. Place the student's RFID card on the scanner."));
        }

        /// <summary>
        /// Web/Mobile: Poll the status of a student RFID session.
        /// </summary>
        [HttpGet("student/{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<StudentRfidRegistrationSessionDto>>> GetStudentSession(Guid id)
        {
            var session = await _db.StudentRfidRegistrationSessions
                .Include(s => s.Student)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null)
                return NotFound(ApiResponse<StudentRfidRegistrationSessionDto>.FailResponse("Session not found."));

            // Auto-expire stale sessions
            if (session.Status == "Pending" && DateTime.UtcNow > session.ExpiresAt)
            {
                session.Status = "Failed";
                session.ErrorMessage = "Session expired. No RFID card was scanned in time.";
                await _db.SaveChangesAsync();
            }

            return Ok(ApiResponse<StudentRfidRegistrationSessionDto>.SuccessResponse(
                MapToDto(session, session.Student), "Session retrieved."));
        }

        /// <summary>
        /// Web/Mobile: Cancel a pending student RFID session (user closed the dialog).
        /// </summary>
        [HttpDelete("student/{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> CancelStudentSession(Guid id)
        {
            var session = await _db.StudentRfidRegistrationSessions.FindAsync(id);
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
        /// ESP32: Fetch the oldest active pending student RFID session.
        /// Returns 204 No Content when there is nothing to process.
        /// </summary>
        [HttpGet("pending/student")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<StudentRfidRegistrationSessionDto>>> GetPendingStudentSession()
        {
            var session = await _db.StudentRfidRegistrationSessions
                .Include(s => s.Student)
                .Where(s => s.Status == "Pending" && s.ExpiresAt > DateTime.UtcNow)
                .OrderBy(s => s.CreatedAt)
                .FirstOrDefaultAsync();

            if (session == null)
                return NoContent(); // 204 — ESP32 keeps polling

            return Ok(ApiResponse<StudentRfidRegistrationSessionDto>.SuccessResponse(
                MapToDto(session, session.Student), "Pending student session found."));
        }

        /// <summary>
        /// ESP32: Complete a student RFID session by submitting the scanned RFID UID.
        /// Assigns the UID directly to the student.
        /// </summary>
        [HttpPost("{id}/complete/student")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<object>>> CompleteStudentSession(
            Guid id, [FromBody] CompleteStudentRfidSessionDto dto)
        {
            var session = await _db.StudentRfidRegistrationSessions
                .Include(s => s.Student)
                .FirstOrDefaultAsync(s => s.Id == id);

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

            // Assign the scanned UID directly to the student
            var (success, errorMessage) = await _userService.RegisterRfidToStudentAsync(session.StudentId, dto.RfidUid);

            session.ScannedRfidUid = dto.RfidUid;
            session.Status = success ? "Completed" : "Failed";
            session.ErrorMessage = success ? null : errorMessage;
            await _db.SaveChangesAsync();

            if (!success)
                return Conflict(ApiResponse<object>.FailResponse(errorMessage));

            var studentName = session.Student != null
                ? $"{session.Student.FirstName} {session.Student.LastName}"
                : session.StudentId.ToString();

            return Ok(ApiResponse<object>.SuccessResponse(null,
                $"RFID '{dto.RfidUid}' registered to {studentName} successfully."));
        }

        // ── Helper ────────────────────────────────────────────────────────────────

        private static StudentRfidRegistrationSessionDto MapToDto(StudentRfidRegistrationSession s, Student? student) => new()
        {
            Id = s.Id,
            StudentId = s.StudentId,
            StudentName = student != null ? $"{student.FirstName} {student.LastName}" : string.Empty,
            Status = s.Status,
            ScannedRfidUid = s.ScannedRfidUid,
            ErrorMessage = s.ErrorMessage,
            CreatedAt = s.CreatedAt,
            ExpiresAt = s.ExpiresAt
        };
    }
}
