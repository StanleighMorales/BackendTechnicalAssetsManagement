using BackendTechnicalAssetsManagement.src.DTOs;
using BackendTechnicalAssetsManagement.src.DTOs.LentItems;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Services;
using BackendTechnicalAssetsManagement.src.Utils;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZXing.QrCode.Internal;
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

        // GET: api/v1/lentitems/barcode/{barcode}
        [HttpGet("barcode/{barcode}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<LentItemsDto>>> GetByBarcode(string barcode)
        {
            const string prefix = "LENT-";

            // Validate that the barcode starts with the expected prefix and follows the correct format
            if (!barcode.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(ApiResponse<LentItemsDto>.FailResponse("Invalid barcode format. Expected format: LENT-YYYYMMDD-XXX"));
            }

            // Additional validation for the complete format: LENT-YYYYMMDD-XXX
            if (barcode.Length != 17 || !System.Text.RegularExpressions.Regex.IsMatch(barcode, @"^LENT-\d{8}-\d{3}$"))
            {
                return BadRequest(ApiResponse<LentItemsDto>.FailResponse("Invalid barcode format. Expected format: LENT-YYYYMMDD-XXX"));
            }

            var item = await _service.GetByBarcodeAsync(barcode);
            if (item == null)
            {
                var errorResponse = ApiResponse<LentItemsDto>.FailResponse("Lent item not found.");
                return NotFound(errorResponse);
            }
            var successResponse = ApiResponse<LentItemsDto>.SuccessResponse(item, "Lent item retrieved successfully.");
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
        [HttpPatch("scan/{barcode}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateStatus(string barcode, [FromBody] ScanLentItemDto dto)
        {
            const string prefix = "LENT-";

            // Validate that the barcode starts with the expected prefix and follows the correct format
            if (!barcode.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(ApiResponse<object>.FailResponse("Invalid barcode format. Expected format: LENT-YYYYMMDD-XXX"));
            }

            // Additional validation for the complete format: LENT-YYYYMMDD-XXX
            if (barcode.Length != 17 || !System.Text.RegularExpressions.Regex.IsMatch(barcode, @"^LENT-\d{8}-\d{3}$"))
            {
                return BadRequest(ApiResponse<object>.FailResponse("Invalid barcode format. Expected format: LENT-YYYYMMDD-XXX"));
            }

            // Find the lent item by barcode
            var success = await _service.UpdateStatusByBarcodeAsync(barcode, dto);

            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse("Lent item not found or update failed.");
                return NotFound(errorResponse);
            }

            var successResponse = ApiResponse<object>.SuccessResponse(null, "Status updated successfully.");
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
        [HttpPatch("return/item/{itemBarcode}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> ReturnItemByItemBarcode(string itemBarcode)
        {
            var success = await _service.ReturnItemByItemBarcodeAsync(itemBarcode);
            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse("Return failed. Item not found, not currently lent, or already returned.");
                return NotFound(errorResponse);
            }
            var successResponse = ApiResponse<object>.SuccessResponse(null, "Item returned successfully.");
            return Ok(successResponse);
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
    }
}
