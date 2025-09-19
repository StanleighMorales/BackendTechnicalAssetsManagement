using System.ComponentModel.DataAnnotations;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace TechnicalAssetManagementApi.Dtos.Item
{
    public class CreateItemDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string SerialNumber { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }

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