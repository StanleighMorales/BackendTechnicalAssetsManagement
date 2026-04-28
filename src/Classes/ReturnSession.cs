namespace BackendTechnicalAssetsManagement.src.Classes
{
    /// <summary>
    /// Represents a web-triggered return session.
    /// Web creates it, ESP32 polls for it, student taps the item NFC tag, return is processed.
    /// </summary>
    public class ReturnSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Session status:
        ///   Pending   – waiting for ESP32 to scan the item tag
        ///   Completed – item marked as Returned successfully
        ///   Failed    – return failed (item not borrowed, tag not registered, etc.)
        ///   Cancelled – web user cancelled before ESP32 scanned
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>Populated by ESP32 on completion.</summary>
        public string? ItemName     { get; set; }
        public string? BorrowerName { get; set; }
        public Guid?   LentItemId   { get; set; }
        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(5);
    }
}
