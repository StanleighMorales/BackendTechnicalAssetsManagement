using BackendTechnicalAssetsManagement.src.Interfaces.IServices;
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
        public async Task<ActionResult<GetUserProfileDto>> GetMyProfile()
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

                var userProfile = await _userService.GetUserProfileByIdAsync(int.Parse(userIdString));

                return Ok(userProfile);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
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
