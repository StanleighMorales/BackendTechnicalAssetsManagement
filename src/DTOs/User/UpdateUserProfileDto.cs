using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    public class UpdateUserProfileDto
    {
        public string? PhoneNumber { get; set; }

        // Student-Specific
        public string? Course { get; set; }
        public string? Year { get; set; }
        public string? Section { get; set; }
        public string? Street { get; set; }
        public string? FrontStudentIdPicture { get; set; }
        public string? BackStudentIdPicture { get; set; }

        [Required]
        public string? LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string? FirstName { get; set; } = string.Empty;
        [Required]
        public string? StudentIdNumber { get; set; } = string.Empty;
        [Required]

        public string? ProfilePicture { get; set; }


        // Teacher-Specific
        public string? Department { get; set; }

        // Staff-Specific
        public string? Position { get; set; }



    }
}
