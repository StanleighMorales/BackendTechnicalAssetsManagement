namespace BackendTechnicalAssetsManagement.src.DTOs.Item
{
    public class RfidRegistrationSessionDto
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ScannedRfidUid { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
