using BackendTechnicalAssetsManagement.src.DTOs;
using BackendTechnicalAssetsManagement.src.DTOs.LentItems;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/v1/lentItems")]
    [Authorize(Policy = "AdminOrStaff")]
    public class LentItemsController : ControllerBase
    {
        private readonly ILentItemsService _service;

        public LentItemsController(ILentItemsService service)
        {
            _service = service;
        }

        // GET: api/v1/lentitems
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<LentItemsDto>>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            var response = ApiResponse<IEnumerable<LentItemsDto>>.SuccessResponse(items, "Items retrieved successfully.");
            return Ok(response);
        }

        // GET: api/v1/lentitems/{id}
        [HttpGet("{id}")]
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

        // GET: api/v1/lentitems/date/{dateTime}
        [HttpGet("date/{dateTime}")]
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

        // POST: api/v1/lentitems
        [HttpPost]
        public async Task<ActionResult<ApiResponse<LentItemsDto>>> Add([FromBody] CreateLentItemDto dto)
        {
            var created = await _service.AddAsync(dto);
            var response = ApiResponse<LentItemsDto>.SuccessResponse(created, "User - Item Listed Successfully.");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
        }
        [HttpPost("guests")]
        public async Task<ActionResult<ApiResponse<LentItemsDto>>> AddForGuest([FromBody] CreateLentItemsForGuestDto dto)
        {
            // You might want to add some validation here, e.g., if role is "Student", ensure StudentIdNumber is not null.
            if (dto.BorrowerRole.Equals("Student", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(dto.StudentIdNumber))
            {
                var badRequestResponse = ApiResponse<LentItemsDto>.FailResponse("Student ID number is required for students.");
                return BadRequest(badRequestResponse);
            }

            var created = await _service.AddForGuestAsync(dto);
            // You can still use GetById to retrieve the newly created item
            var response = ApiResponse<LentItemsDto>.SuccessResponse(created, "Guest - Item Listed Successfully.");
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
        }

        // PATCH: api/v1/lentitems/{id}
        [HttpPatch("{id}")]
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

        // DELETE (soft): api/v1/lentitems/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> SoftDelete(Guid id)
        {
            var success = await _service.SoftDeleteAsync(id);
            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse("Soft delete failed. Item not found.");
                return NotFound(errorResponse); // Or BadRequest("Soft delete failed");
            }
            var successResponse = ApiResponse<object>.SuccessResponse(null, "Item soft-deleted successfully.");
            return Ok(successResponse);
        }
    }
}
