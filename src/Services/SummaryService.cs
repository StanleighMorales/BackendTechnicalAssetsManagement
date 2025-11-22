using BackendTechnicalAssetsManagement.src.DTOs.Statistics;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using System.Linq;
using System.Threading.Tasks;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;
using System.Collections.Generic; // Added for Dictionary extension methods if needed, though GetValueOrDefault is generally available or can be assumed.

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILentItemsRepository _lentItemRepository;
        private readonly IUserRepository _userRepository;

        public SummaryService(IItemRepository itemRepository, ILentItemsRepository lentItemRepository, IUserRepository userRepository)
        {
            _itemRepository = itemRepository;
            _lentItemRepository = lentItemRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Calculates a high-level, overall summary including stock information.
        /// </summary>
        public async Task<SummaryDto> GetOverallSummaryAsync()
        {
            var allItems = await _itemRepository.GetAllAsync();
            var allLentRecords = await _lentItemRepository.GetAllAsync();
            var allUsers = await _userRepository.GetAllAsync();
            var allCategories = Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>();

            // Calculate stock information grouped by ItemType
            var itemStocks = allItems
                .GroupBy(i => i.ItemType)
                .Select(g => new ItemStockDto
                {
                    ItemType = g.Key,
                    TotalCount = g.Count(),
                    AvailableCount = g.Count(i => i.Status == ItemStatus.Available),
                    BorrowedCount = g.Count(i => i.Status == ItemStatus.Unavailable)
                })
                .OrderBy(s => s.ItemType)
                .ToList();

            return new SummaryDto
            {
                TotalItems = allItems.Count(),
                TotalLentItems = allLentRecords.Count(),
                TotalActiveUsers = allUsers.Count(u => u.Status == "Online"),
                TotalItemsCategories = allCategories.Count(),
                ItemStocks = itemStocks
            };
        }

        /// <summary>
        /// Calculates a detailed summary of items ONLY.
        /// This is now much more efficient as it only queries items.
        /// </summary>
        public async Task<ItemCount> GetItemCountAsync()
        {
            var allItems = await _itemRepository.GetAllAsync();

            return new ItemCount
            {
                TotalItems = allItems.Count(),
                NewItems = allItems.Count(item => item.Condition == ItemCondition.New),
                GoodItems = allItems.Count(item => item.Condition == ItemCondition.Good),
                DefectiveItems = allItems.Count(item => item.Condition == ItemCondition.Defective),
                RefurbishedItems = allItems.Count(item => item.Condition == ItemCondition.Refurbished),
                NeedRepairItems = allItems.Count(item => item.Condition == ItemCondition.NeedRepair),
                Electronic = allItems.Count(item => item.Category == ItemCategory.Electronics),
                Keys = allItems.Count(item => item.Category == ItemCategory.Keys),
                MediaEquipment = allItems.Count(item => item.Category == ItemCategory.MediaEquipment),
                Tools = allItems.Count(item => item.Category == ItemCategory.Tools),
                Miscellaneous = allItems.Count(item => item.Category == ItemCategory.Miscellaneous)
            };
        }

        /// <summary>
        /// Calculates a detailed summary of lent items ONLY.
        /// This is now much more efficient as it only queries lent items.
        /// </summary>
        public async Task<LentItemsCount> GetLentItemsCountAsync()
        {
            var allLentRecords = await _lentItemRepository.GetAllAsync();

            return new LentItemsCount
            {
                TotalLentItems = allLentRecords.Count(),
                CurrentlyLentItems = allLentRecords.Count(record => record.ReturnedAt == null),
                ReturnedLentItems = allLentRecords.Count(record => record.ReturnedAt != null)
            };
        }

        /// <summary>
        /// Calculates a detailed summary of active users ONLY.
        /// This is now much more efficient as it only queries users and uses GroupBy for role counts.
        /// </summary>
        public async Task<ActiveUserCount> GetActiveUserCountAsync()
        {
            var allUsers = await _userRepository.GetAllAsync();
            // Filter active users
            var activeUsers = allUsers.Where(u => u.Status == "Online");

            // Group by UserRole and count, then convert to dictionary
            var roleCounts = activeUsers.GroupBy(u => u.UserRole)
                .ToDictionary(g => g.Key, g => g.Count());

            // Calculate total active users from the dictionary values
            var totalActive = roleCounts.Values.Sum();

            // Helper to safely get value from dictionary, defaulting to 0
            // Note: GetValueOrDefault is an extension method often available in modern C# environments.
            static int GetValueOrDefault<TKey>(Dictionary<TKey, int> dict, TKey key) where TKey : notnull
            {
                return dict.TryGetValue(key, out var count) ? count : 0;
            }

            return new ActiveUserCount
            {
                TotalActiveUsers = totalActive,
                TotalActiveAdmins = GetValueOrDefault(roleCounts, UserRole.Admin) + GetValueOrDefault(roleCounts, UserRole.SuperAdmin),
                TotalActiveStaffs = GetValueOrDefault(roleCounts, UserRole.Staff),
                TotalActiveTeachers = GetValueOrDefault(roleCounts, UserRole.Teacher),
                TotalActiveStudents = GetValueOrDefault(roleCounts, UserRole.Student)
            };
        }

    }
}