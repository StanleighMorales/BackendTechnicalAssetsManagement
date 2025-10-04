using BackendTechnicalAssetsManagement.src.DTOs;
using BackendTechnicalAssetsManagement.src.DTOs.LentItems;
using BackendTechnicalAssetsManagement.src.IService;
using Microsoft.AspNetCore.Mvc;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/v1/lentItems")]
    public class LentItemsController : ControllerBase
    {
        private readonly ILentItemsService _service;

        public LentItemsController(ILentItemsService service)
        {
            _service = service;
        }

        // GET: api/v1/lentitems
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        // GET: api/v1/lentitems/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // GET: api/v1/lentitems/date/{dateTime}
        [HttpGet("date/{dateTime}")]
        public async Task<IActionResult> GetByDateTime(DateTime dateTime)
        {
            var item = await _service.GetByDateTimeAsync(dateTime);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/v1/lentitems
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateLentItemDto dto)
        {
            var created = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [HttpPost("guests")]
        public async Task<IActionResult> AddForGuest([FromBody] CreateLentItemsForGuestDto dto)
        {
            // You might want to add some validation here, e.g., if role is "Student", ensure StudentIdNumber is not null.
            if (dto.BorrowerRole.Equals("Student", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(dto.StudentIdNumber))
            {
                return BadRequest("Student ID number is required for students.");
            }

            var created = await _service.AddForGuestAsync(dto);
            // You can still use GetById to retrieve the newly created item
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/v1/lentitems/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLentItemDto dto)
        {
            // The old "ID mismatch" check is no longer needed if you removed Id from the DTO.
            // The 'id' from the URL is now the single source of truth.

            var success = await _service.UpdateAsync(id, dto);

            if (!success)
            {
                return NotFound(); // Or BadRequest("Update failed");
            }

            // Return NoContent for a successful update, or you could return the updated object.
            return NoContent();
        }

        // DELETE (soft): api/v1/lentitems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var success = await _service.SoftDeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
