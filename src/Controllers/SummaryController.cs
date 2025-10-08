using BackendTechnicalAssetsManagement.src.DTOs.Statistics;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/summary")]
    [Authorize(Policy = "AdminOrStaff")]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        // Our service is "injected" into the controller's constructor.
        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<SummaryDto>>> GetOverallSummary()
        {
            var summary = await _summaryService.GetOverallSummaryAsync();
            var response = ApiResponse<SummaryDto>.SuccessResponse(summary, "Overall summary retrieved successfully.");
            return Ok(response);
        }

        // Endpoint #1: Gets the item counts summary
        // URL: GET api/summary/items
        [HttpGet("items")]
        public async Task<ActionResult<ApiResponse<ItemCount>>> GetItemSummary()
        {
            var summary = await _summaryService.GetItemCountAsync();
            var response = ApiResponse<ItemCount>.SuccessResponse(summary, "Item summary retrieved successfully.");
            return Ok(response);
        }

        // Endpoint #2: Gets the lent items summary
        // URL: GET api/summary/lent-items
        [HttpGet("lent-items")]
        public async Task<ActionResult<ApiResponse<LentItemsCount>>> GetLentSummary()
        {
            var summary = await _summaryService.GetLentItemsCountAsync();
            var response = ApiResponse<LentItemsCount>.SuccessResponse(summary, "Lent items summary retrieved successfully.");
            return Ok(response);
        }

        // Endpoint #3: Gets the active users summary
        // URL: GET api/summary/users
        [HttpGet("users")]
        public async Task<ActionResult<ApiResponse<ActiveUserCount>>> GetUserSummary()
        {
            var summary = await _summaryService.GetActiveUserCountAsync();
            var response = ApiResponse<ActiveUserCount>.SuccessResponse(summary, "Active user summary retrieved successfully.");
            return Ok(response);
        }
        

    }
}
