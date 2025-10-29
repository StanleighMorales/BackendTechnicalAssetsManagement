using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    public class ChangePasswordDto
    {
        [Required]
        public Guid? UserId { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        // This regex ensures the password contains the required characters.
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string? NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "The passwords do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
