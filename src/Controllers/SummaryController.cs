using BackendTechnicalAssetsManagement.src.DTOs.Statistics;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    /// <summary>
    /// API controller for retrieving statistical summaries and dashboard data.
    /// All endpoints in this controller require authorization for users who are Admins or Staff.
    /// </summary>
    [ApiController]
    [Route("api/v1/summary")]
    [Authorize(Policy = "AdminOrStaff")]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryController"/> class.
        /// </summary>
        /// <param name="summaryService">The summary service injected via dependency injection.</param>
        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        /// <summary>
        /// Retrieves a high-level, overall summary of the system's core entities.
        /// </summary>
        /// <remarks>
        /// This endpoint provides the total counts for items, lending transactions, and active users.
        /// <br/>
        /// **Endpoint:** `GET /api/summary`
        /// </remarks>
        /// <returns>An ApiResponse containing the overall summary data.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<SummaryDto>>> GetOverallSummary()
        {
            var summary = await _summaryService.GetOverallSummaryAsync();
            var response = ApiResponse<SummaryDto>.SuccessResponse(summary, "Overall summary retrieved successfully.");
            return Ok(response);
        }
        #region detailed summary
        // /// <summary>
        // /// Retrieves a detailed summary of all items, categorized by condition and type.
        // /// </summary>
        // /// <remarks>
        // /// **Endpoint:** `GET /api/summary/items`
        // /// </remarks>
        // /// <returns>An ApiResponse containing the detailed item summary.</returns>
        // [HttpGet("items")]
        // public async Task<ActionResult<ApiResponse<ItemCount>>> GetItemSummary()
        // {
        //     var summary = await _summaryService.GetItemCountAsync();
        //     var response = ApiResponse<ItemCount>.SuccessResponse(summary, "Item summary retrieved successfully.");
        //     return Ok(response);
        // }

        // /// <summary>
        // /// Retrieves a detailed summary of lent items, including currently lent and returned counts.
        // /// </summary>
        // /// <remarks>
        // /// **Endpoint:** `GET /api/summary/lent-items`
        // /// </remarks>
        // /// <returns>An ApiResponse containing the detailed lending summary.</returns>
        // [HttpGet("lent-items")]
        // public async Task<ActionResult<ApiResponse<LentItemsCount>>> GetLentSummary()
        // {
        //     var summary = await _summaryService.GetLentItemsCountAsync();
        //     var response = ApiResponse<LentItemsCount>.SuccessResponse(summary, "Lent items summary retrieved successfully.");
        //     return Ok(response);
        // }

        // /// <summary>
        // /// Retrieves a detailed summary of all active users, categorized by role.
        // /// </summary>
        // /// <remarks>
        // /// **Endpoint:** `GET /api/summary/users`
        // /// </remarks>
        // /// <returns>An ApiResponse containing the detailed active user summary.</returns>
        // [HttpGet("users")]
        // public async Task<ActionResult<ApiResponse<ActiveUserCount>>> GetUserSummary()
        // {
        //     var summary = await _summaryService.GetActiveUserCountAsync();
        //     var response = ApiResponse<ActiveUserCount>.SuccessResponse(summary, "Active user summary retrieved successfully.");
        //     return Ok(response);
        // }
        #endregion
    }
}