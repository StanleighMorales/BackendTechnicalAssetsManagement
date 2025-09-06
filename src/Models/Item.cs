using static BackendTechnicalAssetsManagement.Model.Enums;

namespace BackendTechnicalAssetsManagement.Model
{
    public class Item
    {
        public int Id { get; set; } // Primary Key
        public string SerialNumber { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public string? ItemModel { get; set; }
        public string ItemMake { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ItemCategory Category { get; set; }
        public ItemCondition Condition { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;



    }
}
