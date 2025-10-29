using Microsoft.EntityFrameworkCore;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Classes
{
    [Index(nameof(SerialNumber), IsUnique = true)]
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? SerialNumber { get; set; }
        public byte[]? Image { get; set; }
        public string? ImageMimeType { get; set; }
        public string? Barcode { get; set; }

        public byte[]? BarcodeImage { get; set; }
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
