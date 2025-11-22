using BackendTechnicalAssetsManagement.src.DTOs.Item;
using System.Text.Json.Serialization;

namespace BackendTechnicalAssetsManagement.src.DTOs
{
    public class LentItemsDto
    {
        public Guid Id { get; set; }
        //public Guid? ItemId { get; set; }
        public ItemDto? Item { get; set; }
        //public string ItemName { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
        public Guid? TeacherId { get; set; }

        public string BorrowerFullName { get; set; } = string.Empty;
        public string BorrowerRole { get; set; } = string.Empty;
        public string? StudentIdNumber { get; set; }
        public string? TeacherFullName { get; set; }

        public string Room { get; set; } = string.Empty;
        public string SubjectTimeSchedule { get; set; } = string.Empty;

        [JsonIgnore]
        public DateTime? LentAt { get; set; }
        
        [JsonPropertyName("lentAt")]
        public string? LentAtFormatted => LentAt?.ToString("yyyy-MM-dd HH:mm");

        [JsonIgnore]
        public DateTime? ReturnedAt { get; set; }
        
        [JsonPropertyName("returnedAt")]
        public string? ReturnedAtFormatted => ReturnedAt?.ToString("yyyy-MM-dd HH:mm");

        public string Status { get; set; } = string.Empty;

        public string Remarks { get; set; } = string.Empty;
        public bool IsHiddenFromUser { get; set; }

        public string? Barcode { get; set; }
        public string? BarcodeImage { get; set; }
        
        public string? FrontStudentIdPicture { get; set; }
    }

}
