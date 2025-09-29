namespace BackendTechnicalAssetsManagement.src.DTOs
{
    public class UpdateLentItemDto
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? TeacherId { get; set; }
        public string Room { get; set; } = string.Empty;
        public string SubjectTimeSchedule { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public DateTime? ReturnedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
