using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Models.DTOs.Users
{
    public class BaseProfileDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
        public string? Status { get; set; } = string.Empty;

    }
    public class GetStaffProfileDto : BaseProfileDto
    {
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
    }
    public class GetAdminProfileDto : BaseProfileDto
    {
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class GetTeacherProfileDto : BaseProfileDto
    {
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }
    public class GetStudentProfileDto : BaseProfileDto
    {
        public string? ProfilePicture { get; set; }
        public string? FrontStudentIdPicture { get; set; }
        public string? BackStudentIdPicture { get; set; }

        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string StudentIdNumber { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string Course { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;
        public string CityMunicipality { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }

}
