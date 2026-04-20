using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Services;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalAssetManagementApi.Dtos.Item;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/v1/items")]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // POST: /api/v1/items
        [HttpPost]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<ItemDto>>> CreateItem([FromForm] CreateItemsDto createItemDto)
        {
            try
            {
                var newItemDto = await _itemService.CreateItemAsync(createItemDto);
                var response = ApiResponse<ItemDto>.SuccessResponse(newItemDto, "Item created successfully.");
                return CreatedAtAction(nameof(GetItemById), new { id = newItemDto.Id }, response);
            }
            catch (ItemService.DuplicateSerialNumberException ex)
            {
                return Conflict(ApiResponse<ItemDto>.FailResponse(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<ItemDto>.FailResponse(ex.Message));
            }
        }

        // GET: /api/v1/items
        [HttpGet]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ItemDto>>>> GetAllItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(ApiResponse<IEnumerable<ItemDto>>.SuccessResponse(items, "Items retrieved successfully."));
        }

        // GET: /api/v1/items/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<ApiResponse<ItemDto>>> GetItemById(Guid id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
                return NotFound(ApiResponse<ItemDto>.FailResponse("Item not found."));
            return Ok(ApiResponse<ItemDto>.SuccessResponse(item, "Item retrieved successfully."));
        }

        // GET: /api/v1/items/by-serial/{serialNumber}
        [HttpGet("by-serial/{serialNumber}")]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<ApiResponse<ItemDto>>> GetItemBySerialNumber(string serialNumber)
        {
            var item = await _itemService.GetItemBySerialNumberAsync(serialNumber);
            if (item == null)
                return NotFound(ApiResponse<ItemDto>.FailResponse("Item not found."));
            return Ok(ApiResponse<ItemDto>.SuccessResponse(item, "Item retrieved successfully."));
        }

        // POST: /api/v1/items/import
        /// <summary>
        /// Imports items from an Excel (.xlsx, .xls) or CSV file.
        /// Expected columns: SerialNumber, ItemName, ItemType, ItemModel, ItemMake, Description, Category, Condition, Image
        /// Access is restricted to users with 'Admin' or 'Staff' roles.
        /// </summary>
        [HttpPost("import")]
        [Consumes("multipart/form-data")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<ImportItemsResponseDto>>> ImportItemsFromExcel(IFormFile file)
        {
            // Comprehensive file validation (extension, MIME type, magic bytes)
            var (isValid, errorMessage) = await FileValidationUtils.ValidateImportFileAsync(file);
            if (!isValid)
            {
                return StatusCode(415, ApiResponse<ImportItemsResponseDto>.FailResponse(errorMessage!));
            }

            try
            {
                var result = await _itemService.ImportItemsFromExcelAsync(file);

                if (result.SuccessCount == 0)
                {
                    var message = result.FailureCount > 0
                        ? $"Import failed. No items were imported. {result.FailureCount} row(s) had errors."
                        : "Import failed. No valid items found in the file.";
                    return BadRequest(new ApiResponse<ImportItemsResponseDto>
                    {
                        Success = false,
                        Message = message,
                        Data = result,
                        Errors = result.Errors
                    });
                }

                return Ok(ApiResponse<ImportItemsResponseDto>.SuccessResponse(result,
                    $"Import completed. Success: {result.SuccessCount}, Failed: {result.FailureCount}"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ImportItemsResponseDto>.FailResponse(
                    $"An error occurred during the import process: {ex.Message}"));
            }
        }

        // PATCH: /api/v1/items/{id}
        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminOrStaff")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateItem(Guid id, [FromForm] UpdateItemsDto updateItemDto)
        {
            try
            {
                var success = await _itemService.UpdateItemAsync(id, updateItemDto);
                if (!success)
                    return NotFound(ApiResponse<object>.FailResponse("Update failed. Item not found."));
                return Ok(ApiResponse<object>.SuccessResponse(null, "Item updated successfully."));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<object>.FailResponse(ex.Message));
            }
        }

        // PATCH: /api/v1/items/rfid-scan/{rfidUid}
        // Called by IoT RFID scanner — toggles item status between Available and Borrowed
        [HttpPatch("rfid-scan/{rfidUid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<object>>> ScanRfid(string rfidUid)
        {
            var (success, errorMessage, newStatus) = await _itemService.ScanRfidAsync(rfidUid);
            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse(errorMessage);
                return errorMessage.Contains("No item") ? NotFound(errorResponse) : BadRequest(errorResponse);
            }
            return Ok(ApiResponse<object>.SuccessResponse(null, $"Item status toggled to '{newStatus}'."));
        }

        // GET: /api/v1/items/rfid/{rfidUid}
        // Called by ESP32 borrow scanner to resolve item by its RFID tag
        [HttpGet("rfid/{rfidUid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<ItemDto>>> GetByRfid(string rfidUid)
        {
            var item = await _itemService.GetItemByRfidUidAsync(rfidUid);
            if (item == null)
                return NotFound(ApiResponse<ItemDto>.FailResponse("No item registered to this RFID tag."));
            return Ok(ApiResponse<ItemDto>.SuccessResponse(item, "Item found."));
        }

        // POST: /api/v1/items/{id}/register-rfid
        // Called by ESP32 in registration mode — assigns a scanned RFID UID to the specified item
        [HttpPost("{id}/register-rfid")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<object>>> RegisterRfid(Guid id, [FromBody] RegisterRfidDto dto)
        {
            var (success, errorMessage) = await _itemService.RegisterRfidToItemAsync(id, dto.RfidUid);
            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse(errorMessage);
                return errorMessage.Contains("not found") ? NotFound(errorResponse) : Conflict(errorResponse);
            }
            return Ok(ApiResponse<object>.SuccessResponse(null, $"RFID '{dto.RfidUid}' registered to item successfully."));
        }

        // POST: /api/v1/items/{id}/update-location
        // Called by ESP32 location tracker — updates the physical location of an item
        [HttpPost("{id}/update-location")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<object>>> UpdateLocation(Guid id, [FromBody] UpdateLocationDto dto)
        {
            var (success, errorMessage) = await _itemService.UpdateItemLocationAsync(id, dto.Location);
            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse(errorMessage);
                return errorMessage.Contains("not found") ? NotFound(errorResponse) : BadRequest(errorResponse);
            }
            return Ok(ApiResponse<object>.SuccessResponse(null, $"Location updated to '{dto.Location}'."));
        }

        // DELETE: /api/v1/items/archive/{id}
        [HttpDelete("archive/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<object>>> ArchiveItem(Guid id)
        {
            var (success, errorMessage) = await _itemService.DeleteItemAsync(id);
            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse($"Archive failed. {errorMessage}");
                return errorMessage.Contains("not found") ? NotFound(errorResponse) : BadRequest(errorResponse);
            }
            return Ok(ApiResponse<object>.SuccessResponse(null, "Item Archived successfully."));
        }
    }
}
