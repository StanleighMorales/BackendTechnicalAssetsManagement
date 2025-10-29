namespace BackendTechnicalAssetsManagement.src.DTOs
{
    public class CreateLentItemDto
    {
        public Guid ItemId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? TeacherId { get; set; }
        public string Room { get; set; } = string.Empty;
        public string SubjectTimeSchedule { get; set; } = string.Empty;
        public string? Remarks { get; set; }
        public string? Status { get; set; } = string.Empty;
    }
}
