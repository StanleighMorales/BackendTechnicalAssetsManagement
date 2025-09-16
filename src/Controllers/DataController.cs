using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataController : ControllerBase
    {
        private readonly IUserService _userService;

        public DataController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<BaseProfileDto>> GetMyProfile()
        {
            try
            {
                
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

                if (!Guid.TryParse(userIdString, out var userId))
                {
                    // This means the token's ID claim is not a valid integer.
                    return BadRequest("Invalid user ID format in token.");
                }

                var userProfile = await _userService.GetUserProfileByIdAsync(Guid.Parse(userIdString));

                return Ok(userProfile);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                // Log the exception ex
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
        [HttpGet("public-area")]
        [AllowAnonymous]
        public IActionResult GetPublicData()
        {
            return Ok(new { message = "This is the public lobby. Anyone can be here." });
        }
    }
}
