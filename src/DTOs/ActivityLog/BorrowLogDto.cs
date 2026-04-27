using System.Text.Json.Serialization;

namespace BackendTechnicalAssetsManagement.src.DTOs.ActivityLog
{
    /// <summary>
    /// Borrow-specific log DTO with full borrower and status-transition details.
    /// </summary>
    public class BorrowLogDto
    {
        public Guid Id { get; set; }

        // Borrower
        public Guid? BorrowerUserId { get; set; }
        public string BorrowerName { get; set; } = string.Empty;
        public string BorrowerRole { get; set; } = string.Empty;
        public string? StudentIdNumber { get; set; }
        public string? FrontStudentIdPictureUrl { get; set; }
        public string? GuestImageUrl { get; set; }

        // Item
        public Guid ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? ItemSerialNumber { get; set; }

        // Status transition
        public string? PreviousStatus { get; set; }
        public string CurrentStatus { get; set; } = string.Empty;

        // Timestamps
        [JsonIgnore]
        public DateTime? BorrowedAt { get; set; }
        [JsonPropertyName("borrowedAt")]
        public string? BorrowedAtFormatted => BorrowedAt?.ToString("yyyy-MM-dd HH:mm");

        [JsonIgnore]
        public DateTime? ReturnedAt { get; set; }
        [JsonPropertyName("returnedAt")]
        public string? ReturnedAtFormatted => ReturnedAt?.ToString("yyyy-MM-dd HH:mm");

        [JsonIgnore]
        public DateTime? ReservedFor { get; set; }
        [JsonPropertyName("reservedFor")]
        public string? ReservedForFormatted => ReservedFor?.ToString("yyyy-MM-dd HH:mm");

        public string? Remarks { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("createdAt")]
        public string CreatedAtFormatted => CreatedAt.ToString("yyyy-MM-dd HH:mm");
    }
}
