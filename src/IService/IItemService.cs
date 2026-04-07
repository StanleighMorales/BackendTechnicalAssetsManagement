using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using TechnicalAssetManagementApi.Dtos.Item;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

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
        Task<(bool Success, string ErrorMessage)> PatchItemStatusAsync(string barcode, ItemStatus status);
        Task<(bool Success, string ErrorMessage, ItemStatus? NewStatus)> ScanRfidAsync(string rfidUid);
        Task<(bool Success, string ErrorMessage)> RegisterRfidToItemAsync(Guid itemId, string rfidUid);
        Task<(bool Success, string ErrorMessage)> UpdateItemLocationAsync(Guid itemId, string location);
        Task<ItemDto?> GetItemByRfidUidAsync(string rfidUid);
        Task<(bool Success, string ErrorMessage)> DeleteItemAsync(Guid id);

        /// <summary>
        /// Imports items from an Excel (.xlsx) file with automatic GUID and barcode generation.
        /// </summary>
        /// <param name="file">Excel file (.xlsx format only)</param>
        /// <returns>Import results with success/failure counts and error details</returns>
        Task<ImportItemsResponseDto> ImportItemsFromExcelAsync(IFormFile file);

    }
}