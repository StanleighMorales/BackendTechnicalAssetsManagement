using BackendTechnicalAssetsManagement.src.DTOs.Archive;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [Route("api/v1/archiveitems")]
    [ApiController]
    public class ArchiveItemsController : ControllerBase
    {
        private readonly IArchiveItemsService _archiveItemsService;
        private readonly ILogger<ArchiveItemsController> _logger;
        public ArchiveItemsController(IArchiveItemsService archiveItemsService, ILogger<ArchiveItemsController> logger)
        {
            _archiveItemsService = archiveItemsService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ArchiveItemsDto>>> ArchiveItem([FromBody] CreateArchiveItemsDto createArchiveItemsDto)
        {
            var archivedItem = await _archiveItemsService.CreateItemArchiveAsync(createArchiveItemsDto);
            var response = ApiResponse<ArchiveItemsDto>.SuccessResponse(archivedItem, "Item archived successfully.");
            return Ok(response);
        }
        /// <summary>
        /// Retrieves all archived items.
        /// </summary>
        /// <returns>A list of archived items.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ArchiveItemsDto>>>> GetAllItemArchives()
        {
            var archivedItems = await _archiveItemsService.GetAllItemArchivesAsync();
            var response = ApiResponse<IEnumerable<ArchiveItemsDto>>.SuccessResponse(archivedItems, "Archived items retrieved successfully.");
            return Ok(response);
        }
        /// <summary>
        /// Retrieves a specific archived item by its ID.
        /// </summary>
        /// <param name="id">The ID of the archived item to retrieve.</param>
        /// <returns>The requested archived item.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ArchiveItemsDto?>>> GetArchivedItemById(Guid id)
        {
            var archivedItem = await _archiveItemsService.GetItemArchiveByIdAsync(id);
            var response = ApiResponse<ArchiveItemsDto>.SuccessResponse(archivedItem, "Archived item retrieved successfully.");
            return Ok(response);
        }
        /// <summary>
        /// Restores an archived item back to the main items table.
        /// </summary>
        /// <param name="id">The ID of the archived item to restore.</param>
        /// <returns>The newly restored item.</returns>
        [HttpPost("restore/{id}")]
        public async Task<ActionResult<ApiResponse<ItemDto>>> RestoreArchivedItem(Guid id)
        {
            var restoredItem = await _archiveItemsService.RestoreItemAsync(id);

            if (restoredItem == null)
            {
                return NotFound(ApiResponse<ItemDto>.FailResponse("Archived item not found."));
            }

            var response = ApiResponse<ItemDto>.SuccessResponse(restoredItem, "Item restored successfully.");
            return Ok(response);
        }
        /// <summary>
        /// Deletes a specific archived item by its ID.
        /// </summary>
        /// <param name="id">The ID of the archived item to delete.</param>
        /// <returns>A confirmation message upon successful deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteArchivedItem(Guid id)
        {
            await _archiveItemsService.DeleteItemArchiveAsync(id);
            var response = ApiResponse<string>.SuccessResponse(null, "Archived item deleted successfully.");
            return Ok(response);
        }
        
    }
}
