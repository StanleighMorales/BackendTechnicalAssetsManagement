namespace BackendTechnicalAssetsManagement.src.DTOs.Statistics
{
    public class SummaryDto
    {
        public int? TotalItems { get; set; }
        public int? TotalLentItems { get; set; }

        public int? TotalActiveUsers { get; set; }
    }
    public class ItemCount : SummaryDto
    {
        public int? NewItems { get; set; }
        public int? GoodItems { get; set; }
        public int? DefectiveItems { get; set; }
        public int? RefurbishedItems { get; set; }
        public int? NeedRepairItems { get; set; }

        public int? Electronic { get; set; }
        public int? Keys { get; set; }
        public int? MediaEquipment { get; set; }
        public int? Tools { get; set; }
        public int? Miscellaneous { get; set; }

    }
    public class LentItemsCount : SummaryDto
    {

        public int? CurrentlyLentItems { get; set; }
        public int? ReturnedLentItems { get; set; }
    }

    public class ActiveUserCount : SummaryDto
    {
        
        public int? TotalActiveAdmins { get; set; }
        public int? TotalActiveStaffs { get; set; }
        public int? TotalActiveTeachers { get; set; }
        public int? TotalActiveStudents { get; set; }

    }
}
