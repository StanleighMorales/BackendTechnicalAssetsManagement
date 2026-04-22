using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    public class RegisterStudentRfidDto
    {
        [Required]
        public string RfidCode { get; set; } = string.Empty;
    }
}
