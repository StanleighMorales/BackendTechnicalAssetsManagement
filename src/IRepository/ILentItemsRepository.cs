using BackendTechnicalAssetsManagement.src.Classes;

namespace BackendTechnicalAssetsManagement.src.IRepository
{
    public interface ILentItemsRepository
    {
        Task<LentItems?> GetByIdAsync(Guid id);
        Task<IEnumerable<LentItems>> GetAllAsync();
        Task<IEnumerable<LentItems>> GetAllLightAsync();
        Task<IEnumerable<LentItems>> GetActiveByItemIdLightAsync(Guid itemId);
        Task<IEnumerable<LentItems>> GetActiveByUserIdLightAsync(Guid userId);
        Task<IEnumerable<LentItems>> GetExpiredReservationsAsync(DateTime cutoff);
        Task<IEnumerable<LentItems>> GetByDateTime(DateTime dateTime);
        Task<IEnumerable<LentItems>> GetAllBorrowedItemsAsync();

        Task<LentItems> AddAsync(LentItems lentItem);
        Task UpdateAsync(LentItems lentItem);

        Task SoftDeleteAsync(Guid id);
        Task PermaDeleteAsync(Guid id);

        Task<bool> SaveChangesAsync();

        Task<LentItems?> GetActiveByItemIdAsync(Guid itemId);
    }
}
