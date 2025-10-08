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
        [HttpPost("import")]
        public async Task<IActionResult> ImportItemsFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(ApiResponse<object>.FailResponse("No file uploaded."));
            }

            try
            {
                await _itemService.ImportItemsFromExcelAsync(file);
                return Ok(ApiResponse<object>.SuccessResponse(null, "Items imported successfully."));
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here with a logging framework if you have one.
                return StatusCode(500, ApiResponse<object>.FailResponse($"An error occurred during the import process: {ex.Message}"));
            }
        }

        // PUT: /api/item/5
        [HttpPut("{id}")]
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
        [HttpDelete("archive{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteItem(Guid id)
        {
            var success = await _itemService.DeleteItemAsync(id);
            if (!success)
            {
                var errorResponse = ApiResponse<object>.FailResponse("Delete failed. Item not found.");
                return NotFound(errorResponse);
            }
            var successResponse = ApiResponse<object>.SuccessResponse(null, "Item deleted successfully.");
            return Ok(successResponse);
        }
    }
}