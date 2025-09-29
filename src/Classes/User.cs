using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Classes
{
    public class User
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string? PasswordHash { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public UserRole UserRole { get; set; } = UserRole.Staff;

        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
    }
    public class Teacher : User
    {
        public string? PhoneNumber { get; set; }
        public string? Department { get; set; }
    }
    public class Student : User
    {
        public byte[]? ProfilePicture { get; set; }

        public string StudentIdNumber { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string Course { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string CityMunicipality { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public byte[]? FrontStudentIdPicture { get; set; }
        public byte[]? BackStudentIdPicture { get; set; }

    }
    public class Staff : User
    {
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
    }
    public class Admin : User
    {
        public string? PhoneNumber { get; set; }
    }

}
