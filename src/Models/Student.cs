using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.Models
{
    public class Student : User
    {
        public string? ProfilePicture { get; set; }

        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string StudentIdNumber { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string Course { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string CityMunicipality { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
         
        public string? FrontStudentIdPictureUrl { get; set; }
        public string? BackStudentIdPictureUrl { get; set; }

    }
}
