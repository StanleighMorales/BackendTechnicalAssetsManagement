using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTechnicalAssetsManagement.src.Classes
{
    public class ArchiveLentItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public Guid? ItemId { get; set; }
        public Item? Item { get; set; }
        public string ItemName { get; set; } = string.Empty;

        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public Guid? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        // Denormalized fields
        public string BorrowerFullName { get; set; } = string.Empty;
        public string BorrowerRole { get; set; } = string.Empty;
        public string? StudentIdNumber { get; set; }
        public string? TeacherFullName { get; set; } = string.Empty;

        public string? Room { get; set; }
        public string SubjectTimeSchedule { get; set; } = string.Empty;

        public DateTime? LentAt { get; set; }
        public DateTime? ReturnedAt { get; set; }

        public string? Status { get; set; }// Possible values: "Lent", "Returned"
        public string? Remarks { get; set; }

        public bool IsHiddenFromUser { get; set; } = false;

        public string? TagUid { get; set; }       // RFID tag UID of the item being borrowed
        public string? StudentRfid { get; set; }  // RFID UID of the student's ID card

        public string? FrontStudentIdPictureUrl { get; set; }

        // Guest-specific fields
        public string? GuestImageUrl { get; set; }
        public string? Organization { get; set; }
        public string? ContactNumber { get; set; }
        public string? Purpose { get; set; }

        public DateTime? ReservedFor { get; set; }  // When the user plans to use the item

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
