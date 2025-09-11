namespace BackendTechnicalAssetsManagement.src.Models
{
    public class Staff : User
    {
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
    }
}
