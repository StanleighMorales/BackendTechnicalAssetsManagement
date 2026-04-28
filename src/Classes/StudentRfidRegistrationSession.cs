namespace BackendTechnicalAssetsManagement.src.Classes
{
    /// <summary>
    /// Represents a pending student RFID registration request initiated from the web/mobile UI.
    /// The ESP32 polls for these sessions, scans the student's physical RFID card, and completes them.
    /// </summary>
    public class StudentRfidRegistrationSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>The student who needs an RFID card assigned.</summary>
        public Guid StudentId { get; set; }
        public Student? Student { get; set; }

        /// <summary>
        /// Session status:
        ///   Pending   – waiting for ESP32 to scan a card
        ///   Completed – ESP32 scanned and registered successfully
        ///   Failed    – registration failed (duplicate RFID, student not found, etc.)
        ///   Cancelled – web user cancelled before ESP32 scanned
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>The RFID UID that was scanned (populated by ESP32 on completion).</summary>
        public string? ScannedRfidUid { get; set; }

        /// <summary>Error message if Status == Failed.</summary>
        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>Sessions older than this are considered stale and can be ignored.</summary>
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(5);
    }
}
