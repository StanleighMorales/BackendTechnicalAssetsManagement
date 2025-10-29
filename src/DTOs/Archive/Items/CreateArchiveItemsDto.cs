using System.ComponentModel.DataAnnotations;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.Archive.Items
{
    public class CreateArchiveItemsDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string SerialNumber { get; set; } = string.Empty;
        public string? Barcode { get; set; }
        public byte[]? BarcodeImage { get; set; }
        public byte[]? Image { get; set; }
        public string? ImageMimeType { get; set; }
        [Required]
        public string ItemName { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public string? ItemModel { get; set; }
        public string ItemMake { get; set; } = string.Empty;
        public string? Description { get; set; }

        [Required]
        public string? Category { get; set; }
        [Required]
        public string? Condition { get; set; }
        [Required]
        public string? Status { get; set; }
    }
}
