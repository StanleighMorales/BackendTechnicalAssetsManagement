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

            [EmailAddress]
            public string? Email { get; set; }

            public string? StudentIdNumber { get; set; }

            public string? Course { get; set; }
            public string? Section { get; set; }
            public string? Year { get; set; }

            public string? Street { get; set; }
            public string? CityMunicipality { get; set; }
            public string? Province { get; set; }
            public string? PostalCode { get; set; }

            [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
            public string? PhoneNumber { get; set; }
            public IFormFile? ProfilePicture { get; set; }
            public IFormFile? FrontStudentIdPicture { get; set; }
            public IFormFile? BackStudentIdPicture { get; set; }
        }
        public class UpdateTeacherProfileDto
        {
           
            public string? LastName { get; set; }
            public string? MiddleName { get; set; }
            public string? FirstName { get; set; }
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
            public string? PhoneNumber { get; set; }
            public string? Department { get; set; }
        }
        public class UpdateStaffProfileDto
        {
            public string? Username { get; set; }
            public string? Email { get; set; }
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
            public string? PhoneNumber { get; set; }

            public string? LastName { get; set; }

            public string? MiddleName { get; set; }

            public string? FirstName { get; set; }

            public string? Position { get; set; }
        }

    }
}
