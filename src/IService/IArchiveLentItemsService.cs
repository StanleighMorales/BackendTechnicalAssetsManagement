using BackendTechnicalAssetsManagement.src.DTOs.Archive.Items;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.LentItems;
using BackendTechnicalAssetsManagement.src.DTOs.Item;

namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IArchiveLentItemsService
    {
        Task<ArchiveLentItemsDto> CreateLentItemsArchiveAsync(CreateArchiveLentItemsDto createLentItemsArchive);
        Task<IEnumerable<ArchiveLentItemsDto>> GetAllLentItemsArchivesAsync();
        Task<ArchiveLentItemsDto?> GetLentItemsArchiveByIdAsync(Guid id);

        Task<bool> DeleteLentItemsArchiveAsync(Guid id);
        Task<ArchiveLentItemsDto?> RestoreLentItemsAsync(Guid archiveId);
        Task<bool> SaveChangesAsync();

    }
}