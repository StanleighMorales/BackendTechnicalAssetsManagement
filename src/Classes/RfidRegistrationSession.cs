namespace BackendTechnicalAssetsManagement.src.Classes
{
    /// <summary>
    /// Represents a pending RFID registration request initiated from the web UI.
    /// The ESP32 polls for these sessions and scans the physical tag to complete them.
    /// </summary>
    public class RfidRegistrationSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>The item that needs an RFID tag assigned.</summary>
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }

        /// <summary>
        /// Session status:
        ///   Pending   – waiting for ESP32 to scan a tag
        ///   Completed – ESP32 scanned and registered successfully
        ///   Failed    – registration failed (duplicate RFID, item not found, etc.)
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
