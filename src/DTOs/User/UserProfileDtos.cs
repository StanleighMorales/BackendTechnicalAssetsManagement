using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    public class UserProfileDtos
    {
        public class UpdateStudentProfileDto
        {
            [Required]
            public string LastName { get; set; } = string.Empty;
            public string? MiddleName { get; set; }
            [Required]
            public string FirstName { get; set; } = string.Empty;

            [Required]
            public string StudentIdNumber { get; set; } = string.Empty;

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

            // You can handle file uploads separately or include them here.
            // For simplicity, we'll focus on the data first.
            public IFormFile? ProfilePicture { get; set; }
            public IFormFile? FrontStudentIdPicture { get; set; }
            public IFormFile? BackStudentIdPicture { get; set; }
        }
        public class UpdateTeacherProfileDto
        {
            [Required]
            public string LastName { get; set; } = string.Empty;
            public string? MiddleName { get; set; }
            [Required]
            public string FirstName { get; set; } = string.Empty;
            [Required]
            public string Department { get; set; } = string.Empty;
        }
        public class UpdateStaffProfileDto
        {
            [Required]
            public string LastName { get; set; } = string.Empty;

            public string? MiddleName { get; set; }

            [Required]
            public string FirstName { get; set; } = string.Empty;
            [Required]

            public string? Position { get; set; }
        }
        public class UpdateManagerProfileDto
        {
            [Required]
            public string LastName { get; set; } = string.Empty;

            public string? MiddleName { get; set; }

            [Required]
            public string FirstName { get; set; } = string.Empty;
        }

        public class UpdateAdminProfileDto
        {
            [Required]
            public string LastName { get; set; } = string.Empty;

            public string? MiddleName { get; set; }

            [Required]
            public string FirstName { get; set; } = string.Empty;
        }

    }
}
