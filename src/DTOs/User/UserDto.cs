using System.ComponentModel.DataAnnotations;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
    }

    public class StaffDto : UserDto
    {
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = string.Empty;

        public string PhoneNumber {  get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
    }
    public class AdminDto : UserDto
    {
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
    }
    public class TeacherDto : UserDto
    {
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

    public class StudentDto : UserDto
    {
        public string? FrontStudentIdPicture { get; set; }
        public string? BackStudentIdPicture { get; set; }

        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string StudentIdNumber { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        [Required]
        public string Section { get; set; } = string.Empty;
        [Required]
        public string Year { get; set; } = string.Empty;

        public string? ProfilePicture { get; set; }
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public string CityMunicipality { get; set; } = string.Empty;
        [Required]
        public string Province { get; set; } = string.Empty;
        [Required]
        public string PostalCode { get; set; } = string.Empty;
    }

}
