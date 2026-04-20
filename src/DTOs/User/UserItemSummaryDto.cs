namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    /// <summary>
    /// DTO for user's item summary showing counts of items by status
    /// </summary>
    public class UserItemSummaryDto
    {
        public int ReservedCount { get; set; }
        public int BorrowedCount { get; set; }
        public int ReturnedCount { get; set; }
        public int AvailableCount { get; set; }
    }

    /// <summary>
    /// DTO for individual item status count
    /// </summary>
    public class ItemStatusCountDto
    {
        public string Status { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
