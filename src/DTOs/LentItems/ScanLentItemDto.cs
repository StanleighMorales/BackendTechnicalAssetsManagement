using System.ComponentModel.DataAnnotations;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.LentItems
{
    public class ScanLentItemDto
    {
        [Required]
        public LentItemsStatus LentItemsStatus { get; set; }
    }
}
