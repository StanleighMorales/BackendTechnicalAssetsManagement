using BackendTechnicalAssetsManagement.src.DTOs.Item;
using TechnicalAssetManagementApi.Dtos.Item;

namespace BackendTechnicalAssetsManagement.src.Interfaces.IService
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task<ItemDto?> GetItemByIdAsync(Guid id);
        Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto); // Consistent naming
        Task<bool> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto);
        Task<bool> DeleteItemAsync(Guid id);
    }
}