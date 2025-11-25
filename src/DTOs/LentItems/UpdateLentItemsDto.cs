namespace BackendTechnicalAssetsManagement.src.DTOs
{
    public class UpdateLentItemDto
    {

        public Guid? ItemId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? TeacherId { get; set; }
        public string? Room { get; set; }
        public string? SubjectTimeSchedule { get; set; }
        public DateTime? ReservedFor { get; set; }
        public string? Remarks { get; set; }
        public string? Status { get; set; }
        public DateTime? ReturnedAt { get; set; }
    }
}
