using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.IService;
using Microsoft.AspNetCore.Mvc;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LentItemsController : ControllerBase
    {
        private readonly ILentItemsService _service;

        public LentItemsController(ILentItemsService service)
        {
            _service = service;
        }

        // GET: api/lentitems
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        // GET: api/lentitems/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // GET: api/lentitems/date/{dateTime}
        [HttpGet("date/{dateTime}")]
        public async Task<IActionResult> GetByDateTime(DateTime dateTime)
        {
            var item = await _service.GetByDateTimeAsync(dateTime);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/lentitems
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LentItems item)
        {
            var created = await _service.AddAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/lentitems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] LentItems item)
        {
            if (id != item.Id) return BadRequest("ID mismatch");

            await _service.UpdateAsync(item);
            return NoContent();
        }

        // DELETE (soft): api/lentitems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var success = await _service.SoftDeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
