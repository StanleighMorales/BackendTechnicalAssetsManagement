using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.Item
{
    public class UpdateLocationDto
    {
        [Required]
        public string Location { get; set; } = string.Empty;
    }
}
