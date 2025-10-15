using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    public class UserProfileDtos
    {
        public class UpdateStudentProfileDto
        {
            public string? LastName { get; set; }
            public string? MiddleName { get; set; }
 
            public string? FirstName { get; set; }

            public string? StudentIdNumber { get; set; }

            public string? Course { get; set; }
            public string? Section { get; set; }
            public string? Year { get; set; }

            public string? Street { get; set; }
            public string? CityMunicipality { get; set; }
            public string? Province { get; set; }
            public string? PostalCode { get; set; }

            // You can handle file uploads separately or include them here.
            // For simplicity, we'll focus on the data first.
            public IFormFile? ProfilePicture { get; set; }
            public IFormFile? FrontStudentIdPicture { get; set; }
            public IFormFile? BackStudentIdPicture { get; set; }
        }
        public class UpdateTeacherProfileDto
        {
           
            public string LastName { get; set; } = string.Empty;
            public string? MiddleName { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string Department { get; set; } = string.Empty;
        }
        public class UpdateStaffProfileDto
        {
            public string LastName { get; set; } = string.Empty;

            public string? MiddleName { get; set; }

            public string FirstName { get; set; } = string.Empty;

            public string? Position { get; set; }
        }
        public class UpdateManagerProfileDto
        {
            public string LastName { get; set; } = string.Empty;

            public string? MiddleName { get; set; }

            public string FirstName { get; set; } = string.Empty;
        }

        public class UpdateAdminProfileDto
        {
            public string LastName { get; set; } = string.Empty;

            public string? MiddleName { get; set; }

            public string FirstName { get; set; } = string.Empty;
        }

    }
}
