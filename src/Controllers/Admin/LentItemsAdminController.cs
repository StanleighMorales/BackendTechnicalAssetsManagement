using BackendTechnicalAssetsManagement.src.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/v1/admin/lentItemsAdmin")]
    [Authorize(Roles = "Admin")]
    public class LentItemsAdminController : ControllerBase
    {
        private readonly ILentItemsService _service;

        public LentItemsAdminController(ILentItemsService service)
        {
            _service = service;
        }

        // GET: api/admin/lentitems
        [HttpGet]
        public async Task<IActionResult> GetAllIncludingDeleted()
        {
            var items = await _service.GetAllIncludingDeletedAsync();
            return Ok(items);
        }

        // GET: api/admin/lentitems/deleted
        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeleted()
        {
            var items = await _service.GetDeletedAsync();
            return Ok(items);
        }

        // GET: api/admin/lentitems/deleted/{id}
        [HttpGet("deleted/{id}")]
        public async Task<IActionResult> GetDeletedById(Guid id)
        {
            var item = await _service.GetDeletedByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // PATCH: api/admin/lentitems/restore/{id}
        [HttpPatch("restore/{id}")]
        public async Task<IActionResult> Restore(Guid id)
        {
            var success = await _service.RestoreAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        // DELETE (hard): api/admin/lentitems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> PermaDelete(Guid id)
        {
            var success = await _service.PermaDeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

