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
    [Authorize(Policy = "AdminOrStaff")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        // POST: /api/item
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ItemDto>>> CreateItem([FromForm] CreateItemsDto createItemDto)
        {
            try
            {
                var newItemDto = await _itemService.CreateItemAsync(createItemDto);
                var reponse = ApiResponse<ItemDto>.SuccessResponse(newItemDto, "Item created successfully.");
                return CreatedAtAction(nameof(GetItemById), new { id = newItemDto.Id }, reponse);
            }
            catch (ItemService.DuplicateSerialNumberException ex)
            {
                var errorResponse = ApiResponse<ItemDto>.FailResponse(ex.Message);
                return Conflict(errorResponse);
            }
            catch (ArgumentException ex)
            {
                var response = ApiResponse<ItemDto>.FailResponse(ex.Message);
                return BadRequest(response);
            }
        }


        // GET: /api/item
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ItemDto>>>> GetAllItems()
        {
            
            var items = await _itemService.GetAllItemsAsync();
            var response = ApiResponse<IEnumerable<ItemDto>>.SuccessResponse(items, "Items retrieved successfully.");
            return Ok(response);
        }

        // GET: /api/item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ItemDto>>> GetItemById(Guid id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                var errorResponse = ApiResponse<ItemDto>.FailResponse("Item not found.");
                return NotFound(errorResponse);
            }
            var successResponse = ApiResponse<ItemDto>.SuccessResponse(item, "Item retrieved successfully.");
            return Ok(successResponse);
        }
        [HttpGet("by-barcode/{barcode}")]
        public async Task<ActionResult<ApiResponse<ItemDto>>> GetItemByBarcode(string barcodeText)
        {
            var item = await _itemService.GetItemByBarcodeAsync(barcodeText);
            if (item == null)
            {
                var errorResponse = ApiResponse<ItemDto>.FailResponse("Item not found.");
                return NotFound(errorResponse);
            }
            var successResponse = ApiResponse<ItemDto>.SuccessResponse(item, "Item retrieved successfully.");
            return Ok(successResponse);
        }

        [HttpGet("by-serial/{serialNumber}")]
        public async Task<ActionResult<ApiResponse<ItemDto>>> GetItemBySerialNumber(string serialNumber)
        {
            var item = await _itemService.GetItemBySerialNumberAsync(serialNumber);
            if (item == null)
            {
                var errorResponse = ApiResponse<ItemDto>.FailResponse("Item not found.");
                return NotFound(errorResponse);
            }
            var successResponse = ApiResponse<ItemDto>.SuccessResponse(item, "Item retrieved successfully.");
            return Ok(successResponse);
        }
        [HttpPost("import")]
        public async Task<ActionResult<ApiResponse<ImportItemsResponseDto>>> ImportItemsFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(ApiResponse<ImportItemsResponseDto>.FailResponse("No file uploaded."));
            }

            // Validate file extension - only accept .xlsx files
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (fileExtension != ".xlsx")
            {
                return BadRequest(ApiResponse<ImportItemsResponseDto>.FailResponse("Only .xlsx files are allowed for import."));
            }

            // Validate MIME type as additional security
            var allowedMimeTypes = new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
            if (!allowedMimeTypes.Contains(file.ContentType))
            {
                return BadRequest(ApiResponse<ImportItemsResponseDto>.FailResponse("Invalid file type. Only Excel (.xlsx) files are allowed."));
            }

            try
            {
                var result = await _itemService.ImportItemsFromExcelAsync(file);
                
                // Check if no items were imported
                if (result.SuccessCount == 0)
                {
                    var message = result.FailureCount > 0 
                        ? $"Import failed. No items were imported. {result.FailureCount} row(s) had errors."
                        : "Import failed. No valid items found in the file.";
                    
                    // Return the result data even on failure so frontend can show details
                    return BadRequest(new ApiResponse<ImportItemsResponseDto>
                    {
                        Success = false,
                        Message = message,
                        Data = result,
                        Errors = result.Errors
                    });
                }
                
                var successMessage = $"Import completed. Success: {result.SuccessCount}, Failed: {result.FailureCount}";
                return Ok(ApiResponse<ImportItemsResponseDto>.SuccessResponse(result, successMessage));
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here with a logging framework if you have one.
                return StatusCode(500, ApiResponse<ImportItemsResponseDto>.FailResponse($"An error occurred during the import process: {ex.Message}"));
            }
        }

        

        // PUT: /api/item/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateItem(Guid id, [FromForm] UpdateItemsDto updateItemDto)
        {
            try
            {
                var success = await _itemService.UpdateItemAsync(id, updateItemDto);
                if (!success)
                {
                    var errorResponse = ApiResponse<object>.FailResponse("Update failed. Item not found.");
                    return NotFound(errorResponse);
                }
                var successResponse = ApiResponse<object>.SuccessResponse(null, "Item updated successfully.");
                return Ok(successResponse);
            }
            catch (ArgumentException ex) // Catch invalid file uploads on update
            {
                var errorResponse = ApiResponse<object>.FailResponse(ex.Message);
                return BadRequest(errorResponse);
            }
        }

        // DELETE: /api/item/5
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
            var successResponse = ApiResponse<object>.SuccessResponse(null, "Item Archived successfully.");
            return Ok(successResponse);
        }
    }
}