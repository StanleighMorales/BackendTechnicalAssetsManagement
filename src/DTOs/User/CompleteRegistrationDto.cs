using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    /// <summary>
    /// DTO for completing remaining student registration details
    /// Excludes FirstName, MiddleName, LastName as they are already set during initial registration
    /// </summary>
    public class CompleteStudentRegistrationDto
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
        [MaxLength(254, ErrorMessage = "The email address cannot exceed 254 characters.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string StudentIdNumber { get; set; } = string.Empty;

        [Required]
        public string Course { get; set; } = string.Empty;

        [Required]
        public string Section { get; set; } = string.Empty;

        [Required]
        public string Year { get; set; } = string.Empty;

        [Required]
        public string Street { get; set; } = string.Empty;

        [Required]
        public string CityMunicipality { get; set; } = string.Empty;

        [Required]
        public string Province { get; set; } = string.Empty;

        [Required]
        public string PostalCode { get; set; } = string.Empty;

        public IFormFile? ProfilePicture { get; set; }

        [Required]
        public IFormFile FrontStudentIdPicture { get; set; } = null!;

        [Required]
        public IFormFile BackStudentIdPicture { get; set; } = null!;
    }
}
