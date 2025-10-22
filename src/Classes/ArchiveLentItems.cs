using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTechnicalAssetsManagement.src.Classes
{
    public class ArchiveLentItems
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey("Id")]
        public Guid LentItemId { get; set; }

        public Guid ItemId { get; set; }
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

        public string Room { get; set; } = string.Empty;
        public string SubjectTimeSchedule { get; set; } = string.Empty;

        public DateTime LentAt { get; set; } = DateTime.Now;
        public DateTime? ReturnedAt { get; set; }

        public string? Status { get; set; }// Possible values: "Lent", "Returned"
        public string? Remarks { get; set; }

        public bool IsHiddenFromUser { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string? Barcode { get; set; }
        public byte[]? BarcodeImage { get; set; }
    }
}
