using BackendTechnicalAssetsManagement.src.DTOs.Statistics;

namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface ISummaryService
    {
        Task<ItemCount> GetItemCountAsync();
        Task<LentItemsCount> GetLentItemsCountAsync();
        Task<ActiveUserCount> GetActiveUserCountAsync();
        Task<SummaryDto> GetOverallSummaryAsync();
    }
}
