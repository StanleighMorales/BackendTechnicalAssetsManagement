    using BackendTechnicalAssetsManagement.src.DTOs;

    namespace BackendTechnicalAssetsManagement.src.IService
    {
        public interface ILentItemsService
        {
            // Create
            Task<LentItemsDto> AddAsync(CreateLentItemDto dto);

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

            //Admin-only methods
            Task<IEnumerable<LentItemsDto>> GetAllIncludingDeletedAsync();
            Task<IEnumerable<LentItemsDto>> GetDeletedAsync();
            Task<LentItemsDto?> GetDeletedByIdAsync(Guid id);
            Task<bool> RestoreAsync(Guid id);
        }
    }
