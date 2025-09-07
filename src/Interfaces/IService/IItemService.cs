using BackendTechnicalAssetsManagement.src.DTOs.Item;
using TechnicalAssetManagementApi.Dtos.Item;

namespace BackendTechnicalAssetsManagement.src.Interfaces.IService
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task<ItemDto?> GetItemByIdAsync(int id);
        Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto); // Consistent naming
        Task<bool> UpdateItemAsync(int id, UpdateItemDto updateItemDto);
        Task<bool> DeleteItemAsync(int id);
    }
}