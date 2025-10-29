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
        Task<ItemDto?> GetItemBySerialNumberAsync(string serialNumber);
        Task<ItemDto> CreateItemAsync(CreateItemsDto createItemDto);
        Task<bool> UpdateItemAsync(Guid id, UpdateItemsDto updateItemDto);
        Task<bool> DeleteItemAsync(Guid id);

        /// <summary>
        /// Imports items from an Excel (.xlsx) file with automatic GUID and barcode generation.
        /// </summary>
        /// <param name="file">Excel file (.xlsx format only)</param>
        Task ImportItemsFromExcelAsync(IFormFile file);

    }
}