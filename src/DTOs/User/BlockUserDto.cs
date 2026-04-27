using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    public class BlockUserDto
    {
        [Required(ErrorMessage = "Block reason is required")]
        [StringLength(500, ErrorMessage = "Block reason cannot exceed 500 characters")]
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// If true, this is a permanent ban. If false, BlockedUntil must be provided.
        /// </summary>
        public bool IsPermanent { get; set; } = false;

        /// <summary>
        /// Required if IsPermanent is false. The date/time when the temporary ban expires.
        /// </summary>
        public DateTime? BlockedUntil { get; set; }
    }

    public class UnblockUserDto
    {
        public string? UnblockNote { get; set; }
    }

    public class BlockStatusDto
    {
        public bool IsBlocked { get; set; }
        public string? BlockReason { get; set; }
        public DateTime? BlockedAt { get; set; }
        public DateTime? BlockedUntil { get; set; }
        public bool IsPermanent { get; set; }
        public Guid? BlockedById { get; set; }
        public string? BlockedByUsername { get; set; }
    }
}
