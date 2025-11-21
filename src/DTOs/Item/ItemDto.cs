using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.Item
{
    public class ItemDto
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string? Barcode { get; set; }
        public string? BarcodeImage { get; set; }
        public string? Image { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public string? ItemModel { get; set; }
        public string ItemMake { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ItemCategory Category { get; set; }
        public ItemCondition Condition { get; set; }
        public ItemStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

    /// <summary>
    /// Response DTO for bulk item import containing import results
    /// </summary>
    public class ImportItemsResponseDto
    {
        public int TotalProcessed { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public List<string> Errors { get; set; } = new();
        public List<string> SkippedDuplicates { get; set; } = new();
    }
