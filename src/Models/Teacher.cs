namespace BackendTechnicalAssetsManagement.src.Models
{
    public class Teacher : User
    {
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Department { get; set; }
    }
}
