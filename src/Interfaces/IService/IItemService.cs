using BackendTechnicalAssetsManagement.Model;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Items;

namespace BackendTechnicalAssetsManagement.src.Interfaces.IServices
{
    public interface IItemService
    {
        Task<Item> CreateItemAsync(CreateItemDto request);
        Task<List<Item>> GetItemsAsync(int pageNumber, int pageSize);
        Task<Item?> GetItemByIdAsync(int id);
        Task<List<Item>> GetAllItems();
    }
}
