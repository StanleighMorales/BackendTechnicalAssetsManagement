using BackendTechnicalAssetsManagement.src.DTOs.Statistics;
using System.Threading.Tasks;

namespace BackendTechnicalAssetsManagement.src.IService
{
    /// <summary>
    /// Defines the contract for a service that provides statistical summaries for the application.
    /// This interface outlines the methods for retrieving various data aggregations and counts.
    /// </summary>
    public interface ISummaryService
    {
        /// <summary>
        /// Asynchronously retrieves a detailed summary of all items, categorized by their condition and category.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an
        /// <see cref="ItemCount"/> DTO with the detailed item breakdown.
        /// </returns>
        Task<ItemCount> GetItemCountAsync();

        /// <summary>
        /// Asynchronously retrieves a detailed summary of lent items, distinguishing between currently borrowed and returned items.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a
        /// <see cref="LentItemsCount"/> DTO with the lending summary.
        /// </returns>
        Task<LentItemsCount> GetLentItemsCountAsync();

        /// <summary>
        /// Asynchronously retrieves a detailed summary of active users, categorized by their specific roles.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an
        /// <see cref="ActiveUserCount"/> DTO with the active user breakdown.
        /// </returns>
        Task<ActiveUserCount> GetActiveUserCountAsync();

        /// <summary>
        /// Asynchronously retrieves a high-level, overall summary of the primary entities in the system.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a
        /// <see cref="SummaryDto"/> with the total counts of items, lent items, and active users.
        /// </returns>
        Task<SummaryDto> GetOverallSummaryAsync();
    }
}