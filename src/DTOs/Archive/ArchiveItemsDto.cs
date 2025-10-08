using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.Archive
{
    public class ArchiveItemsDto
    {
        public Guid Id { get; set; }
        
        public Guid ItemId { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public string? ItemModel { get; set; }
        public string ItemMake { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ItemCategory Category { get; set; }
        public ItemCondition Condition { get; set; }

        public DateTime ArchivedAt { get; set; } = DateTime.Now;
    }
}
