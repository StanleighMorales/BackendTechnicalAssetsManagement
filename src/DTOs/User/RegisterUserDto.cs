using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.Models.DTOs.Users
{
    public class RegisterUserDto
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address. user")]
        [MaxLength(254, ErrorMessage = "The email address cannot exceed 254 characters.")]
        public string Email { get; set; } = string.Empty;

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

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? PhoneNumber { get; set; }

        public string? Position { get; set; }
    }
    public class RegisterTeacherDto : RegisterUserDto
    {
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? PhoneNumber { get; set; }

        [Required]
        public string Department { get; set; } = string.Empty;
    }
    public class RegisterStudentDto : RegisterUserDto
    {
        public IFormFile? FrontStudentIdPicture { get; set; }
        public IFormFile? BackStudentIdPicture { get; set; }

        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string StudentIdNumber { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        [Required]
        public string Section { get; set; } = string.Empty;
        [Required]
        public string Year { get; set; } = string.Empty;

        public IFormFile? ProfilePicture { get; set; }
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public string CityMunicipality { get; set; } = string.Empty;
        [Required]
        public string Province { get; set; } = string.Empty;
        [Required]
        public string PostalCode { get; set; } = string.Empty;
    }
    public class RegisterManagerDto : RegisterUserDto
    {
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string? PhoneNumber { get; set; }
    }
    public class RegisterAdminDto : RegisterUserDto
    {
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string? PhoneNumber { get; set; }
    }
}