using BackendTechnicalAssetsManagement.src.DTOs.Archive.LentItems;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    
    [Route("api/v1/archivelentitems")]
    [ApiController]
    [Authorize]
    public class ArchiveLentItemsController : ControllerBase
    {
        private readonly IArchiveLentItemsService _archiveLentItemsService;
        private readonly ILogger<ArchiveLentItemsController> _logger;
        public ArchiveLentItemsController(IArchiveLentItemsService archiveLentItemsService, ILogger<ArchiveLentItemsController> logger)
        {
            _archiveLentItemsService = archiveLentItemsService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ArchiveLentItemsDto>>>> GetAllLentItemsArchives()
        {
            var archivedLentItems = await _archiveLentItemsService.GetAllLentItemsArchivesAsync();
            var response = ApiResponse<IEnumerable<ArchiveLentItemsDto>>.SuccessResponse(archivedLentItems, "Archived lent items retrieved successfully.");
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ArchiveLentItemsDto?>>> GetLentItemsArchiveById(Guid id)
        {
            var archivedLentItems = await _archiveLentItemsService.GetLentItemsArchiveByIdAsync(id);
            var response = ApiResponse<ArchiveLentItemsDto>.SuccessResponse(archivedLentItems, "Archived lent items retrieved successfully.");
            return Ok(response);
        }
        [HttpDelete("restore/{id}")]
        public async Task<ActionResult<ApiResponse<ArchiveLentItemsDto>>> RestoreArchivedLentItems(Guid id)
        {
            var restoredLentItems = await _archiveLentItemsService.RestoreLentItemsAsync(id);
            if (restoredLentItems == null)
            {
                return NotFound(ApiResponse<ArchiveLentItemsDto>.FailResponse("Archived lent items not found."));
            }
            var response = ApiResponse<ArchiveLentItemsDto>.SuccessResponse(restoredLentItems, "Archived lent items restored successfully.");
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteLentItemsArchive(Guid id)
        {
            var success = await _archiveLentItemsService.DeleteLentItemsArchiveAsync(id);
            if (!success)
            {
                return NotFound(ApiResponse<object>.FailResponse("Archived lent items not found."));
            }
            return Ok(ApiResponse<object>.SuccessResponse(null, "Archived lent items deleted successfully."));
        }
    }
}

