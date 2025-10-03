    using BackendTechnicalAssetsManagement.src.DTOs;
using BackendTechnicalAssetsManagement.src.DTOs.LentItems;

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
            Task UpdateAsync(UpdateLentItemDto dto);

            // Delete
            Task<bool> SoftDeleteAsync(Guid id);
            Task<bool> PermaDeleteAsync(Guid id);

            // Persistence
            Task<bool> SaveChangesAsync();

        }
    }
