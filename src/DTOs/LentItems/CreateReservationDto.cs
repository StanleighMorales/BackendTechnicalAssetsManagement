using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.LentItems
{
    /// <summary>
    /// DTO for a future reservation — item will be picked up at a scheduled time.
    /// Status is always set to "Pending" by the backend.
    /// ReservedFor is required and must be in the future.
    /// </summary>
    public class CreateReservationDto
    {
        [Required]
        public Guid ItemId { get; set; }

        /// <summary>
        /// Authenticated user making the reservation. Mutually exclusive with TeacherId.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Teacher making the reservation. Mutually exclusive with UserId.
        /// </summary>
        public Guid? TeacherId { get; set; }

        [Required]
        public string Room { get; set; } = string.Empty;

        [Required]
        public string SubjectTimeSchedule { get; set; } = string.Empty;

        /// <summary>
        /// When the borrower plans to pick up the item. Must be in the future and within 7 days.
        /// </summary>
        [Required]
        public DateTime ReservedFor { get; set; }

        public string? Remarks { get; set; }
    }
}
