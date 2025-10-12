    using BackendTechnicalAssetsManagement.src.DTOs;
using BackendTechnicalAssetsManagement.src.DTOs.LentItems;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

    namespace BackendTechnicalAssetsManagement.src.IService
    {
        public interface ILentItemsService
        {
            // Create
            Task<LentItemsDto> AddAsync(CreateLentItemDto dto);

            Task<LentItemsDto> AddForGuestAsync(CreateLentItemsForGuestDto dto);
            // Read
            Task<IEnumerable<LentItemsDto>> GetAllAsync();
            Task<LentItemsDto?> GetByIdAsync(Guid id);
            Task<LentItemsDto?> GetByDateTimeAsync(DateTime dateTime);

            // Update
            Task<bool> UpdateAsync(Guid id, UpdateLentItemDto dto);
            Task<bool> UpdateStatusAsync(Guid id, ScanLentItemDto dto);

        // Delete
        Task<bool> SoftDeleteAsync(Guid id);
            Task<bool> PermaDeleteAsync(Guid id);

            // Persistence
            Task<bool> SaveChangesAsync();

        }
    }
