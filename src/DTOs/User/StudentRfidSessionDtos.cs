using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    /// <summary>DTO returned to the web/mobile UI when polling session status.</summary>
    public class StudentRfidRegistrationSessionDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? ScannedRfidUid { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    /// <summary>Request body for creating a new student RFID registration session.</summary>
    public class CreateStudentRfidSessionDto
    {
        [Required]
        public Guid StudentId { get; set; }
    }

    /// <summary>Request body sent by the ESP32 to complete a session.</summary>
    public class CompleteStudentRfidSessionDto
    {
        [Required]
        public string RfidUid { get; set; } = string.Empty;
    }
}
