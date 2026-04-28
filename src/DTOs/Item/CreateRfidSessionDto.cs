using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.Item
{
    public class CreateRfidSessionDto
    {
        [Required]
        public Guid ItemId { get; set; }
    }
}
