using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTechnicalAssetsManagement.src.Classes
{
    public record RefreshToken
    {
        [Key]
        public int Id { get; set; }

        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedAt { get; set; }

        // Foreign Key to the User
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}