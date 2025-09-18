using AutoMapper;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Interfaces.IRepository;
using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Models;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static BackendTechnicalAssetsManagement.src.DTOs.User.UserProfileDtos;
using static BackendTechnicalAssetsManagement.src.Models.Enums;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IUserRepository userRepository, IMapper mapper)
        {
            _userService = userService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //[HttpGet("me")]
        //public async Task<ActionResult<BaseProfileDto>> GetMyProfile()
        //{
        //    try
        //    {
                
        //        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        //        if (!Guid.TryParse(userIdString, out var userId))
        //        {
        //            // This means the token's ID claim is not a valid integer.
        //            return BadRequest("Invalid user ID format in token.");
        //        }

        //        var userProfile = await _userService.GetUserProfileByIdAsync(Guid.Parse(userIdString));

        //        return Ok(userProfile);
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(new { message = ex.Message });
        //    }
        //    catch (Exception)
        //    {
        //        // Log the exception ex
        //        return StatusCode(500, "An unexpected error occurred.");
        //    }
        //}
        [HttpGet("me")]
        // Change the return type here from ActionResult<BaseProfileDto> to IActionResult
        public async Task<IActionResult> GetMyProfile()
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString))
                {
                    return Unauthorized();
                }

                // We already have the service method that does the hard work.
                var userProfile = await _userService.GetUserProfileByIdAsync(Guid.Parse(userIdString));

                // The userProfile object here is actually a GetStudentProfileDto (or TeacherDto, etc.).
                // By returning Ok(userProfile), the JSON serializer will inspect its true type
                // and serialize ALL of its properties.
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

        #region public area
        [HttpGet("public-area")]
        [AllowAnonymous]
        public IActionResult GetPublicData()
        {
            return Ok(new { message = "This is the public lobby. Anyone can be here." });
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {

            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }


        [HttpPut("students/{id}/profile")]
        public async Task<IActionResult> UpdateStudentProfile(Guid id, [FromForm] UpdateStudentProfileDto studentDto)
        {
            var userIdFromTokenString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdFromTokenString) || id.ToString() != userIdFromTokenString)
            {
                // Deny access if the user is trying to edit a profile that is not their own.
                // You might allow Admins to bypass this check in the future.
                return Forbid(); // HTTP 403 Forbidden
            }

            // Find the user and make sure they are a student
            var user = await _userRepository.GetByIdAsync(id);
            if (user is not Student student) // Type check
            {
                return NotFound("Student not found.");
            }

            // Map the DTO onto the existing student object
            _mapper.Map(studentDto, student);

            await _userRepository.UpdateAsync(student);
            await _userRepository.SaveChangesAsync();

            return NoContent();
        }

        // Route for updating a Teacher's profile
        [HttpPut("teachers/{id}/profile")]
        public async Task<IActionResult> UpdateTeacherProfile(Guid id, [FromForm] UpdateTeacherProfileDto teacherDto)
        {
            var userIdFromTokenString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdFromTokenString) || id.ToString() != userIdFromTokenString)
            {
                // Deny access if the user is trying to edit a profile that is not their own.
                // You might allow Admins to bypass this check in the future.
                return Forbid(); // HTTP 403 Forbidden
            }
            // Find the user and make sure they are a teacher
            var user = await _userRepository.GetByIdAsync(id);
            if (user is not Teacher teacher) // Type check
            {
                return NotFound("Teacher not found.");
            }

            // Map the DTO onto the existing teacher object
            _mapper.Map(teacherDto, teacher);

            await _userRepository.UpdateAsync(teacher);
            await _userRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("Staff/{id}/profile")]
        public async Task<IActionResult> UpdateStaffProfile(Guid id, [FromForm] UpdateStaffProfileDto staffDto)
        {
            var userIdFromTokenString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdFromTokenString) || id.ToString() != userIdFromTokenString)
            {
                return Forbid();
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user is not Staff staff)
            {
                return NotFound("Staff not Found");
            }
            _mapper.Map(staffDto, staff);

            await _userRepository.UpdateAsync(staff);
            await _userRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("Admin/{id}/profile")]
        public async Task<IActionResult> UpdateAdminProfile(Guid id, [FromBody] UpdateAdminProfileDto adminDto)
        {
            var userIdFromTokenString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdFromTokenString) || id.ToString() != userIdFromTokenString)
            {
                return Forbid();
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user is not Admin admin)
            {
                return NotFound("Admin not Found");
            }
            _mapper.Map(adminDto, admin);

            await _userRepository.UpdateAsync(admin);
            await _userRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
