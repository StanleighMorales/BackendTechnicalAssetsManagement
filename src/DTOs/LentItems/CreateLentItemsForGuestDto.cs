using System.ComponentModel.DataAnnotations;

namespace BackendTechnicalAssetsManagement.src.DTOs.LentItems
{
    public class CreateLentItemsForGuestDto
    {
        [Required]
        public Guid ItemId { get; set; }

        [Required]
        public string BorrowerFirstName { get; set; } = string.Empty;

        [Required]
        public string BorrowerLastName { get; set; } = string.Empty;

        // General fields
        [Required]
        public string BorrowerRole { get; set; } //"Student" or "Teacher"

        public string? TeacherFirstName { get; set; } = string.Empty;
        public string? TeacherLastName { get; set; } = string.Empty;

        [Required]
        public string Room { get; set; } = string.Empty;

        [Required]
        public string SubjectTimeSchedule { get; set; } = string.Empty;

        public string Remarks { get; set; } = string.Empty;


        // Student-specific fields (nullable if the borrower is a Teacher)
        public string? StudentIdNumber { get; set; }
    }
}
