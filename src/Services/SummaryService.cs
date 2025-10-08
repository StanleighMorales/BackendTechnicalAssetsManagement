using BackendTechnicalAssetsManagement.src.DTOs.Statistics;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using System.Linq;
using System.Threading.Tasks;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

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
        /// Calculates a high-level, overall summary.
        /// THIS METHOD REMAINS THE SAME.
        /// </summary>
        public async Task<SummaryDto> GetOverallSummaryAsync()
        {
            var allItems = await _itemRepository.GetAllAsync();
            var allLentRecords = await _lentItemRepository.GetAllAsync();
            var allUsers = await _userRepository.GetAllAsync();

            return new SummaryDto
            {
                TotalItems = allItems.Count(),
                TotalLentItems = allLentRecords.Count(),
                TotalActiveUsers = allUsers.Count(u => u.Status == "Active")
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
        /// This is now much more efficient as it only queries users.
        /// </summary>
        public async Task<ActiveUserCount> GetActiveUserCountAsync()
        {
            var allUsers = await _userRepository.GetAllAsync();
            var activeUsers = allUsers.Where(u => u.Status == "Active").ToList();

            return new ActiveUserCount
            {
                TotalActiveUsers = activeUsers.Count(),
                TotalActiveAdmins = activeUsers.Count(user => user.UserRole == UserRole.Admin || user.UserRole == UserRole.SuperAdmin),
                TotalActiveStaffs = activeUsers.Count(user => user.UserRole == UserRole.Staff),
                TotalActiveTeachers = activeUsers.Count(user => user.UserRole == UserRole.Teacher),
                TotalActiveStudents = activeUsers.Count(user => user.UserRole == UserRole.Student)
            };
        }
    }
}