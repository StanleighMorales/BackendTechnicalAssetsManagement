using System.ComponentModel.DataAnnotations;
using static BackendTechnicalAssetsManagement.src.Models.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.Item
{
    public class UpdateItemDto
    {
        [Required]
        public string SerialNumber { get; set; } = string.Empty;
        public IFormFile? Image { get; set; } // Allow updating the image
        [Required]
        public string ItemName { get; set; } = string.Empty;
        [Required]
        public string ItemType { get; set; } = string.Empty;
        public string? ItemModel { get; set; }
        [Required]
        public string ItemMake { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public ItemCategory Category { get; set; }
        [Required]
        public ItemCondition Condition { get; set; }
    }
}
