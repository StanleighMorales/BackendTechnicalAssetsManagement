namespace BackendTechnicalAssetsManagement.src.Classes
{
    /// <summary>
    /// Represents a web-triggered item scan session for guest borrow/reserve.
    /// Web creates it, ESP32 polls for it, scans the item NFC tag, and returns
    /// the itemId + itemName back to the web so staff can fill in guest details.
    ///
    /// The ESP32 does NOT create the lent record — it only resolves the item.
    /// The web completes the borrow/reserve after the staff fills in the form.
    /// </summary>
    public class ItemScanSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Session status:
        ///   Pending   – waiting for ESP32 to scan the item tag
        ///   Completed – item tag scanned, itemId returned to web
        ///   Failed    – scan failed (tag not registered, etc.)
        ///   Cancelled – web user cancelled before ESP32 scanned
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>Populated by ESP32 on completion.</summary>
        public Guid?   ItemId      { get; set; }
        public string? ItemName    { get; set; }
        public string? RfidUid     { get; set; }   // raw tag UID — passed to POST /api/v1/lentItems/guests as TagUid
        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(5);
    }
}
