using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Classes
{
    public class ArchiveUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? OriginalUserId { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }

        public string? Email { get; set; }

        public UserRole? UserRole { get; set; }

        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? FirstName { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Status { get; set; } = "Inactive";
       public DateTime ArchivedAt { get; set; }
    }
    public class ArchiveStudent : ArchiveUser
    {
        public byte[]? ProfilePicture { get; set; }
        public string? StudentIdNumber { get; set; }
        public string? Course { get; set; }
        public string? Year { get; set; }
        public string? Section { get; set; }
        public string? Street { get; set; }
        public string? CityMunicipality { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public byte[]? FrontStudentIdPicture { get; set; }
        public byte[]? BackStudentIdPicture { get; set; }
    }
    public class ArchiveTeacher : ArchiveUser
    {
        public string? Department { get; set; }
    }
    public class ArchiveStaff : ArchiveUser
    {
        public string? Position { get; set; }
    }

}
