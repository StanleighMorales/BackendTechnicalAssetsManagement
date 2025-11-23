using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTechnicalAssetsManagement.src.DTOs.Archive.LentItems
{
    public class ArchiveLentItemsDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? LentItemId { get; set; }
        public ItemDto? Item { get; set; }

        public Guid? UserId { get; set; }
        public string? User { get; set; }

        public Guid? TeacherId { get; set; }
        public string? Teacher { get; set; }

        // Denormalized fields
        public string? BorrowerFullName { get; set; }
        public string? BorrowerRole { get; set; }
        public string? StudentIdNumber { get; set; }
        public string? TeacherFullName { get; set; }

        public string? Room { get; set; }
        public string? SubjectTimeSchedule { get; set; }

        public DateTime? LentAt { get; set; }
        public DateTime? ReturnedAt { get; set; }

        public string? Status { get; set; }// Possible values: "Lent", "Returned"
        public string? Remarks { get; set; }

        public bool IsHiddenFromUser { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string? Barcode { get; set; }
        public string? BarcodeImage { get; set; }

        public string? FrontStudentIdPicture { get; set; }
    }
}
