using BackendTechnicalAssetsManagement.src.Classes;

namespace BackendTechnicalAssetsManagement.src.IRepository
{
    public interface IArchiveItemRepository
    {
        Task<ArchiveItems> CreateItemArchiveAsync(ArchiveItems itemArchive);
        Task<ArchiveItems?> GetItemArchiveByIdAsync(Guid id);
        Task<IEnumerable<ArchiveItems>> GetAllItemArchivesAsync();
        Task<ArchiveItems?> UpdateItemArchiveAsync(ArchiveItems itemArchive);
        Task DeleteItemArchiveAsync(Guid id);

        Task<bool> SaveChangesAsync();
    }
}
