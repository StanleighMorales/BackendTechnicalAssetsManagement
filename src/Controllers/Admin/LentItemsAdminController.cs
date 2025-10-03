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

