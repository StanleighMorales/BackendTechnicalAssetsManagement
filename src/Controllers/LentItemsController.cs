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
        // POST: api/v1/lentItems — instant borrow via RFID scan, status set to Borrowed by backend
        // AllowAnonymous so the ESP32 borrow station can submit without a JWT
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<LentItemsDto>>> Borrow([FromBody] CreateBorrowDto dto)
        {
            var created = await _service.AddBorrowAsync(dto);
            var response = ApiResponse<LentItemsDto>.SuccessResponse(created, "Item borrowed successfully.");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
        }

        /// <summary>
        /// Future reservation — item will be picked up at a scheduled time.
        /// ReservedFor is required and must be in the future.
        /// Status is set to Pending by the backend.
        /// If userId is not provided in the request, the authenticated user's ID is used automatically.
        /// </summary>
        [HttpPost("reserve")]
        public async Task<ActionResult<ApiResponse<LentItemsDto>>> Reserve([FromBody] CreateReservationDto dto)
        {
            // If userId is not provided, use the authenticated user's ID
            if (!dto.UserId.HasValue && !dto.TeacherId.HasValue)
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (Guid.TryParse(userIdClaim, out var authenticatedUserId))
                {
                    dto.UserId = authenticatedUserId;
                }
            }

            var created = await _service.AddReservationAsync(dto);
            var response = ApiResponse<LentItemsDto>.SuccessResponse(created, "Reservation created successfully.");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
        }

        [HttpPost("guests")]
        [Authorize(Policy = "AdminOrStaff")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponse<LentItemsDto>>> AddForGuest([FromForm] CreateLentItemsForGuestDto dto)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out var issuedById))
                return Unauthorized(ApiResponse<LentItemsDto>.FailResponse("User not authenticated."));

            var created = await _service.AddForGuestAsync(dto, issuedById);
            var response = ApiResponse<LentItemsDto>.SuccessResponse(created, "Guest - Item Listed Successfully.");
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
        }

       

        // GET: api/v1/lentitems/date/{dateTime}
        [HttpGet("date/{dateTime}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<IEnumerable<LentItemsDto>>>> GetByDateTime(string dateTime)
        {
            try
            {
                // Parse the date string manually to handle different formats
                if (!DateTime.TryParse(dateTime, out DateTime parsedDateTime))
                {
                    var badRequestResponse = ApiResponse<IEnumerable<LentItemsDto>>.FailResponse($"Invalid date format: '{dateTime}'. Use formats like '2025-11-01' or '2025-11-01T12:30:00'.");
                    return BadRequest(badRequestResponse);
                }

                // Convert to UTC for database comparison since LentAt is stored in UTC
                var utcDateTime = DateTime.SpecifyKind(parsedDateTime, DateTimeKind.Utc);
                
                var items = await _service.GetByDateTimeAsync(utcDateTime);
                if (items == null || !items.Any())
                {
                    // Add debug information to help troubleshoot
                    var debugInfo = $"No items found for date: {dateTime} (parsed as UTC: {utcDateTime:yyyy-MM-dd HH:mm:ss.fff})";
                    var errorResponse = ApiResponse<IEnumerable<LentItemsDto>>.FailResponse(debugInfo);
                    return NotFound(errorResponse);
                }
                var successResponse = ApiResponse<IEnumerable<LentItemsDto>>.SuccessResponse(items, $"Found {items.Count()} items for the specified date and time.");
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponse<IEnumerable<LentItemsDto>>.FailResponse($"Error processing request: {ex.Message}");
                return BadRequest(errorResponse);
            }
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

        // GET: api/v1/lentitems/borrowed
        // AllowAnonymous so the ESP32 return station can query without a JWT
        [HttpGet("borrowed")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<LentItemsDto>>>> GetAllBorrowedItems()
        {
            var items = await _service.GetAllBorrowedItemsAsync();
            var response = ApiResponse<IEnumerable<LentItemsDto>>.SuccessResponse(items, $"Retrieved {items.Count()} borrowed item records.");
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
        // AllowAnonymous so the ESP32 return station can patch without a JWT
        [HttpPatch("{id}")]
        [AllowAnonymous]
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

        [HttpPatch("hide/{lent-item-id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<object>>> HideFromHistory(Guid lentItemId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new ApiResponse<object> { Success = false, Message = "User not authenticated" });
            
            var userId = Guid.Parse(userIdClaim);

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
            var (success, errorMessage) = await _service.ArchiveLentItems(id);
            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse($"Archive failed. {errorMessage}");
                return errorMessage.Contains("not found") ? NotFound(errorResponse) : BadRequest(errorResponse);
            }
            var successResponse = ApiResponse<object>.SuccessResponse(null, "Item archived successfully.");
            return Ok(successResponse);
        }

        /// <summary>
        /// Allows students to cancel their own Pending or Approved reservations.
        /// Cannot cancel if item is already Borrowed, Returned, Denied, Expired, or Canceled.
        /// </summary>
        [HttpPatch("cancel/{id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<object>>> CancelReservation(Guid id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(ApiResponse<object>.FailResponse("User not authenticated."));
            }

            var (success, errorMessage) = await _service.CancelReservationAsync(id, userId);
            
            if (!success)
            {
                if (errorMessage.Contains("not found"))
                    return NotFound(ApiResponse<object>.FailResponse(errorMessage));
                if (errorMessage.Contains("not authorized"))
                    return StatusCode(403, ApiResponse<object>.FailResponse(errorMessage));
                
                return BadRequest(ApiResponse<object>.FailResponse(errorMessage));
            }

            return Ok(ApiResponse<object>.SuccessResponse(null, "Reservation canceled successfully."));
        }
    }
}
