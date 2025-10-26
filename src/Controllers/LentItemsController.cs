using BackendTechnicalAssetsManagement.src.DTOs;
using BackendTechnicalAssetsManagement.src.DTOs.LentItems;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Services;
using BackendTechnicalAssetsManagement.src.Utils;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/v1/lentItems")]
    [Authorize]
    public class LentItemsController : ControllerBase
    {
        private readonly ILentItemsService _service;

        public LentItemsController(ILentItemsService service)
        {
            _service = service;
        }
        // POST: api/v1/lentitems
        [HttpPost]
        public async Task<ActionResult<ApiResponse<LentItemsDto>>> Add([FromBody] CreateLentItemDto dto)
        {
            var created = await _service.AddAsync(dto);
            var response = ApiResponse<LentItemsDto>.SuccessResponse(created, "User - Item Listed Successfully.");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
        }

        [HttpPost("guests")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<LentItemsDto>>> AddForGuest([FromBody] CreateLentItemsForGuestDto dto)
        {
            // You might want to add some validation here, e.g., if role is "Student", ensure StudentIdNumber is not null.
            if (dto.BorrowerRole != null && dto.BorrowerRole.Equals("Student", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(dto.StudentIdNumber))
            {
                var badRequestResponse = ApiResponse<LentItemsDto>.FailResponse("Student ID number is required for students.");
                return BadRequest(badRequestResponse);
            }

            var created = await _service.AddForGuestAsync(dto);
            // You can still use GetById to retrieve the newly created item
            var response = ApiResponse<LentItemsDto>.SuccessResponse(created, "Guest - Item Listed Successfully.");
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
        }

       

        // GET: api/v1/lentitems/date/{dateTime}
        [HttpGet("date/{dateTime}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<LentItemsDto>>> GetByDateTime(DateTime dateTime)
        {
            var item = await _service.GetByDateTimeAsync(dateTime);
            if (item == null)
            {
                var errorResponse = ApiResponse<LentItemsDto>.FailResponse("Item not found for the specified date and time.");
                return NotFound(errorResponse);
            }
            var successResponse = ApiResponse<LentItemsDto>.SuccessResponse(item, "Item retrieved successfully.");
            return Ok(successResponse);
        }
        // GET: api/v1/lentitems
        [HttpGet]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<IEnumerable<LentItemsDto>>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            var response = ApiResponse<IEnumerable<LentItemsDto>>.SuccessResponse(items, "Items retrieved successfully.");
            return Ok(response);
        }

        // GET: api/v1/lentitems/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<LentItemsDto>>> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
            {
                var errorResponse = ApiResponse<LentItemsDto>.FailResponse("Item not found.");
                return NotFound(errorResponse);
            }
            var successResponse = ApiResponse<LentItemsDto>.SuccessResponse(item, "Item retrieved successfully.");
            return Ok(successResponse);
        }

        // PATCH: api/v1/lentitems/{id}
        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> Update(Guid id, [FromBody] UpdateLentItemDto dto)
        {
            // The old "ID mismatch" check is no longer needed if you removed Id from the DTO.
            // The 'id' from the URL is now the single source of truth.

            var success = await _service.UpdateAsync(id, dto);

            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse("Update failed. Item not found or no changes made.");
                return NotFound(errorResponse); // Or BadRequest("Update failed");
            }

            // Return NoContent for a successful update, or you could return the updated object.
            var successResponse = ApiResponse<object>.SuccessResponse(null, "Item updated successfully.");
            return Ok(successResponse); 
        }
        [HttpPatch("scan/updateStatus{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateStatus(Guid id, [FromBody] ScanLentItemDto dto)
        {
            var success = await _service.UpdateStatusAsync(id, dto);
            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse("Status update failed. Item not found or no changes made.");
                return NotFound(errorResponse);
            }
            var successResponse = ApiResponse<object>.SuccessResponse(null, "Item status updated successfully.");
            return Ok(successResponse);
        }

        [HttpPatch("hide/{lentItemId}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<object>>> HideFromHistory(Guid lentItemId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // 1. Service verifies: Does the LentItems record exist AND does it belong to this userId?
            var success = await _service.UpdateHistoryVisibility(lentItemId, userId, true);

            if (!success)
            {
                return NotFound(ApiResponse<object>.FailResponse("Item not found or not authorized."));
            }

            return Ok(ApiResponse<object>.SuccessResponse(null, "Item hidden from history."));
        }
        [HttpDelete("archive/{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> ArchiveLentItems(Guid id)
        {
            var success = await _service.ArchiveLentItems(id);
            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse("archive failed. Item not found.");
                return NotFound(errorResponse); // Or BadRequest("Soft delete failed");
            }
            var successResponse = ApiResponse<object>.SuccessResponse(null, "Item archived successfully.");
            return Ok(successResponse);
        }
    }
}
