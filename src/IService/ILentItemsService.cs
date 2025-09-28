using BackendTechnicalAssetsManagement.src.Classes;

namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface ILentItemsService
    {
        Task<LentItems> AddAsync(LentItems lentItem);
        Task<IEnumerable<LentItems>> GetAllAsync();
        Task<LentItems?> GetByIdAsync(Guid id);
        Task<LentItems?> GetByDateTimeAsync(DateTime dateTime);
        Task UpdateAsync(LentItems lentItem);
        Task<bool> SoftDeleteAsync(Guid id);
        Task<bool> PermaDeleteAsync(Guid id);
        Task<bool> SaveChangesAsync();

        // 🔹 Admin-only methods
        Task<IEnumerable<LentItems>> GetAllIncludingDeletedAsync();
        Task<IEnumerable<LentItems>> GetDeletedAsync();
        Task<LentItems?> GetDeletedByIdAsync(Guid id);
        Task<bool> RestoreAsync(Guid id);
    }
}
