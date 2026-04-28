using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    public class RegisterStudentRfidDto
    {
        [Required]
        public string RfidUid { get; set; } = string.Empty;
    }
}
