using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackendTechnicalAssetsManagement.src.Classes
{
    public class LentItems
    {
        public Guid Id { get; set; } = Guid.NewGuid();

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

        public DateTime? ReservedFor { get; set; }  // When the user plans to use the item
        public DateTime? LentAt { get; set; }
        public DateTime? ReturnedAt { get; set; }

        public string? Status { get; set; }// Possible values: "Lent", "Returned"
        public string? Remarks { get; set; }

        public bool IsHiddenFromUser { get; set; } = false;

        public string? Barcode { get; set; }
        public byte[]? BarcodeImage { get; set; }

        public byte[]? FrontStudentIdPicture { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
