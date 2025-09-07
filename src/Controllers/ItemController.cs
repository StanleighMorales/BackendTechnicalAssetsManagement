using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Services;
using Microsoft.AspNetCore.Mvc;
using TechnicalAssetManagementApi.Dtos.Item;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: /api/item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        // GET: /api/item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemById(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST: /api/item
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem([FromForm] CreateItemDto createItemDto)
        {
            try
            {
                var newItemDto = await _itemService.CreateItemAsync(createItemDto);
                return CreatedAtAction(nameof(GetItemById), new { id = newItemDto.Id }, newItemDto);
            }
            catch (ItemService.DuplicateSerialNumberException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: /api/item/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromForm] UpdateItemDto updateItemDto)
        {
            try
            {
                var success = await _itemService.UpdateItemAsync(id, updateItemDto);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent(); // Standard response for a successful update
            }
            catch (ArgumentException ex) // Catch invalid file uploads on update
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: /api/item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var success = await _itemService.DeleteItemAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent(); // Standard response for a successful delete
        }
    }
}