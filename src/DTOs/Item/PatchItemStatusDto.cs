using System.ComponentModel.DataAnnotations;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.Item
{
    public class PatchItemStatusDto
    {
        [Required]
        [EnumDataType(typeof(ItemStatus), ErrorMessage = "Status must be 'Available' or 'Borrowed'.")]
        public ItemStatus Status { get; set; }
    }
}
