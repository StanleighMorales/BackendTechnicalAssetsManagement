namespace BackendTechnicalAssetsManagement.src.Classes
{
    /// <summary>
    /// Represents a web-triggered borrow session.
    /// Web creates it, ESP32 polls for it, scans student + item, then completes it.
    /// </summary>
    public class BorrowSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Session status:
        ///   Pending   – waiting for ESP32 to scan student card + item tag
        ///   Completed – ESP32 submitted the borrow successfully
        ///   Failed    – borrow failed (item unavailable, student not found, etc.)
        ///   Cancelled – web user cancelled before ESP32 scanned
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>Populated by ESP32 on completion.</summary>
        public string? StudentName  { get; set; }
        public string? ItemName     { get; set; }
        public Guid?   LentItemId   { get; set; }
        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(5);
    }
}
