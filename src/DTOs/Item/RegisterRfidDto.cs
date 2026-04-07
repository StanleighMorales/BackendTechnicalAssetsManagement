using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.Item
{
    public class RegisterRfidDto
    {
        [Required]
        public string RfidUid { get; set; } = string.Empty;
    }
}
