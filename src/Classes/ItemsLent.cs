namespace BackendTechnicalAssetsManagement.src.Classes
{
    public class ItemsLent
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ItemId { get; set; }

        public Guid? UserId { get; set; }

        public string Room { get; set; } = string.Empty;

        public Guid? TeacherId { get; set; }

        public DateTime LentAt { get; set; } = DateTime.Now;

        public DateTime? ReturnedAt { get; set; }

        public string SubjectTimeSchedule { get; set; } = string.Empty;

        public string Remarks { get; set; } = string.Empty;


        public DateTime? DeletedAt { get; set; }

        public bool? IsDeleted { get; set; } = false;


    }
}
