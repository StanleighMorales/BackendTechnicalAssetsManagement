using BackendTechnicalAssetsManagement.src.Classes.ArchiveUsers;
using System.Text.Json.Serialization;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.Archive.Users
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(ArchiveStudentDto), typeDiscriminator: "ArchiveStudent")] // Corrected type
    [JsonDerivedType(typeof(ArchiveTeacherDto), typeDiscriminator: "ArchiveTeacher")] // Corrected type
    [JsonDerivedType(typeof(ArchiveStaffDto), typeDiscriminator: "ArchiveStaff")]     // Corrected type
    public class ArchiveUserDto
    {
        public Guid Id { get; set; } // No default new Guid()
        public Guid? OriginalUserId { get; set; }
        public string? Username { get; set; }
        // REMOVED: public string? PasswordHash { get; set; }

        public string? Email { get; set; }

        public UserRole? UserRole { get; set; }

        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? FirstName { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Status { get; set; } // No default "Inactive"
        public DateTime ArchivedAt { get; set; }
    }

    public class ArchiveStudentDto : ArchiveUserDto
    {
        // Changed byte[] to string for client consumption (Base64)
        public string? ProfilePicture { get; set; }
        public string? StudentIdNumber { get; set; }
        public string? Course { get; set; }
        public string? Year { get; set; }
        public string? Section { get; set; }
        public string? Street { get; set; }
        public string? CityMunicipality { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public string? FrontStudentIdPicture { get; set; } // Changed byte[] to string
        public string? BackStudentIdPicture { get; set; }   // Changed byte[] to string
    }

    public class ArchiveTeacherDto : ArchiveUserDto
    {
        public string? Department { get; set; }
    }

    public class ArchiveStaffDto : ArchiveUserDto
    {
        public string? Position { get; set; }
    }
}