namespace BackendTechnicalAssetsManagement.src.Classes
{
    public class LentItems
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ItemId { get; set; }

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

        public string Remarks { get; set; } = string.Empty;

    }
}
