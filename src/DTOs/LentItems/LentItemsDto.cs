namespace BackendTechnicalAssetsManagement.src.DTOs
{
    public class LentItemsDto
    {
        public Guid Id { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? TeacherId { get; set; }

        public string BorrowerFullName { get; set; } = string.Empty;
        public string BorrowerRole { get; set; } = string.Empty;
        public string? TeacherFullName { get; set; }

        public string Room { get; set; } = string.Empty;
        public string SubjectTimeSchedule { get; set; } = string.Empty;

        public DateTime LentAt { get; set; }
        public DateTime? ReturnedAt { get; set; }

        public string Remarks { get; set; } = string.Empty;

    }
    public class LentItemsCount
    {
        public int? TotalLentItems { get; set; }
        public int? CurrentlyLentItems { get; set; }
        public int? ReturnedLentItems { get; set; }
    }
}
