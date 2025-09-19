using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.Item
{
    public class ItemDto
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public string? ItemModel { get; set; }
        public string ItemMake { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ItemCategory Category { get; set; }
        public ItemCondition Condition { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
