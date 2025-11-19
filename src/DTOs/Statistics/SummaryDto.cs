namespace BackendTechnicalAssetsManagement.src.DTOs.Statistics
{
    /// <summary>
    /// Represents a basic summary of the core entities in the system.
    /// Serves as a base class for more detailed statistical DTOs.
    /// </summary>
    public class SummaryDto
    {
        /// <summary>
        /// The total number of items registered in the system.
        /// </summary>
        /// <example>150</example>
        public int? TotalItems { get; set; }

        /// <summary>
        /// The total number of lending transactions that have occurred (both active and returned).
        /// </summary>
        /// <example>75</example>
        public int? TotalLentItems { get; set; }

        /// <summary>
        /// The total number of users with an "Active" status.
        /// </summary>
        /// <example>42</example>
        public int? TotalActiveUsers { get; set; }
        /// <summary>
        /// The total number of category used for items.
        /// </summary>
        /// <example>5</example>
        public int? TotalItemsCategories { get; set; }

        /// <summary>
        /// Stock information for all items grouped by name.
        /// </summary>
        public List<ItemStockDto>? ItemStocks { get; set; }
    }

    /// <summary>
    /// Extends the base summary with a detailed breakdown of items by their condition and category.
    /// </summary>
    public class ItemCount 
    {
        public int? TotalItems { get; set; }
        /// <summary>
        /// The count of items with the 'New' condition.
        /// </summary>
        /// <example>25</example>
        public int? NewItems { get; set; }

        /// <summary>
        /// The count of items with the 'Good' condition.
        /// </summary>
        /// <example>100</example>
        public int? GoodItems { get; set; }

        /// <summary>
        /// The count of items with the 'Defective' condition.
        /// </summary>
        /// <example>5</example>
        public int? DefectiveItems { get; set; }

        /// <summary>
        /// The count of items with the 'Refurbished' condition.
        /// </summary>
        /// <example>10</example>
        public int? RefurbishedItems { get; set; }

        /// <summary>
        /// The count of items with the 'NeedRepair' condition.
        /// </summary>
        /// <example>10</example>
        public int? NeedRepairItems { get; set; }

        /// <summary>
        /// The count of items belonging to the 'Electronic' category.
        /// </summary>
        /// <example>60</example>
        public int? Electronic { get; set; }

        /// <summary>
        /// The count of items belonging to the 'Keys' category.
        /// </summary>
        /// <example>20</example>
        public int? Keys { get; set; }

        /// <summary>
        /// The count of items belonging to the 'MediaEquipment' category.
        /// </summary>
        /// <example>30</example>
        public int? MediaEquipment { get; set; }

        /// <summary>
        /// The count of items belonging to the 'Tools' category.
        /// </summary>
        /// <example>15</example>
        public int? Tools { get; set; }

        /// <summary>
        /// The count of items belonging to the 'Miscellaneous' category.
        /// </summary>
        /// <example>25</example>
        public int? Miscellaneous { get; set; }
    }

    /// <summary>
    /// Extends the base summary with a detailed breakdown of lent items.
    /// </summary>
    public class LentItemsCount 
    {
        public int? TotalLentItems { get; set; }
        /// <summary>
        /// The total number of items that are currently borrowed and not yet returned.
        /// </summary>
        /// <example>15</example>
        public int? CurrentlyLentItems { get; set; }

        /// <summary>
        /// The total number of items that have been successfully returned.
        /// </summary>
        /// <example>60</example>
        public int? ReturnedLentItems { get; set; }
    }

    /// <summary>
    /// Extends the base summary with a detailed breakdown of active users by their role.
    /// </summary>
    public class ActiveUserCount 
    {
        public int? TotalActiveUsers { get; set; }
        /// <summary>
        /// The total count of active users with the 'Admin' or 'SuperAdmin' role.
        /// </summary>
        /// <example>2</example>
        public int? TotalActiveAdmins { get; set; }

        /// <summary>
        /// The total count of active users with the 'Staff' role.
        /// </summary>
        /// <example>5</example>
        public int? TotalActiveStaffs { get; set; }

        /// <summary>
        /// The total count of active users with the 'Teacher' role.
        /// </summary>
        /// <example>10</example>
        public int? TotalActiveTeachers { get; set; }

        /// <summary>
        /// The total count of active users with the 'Student' role.
        /// </summary>
        /// <example>25</example>
        public int? TotalActiveStudents { get; set; }
    }

    /// <summary>
    /// Represents stock information for a specific item grouped by name.
    /// </summary>
    public class ItemStockDto
    {
        /// <summary>
        /// The name of the item.
        /// </summary>
        /// <example>HDMI Cable</example>
        public string ItemName { get; set; } = string.Empty;

        /// <summary>
        /// The type of the item.
        /// </summary>
        /// <example>Cable</example>
        public string ItemType { get; set; } = string.Empty;

        /// <summary>
        /// The total count of this item (all statuses).
        /// </summary>
        /// <example>10</example>
        public int TotalCount { get; set; }

        /// <summary>
        /// The count of available items (not borrowed).
        /// </summary>
        /// <example>7</example>
        public int AvailableCount { get; set; }

        /// <summary>
        /// The count of unavailable items (currently borrowed).
        /// </summary>
        /// <example>3</example>
        public int BorrowedCount { get; set; }
    }

    /// <summary>
    /// Represents a summary of all item stocks in the system.
    /// </summary>
    public class ItemStockSummary
    {
        /// <summary>
        /// List of stock information grouped by item name.
        /// </summary>
        public List<ItemStockDto> ItemStocks { get; set; } = new List<ItemStockDto>();

        /// <summary>
        /// Total number of unique item names in the system.
        /// </summary>
        /// <example>25</example>
        public int TotalUniqueItems { get; set; }

        /// <summary>
        /// Total count of all items across all names.
        /// </summary>
        /// <example>150</example>
        public int TotalItemsCount { get; set; }

        /// <summary>
        /// Total count of all available items.
        /// </summary>
        /// <example>120</example>
        public int TotalAvailableCount { get; set; }

        /// <summary>
        /// Total count of all borrowed items.
        /// </summary>
        /// <example>30</example>
        public int TotalBorrowedCount { get; set; }
    }
}