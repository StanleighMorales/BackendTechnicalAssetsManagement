using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Statistics;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class SummaryService : ISummaryService
    {
        // These are the "Suppliers" of data for our service.
        private readonly IItemRepository _itemRepository;
        private readonly ILentItemsRepository _lentItemRepository;
        private readonly IUserRepository _userRepository;

        // This is the constructor where the suppliers (repositories) are "injected".
        public SummaryService(IItemRepository itemRepository, ILentItemsRepository lentItemRepository, IUserRepository userRepository)
        {
            _itemRepository = itemRepository;
            _lentItemRepository = lentItemRepository;
            _userRepository = userRepository;
        }

        // We will implement the methods one by one here.
        public async Task<ActiveUserCount> GetActiveUserCountAsync()
        {
            // 1. Get all users from the database.
            // EF Core's inheritance handling means this should return a list containing
            // Admins, Staff, Teachers, and Students, all as 'User' objects.
            var allUsers = await _userRepository.GetAllAsync();

            // 2. Filter this list to get only the active users.
            //    IMPORTANT: Change "Active" if you use a different value in your database!
            var activeUsers = allUsers.Where(user => user.Status == "Active").ToList();

            // 3. Create the DTO and populate it with counts from the filtered 'activeUsers' list.
            var activeUserCountDto = new ActiveUserCount
            {
                TotalActiveUsers = activeUsers.Count(),

                // Count active users based on their UserRole enum
                // Note: We check for both Admin and SuperAdmin for the total admin count.
                TotalActiveAdmins = activeUsers.Count(user => user.UserRole == UserRole.Admin || user.UserRole == UserRole.SuperAdmin),
                TotalActiveStaffs = activeUsers.Count(user => user.UserRole == UserRole.Staff),
                TotalActiveTeachers = activeUsers.Count(user => user.UserRole == UserRole.Teacher),
                TotalActiveStudents = activeUsers.Count(user => user.UserRole == UserRole.Student)
            };

            return activeUserCountDto;
        }

        public async Task<ItemCount> GetItemCountAsync()
        {
            // 1. Get all the items from the database in a single call.
            var allItems = await _itemRepository.GetAllAsync();

            // 2. Perform counts using the enums for better type safety.
            var itemCountDto = new ItemCount
            {
                // Counts for the SummaryDto base properties
                TotalItems = allItems.Count(),

                // Counts for Item statuses (using the ItemCondition enum)
                NewItems = allItems.Count(item => item.Condition == ItemCondition.New),
                GoodItems = allItems.Count(item => item.Condition == ItemCondition.Good),
                DefectiveItems = allItems.Count(item => item.Condition == ItemCondition.Defective),
                RefurbishedItems = allItems.Count(item => item.Condition == ItemCondition.Refurbished),
                NeedRepairItems = allItems.Count(item => item.Condition == ItemCondition.NeedRepair),

                // Counts for Item categories (using the ItemCategory enum)
                Electronic = allItems.Count(item => item.Category == ItemCategory.Electronics),
                Keys = allItems.Count(item => item.Category == ItemCategory.Keys),
                MediaEquipment = allItems.Count(item => item.Category == ItemCategory.MediaEquipment),
                Tools = allItems.Count(item => item.Category == ItemCategory.Tools),
                Miscellaneous = allItems.Count(item => item.Category == ItemCategory.Miscellaneous)
            };

            return itemCountDto;
        }

        // Inside the SummaryService class

        public async Task<LentItemsCount> GetLentItemsCountAsync()
        {
            // 1. Get all the necessary data from our repositories.
            var allLentRecords = await _lentItemRepository.GetAllAsync();
            var allItems = await _itemRepository.GetAllAsync();
            var allUsers = await _userRepository.GetAllAsync();

            // 2. Create the DTO to hold our results.
            var lentItemsCountDto = new LentItemsCount
            {
                // Properties inherited from the base SummaryDto
                TotalItems = allItems.Count(),
                TotalLentItems = allLentRecords.Count(), // This is the total number of borrow transactions.
                TotalActiveUsers = allUsers.Count(u => u.Status == "Active"), // Assumes "Active" status string.

                // Properties specific to the LentItemsCount DTO
                // An item is currently lent if its 'ReturnedAt' date is null.
                CurrentlyLentItems = allLentRecords.Count(record => record.ReturnedAt == null),

                // An item is considered returned if its 'ReturnedAt' date has a value.
                ReturnedLentItems = allLentRecords.Count(record => record.ReturnedAt != null)
            };

            // 3. Return the completed DTO.
            return lentItemsCountDto;
        }
        public async Task<SummaryDto> GetOverallSummaryAsync()
        {
            // 1. Ask the librarian for the items and WAIT.
            var allItems = await _itemRepository.GetAllAsync();

            // 2. NOW that they are back, ask for the lent items and WAIT.
            var allLentRecords = await _lentItemRepository.GetAllAsync();

            // 3. NOW that they are back, ask for the users and WAIT.
            var allUsers = await _userRepository.GetAllAsync();

            // 4. Create the DTO and perform the final counts.
            var summary = new SummaryDto
            {
                TotalItems = allItems.Count(),
                TotalLentItems = allLentRecords.Count(),
                TotalActiveUsers = allUsers.Count(u => u.Status == "Active")
            };

            return summary;
        }
    }
}
