using System.ComponentModel.DataAnnotations;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.Archive.Items
{
    public class UpdateArchiveItemsDto
    {
        public string? SerialNumber { get; set; }
        public byte[]? Image { get; set; }
        public string? ItemName { get; set; }
        public string? ItemType { get; set; }
        public string? ItemModel { get; set; }
        public string? ItemMake { get; set; }
        public string? Description { get; set; }

        public ItemCategory? Category { get; set; }
        public ItemCondition? Condition { get; set; }
    }
}
