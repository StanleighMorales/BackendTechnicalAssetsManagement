using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Archive;
using BackendTechnicalAssetsManagement.src.DTOs.Item;

namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IArchiveItemsService
    {
        Task<ArchiveItemsDto> CreateItemArchiveAsync(CreateArchiveItemsDto createItemArchive);
        Task<IEnumerable<ArchiveItemsDto>> GetAllItemArchivesAsync();
        Task<ArchiveItemsDto?> GetItemArchiveByIdAsync(Guid id);
        Task<bool> UpdateItemArchiveAsync(Guid id, UpdateArchiveItemsDto UpdateItemArchive);
        Task <bool> DeleteItemArchiveAsync(Guid id);
        Task<ItemDto?> RestoreItemAsync(Guid archiveId);
        Task<bool> SaveChangesAsync();
    }
}
