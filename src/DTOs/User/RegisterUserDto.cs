using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.Models.DTOs.Users
{
    public class RegisterUserDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class RegisterStaffDto : RegisterUserDto
    {
        [Required]
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address. user")]
        [MaxLength(254, ErrorMessage = "The email address cannot exceed 254 characters.")]
        public string Email { get; set; } = string.Empty;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? PhoneNumber { get; set; }

        public string? Position { get; set; }
    }
}