using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using TechnicalAssetManagementApi.Dtos.Item;

namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task<ItemDto?> GetItemByIdAsync(Guid id);
        Task<ItemDto?> GetItemByBarcodeAsync(string barcode);
        Task<ItemDto> CreateItemAsync(CreateItemsDto createItemDto);
        Task<bool> UpdateItemAsync(Guid id, UpdateItemsDto updateItemDto);
        Task<bool> DeleteItemAsync(Guid id);

        Task ImportItemsFromExcelAsync(IFormFile file);

    }
}