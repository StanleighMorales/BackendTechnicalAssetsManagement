using static BackendTechnicalAssetsManagement.src.Models.Enums;

namespace BackendTechnicalAssetsManagement.src.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? PasswordHash { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public UserRole UserRole { get; set; } = UserRole.Staff;

        public string? RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
    }
}
