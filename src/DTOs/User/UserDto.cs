using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(StudentDto), typeDiscriminator: "Student")]
    [JsonDerivedType(typeof(TeacherDto), typeDiscriminator: "Teacher")]
    [JsonDerivedType(typeof(StaffDto), typeDiscriminator: "Staff")]
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }

        public string? Status { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = string.Empty;

    }

    public class StaffDto : UserDto
    {

        public string PhoneNumber {  get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
    }
    public class TeacherDto : UserDto
    {
        public string Department { get; set; } = string.Empty;
    }

    public class StudentDto : UserDto
    {
        public string? FrontStudentIdPicture { get; set; }
        public string? BackStudentIdPicture { get; set; }

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
