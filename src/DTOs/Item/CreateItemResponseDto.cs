namespace BackendTechnicalAssetsManagement.src.DTOs.Item
{
    /// <summary>
    /// Returned when creating an item.
    /// If the item was created without an RFID, a pending registration session
    /// is automatically created so the ESP32 can scan the tag immediately.
    /// </summary>
    public class CreateItemResponseDto
    {
        public ItemDto Item { get; set; } = null!;

        /// <summary>
        /// Null if the item was created with an RfidUid already set.
        /// Non-null means a session is waiting for the ESP32 to scan a tag.
        /// </summary>
        public RfidRegistrationSessionDto? RfidSession { get; set; }
    }
}
