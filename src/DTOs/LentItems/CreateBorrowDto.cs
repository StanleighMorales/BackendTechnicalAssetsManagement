using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.LentItems
{
    /// <summary>
    /// DTO for an instant borrow — item is taken right now, no reservation time needed.
    /// Status is always set to "Pending" by the backend (auto-promoted to "Borrowed" via RFID flow).
    /// </summary>
    public class CreateBorrowDto
    {
        [Required]
        public Guid ItemId { get; set; }

        /// <summary>
        /// Authenticated user borrowing the item. Mutually exclusive with TeacherId.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Teacher borrowing the item. Mutually exclusive with UserId.
        /// </summary>
        public Guid? TeacherId { get; set; }

        public string? Room { get; set; }

        [Required]
        public string SubjectTimeSchedule { get; set; } = string.Empty;

        public string? Remarks { get; set; }
    }
}
