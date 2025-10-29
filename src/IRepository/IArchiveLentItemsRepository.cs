using BackendTechnicalAssetsManagement.src.Classes;
namespace BackendTechnicalAssetsManagement.src.IRepository
{
    public interface IArchiveLentItemsRepository
    {
        Task<ArchiveLentItems> CreateArchiveLentItemsAsync(ArchiveLentItems archiveLentItems);
        Task<IEnumerable<ArchiveLentItems>> GetAllArchiveLentItemsAsync();
        Task<ArchiveLentItems?> GetArchiveLentItemsByIdAsync(Guid id);
        Task<ArchiveLentItems?> UpdateArchiveLentItemsAsync(Guid id, ArchiveLentItems archiveLentItems);
        Task DeleteArchiveLentItemsAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }
}
