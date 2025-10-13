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

            /// <summary>
            /// Updates the IsHiddenFromUser flag for a specific LentItem, only if it belongs to the specified user.
            /// </summary>
            /// <param name="lentItemId">The ID of the LentItem record to update.</param>
            /// <param name="userId">The ID of the user requesting the change (used for authorization).</param>
            /// <param name="isHidden">The new value for the IsHiddenFromUser flag (true to hide, false to unhide).</param>
            /// <returns>True if the update was successful, false otherwise (e.g., not found or not authorized).</returns>
            Task<bool> UpdateHistoryVisibility(Guid lentItemId, Guid userId, bool isHidden);

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
