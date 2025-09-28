using BackendTechnicalAssetsManagement.src.Classes;
namespace BackendTechnicalAssetsManagement.src.IRepository
{
    public interface ILentItemsRepository
    {
        Task<LentItems?> GetByIdAsync(Guid id);

        Task<IEnumerable<LentItems>> GetAllAsync();

        Task<LentItems?> GetByDateTime(DateTime dateTime);
        Task<LentItems> AddAsync(LentItems lentItem);

        Task UpdateAsync(LentItems lentItem);

        Task SoftDeleteAsync(Guid id);
        Task PermaDeleteAsync(Guid id);
        
        Task<bool> SaveChangesAsync();

        //Admin-only methods
        Task<IEnumerable<LentItems>> GetAllIncludingDeletedAsync();
        Task<IEnumerable<LentItems>> GetDeletedAsync();
        Task<LentItems?> GetDeletedByIdAsync(Guid id);
        Task RestoreAsync(Guid id);

    }
}
