using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static BackendTechnicalAssetsManagement.src.DTOs.User.UserProfileDtos;

[Route("api/v1/users")]
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

    [HttpGet("me")]
    // FIX: The signature now correctly promises a BaseProfileDto for better documentation.
    public async Task<ActionResult<ApiResponse<BaseProfileDto>>> GetMyProfile()
    {
        // FIX: Removed the local try-catch. Let the service return null for "not found"
        // and let the global middleware handle any unexpected errors.
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString))
        {
            var response = ApiResponse<BaseProfileDto>.FailResponse("Invalid Token.");
            return Unauthorized(response);
        }

        var userProfile = await _userService.GetUserProfileByIdAsync(Guid.Parse(userIdString));
        if (userProfile == null)
        {
            var notFoundResponse = ApiResponse<BaseProfileDto>.FailResponse("User profile not found.");
            return NotFound(notFoundResponse);
        }

        var successResponse = ApiResponse<BaseProfileDto>.SuccessResponse(userProfile, "User profile retrieved successfully.");
        return Ok(successResponse);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        var response = ApiResponse<IEnumerable<UserDto>>.SuccessResponse(users, "Users retrieved successfully.");
        return Ok(response);
    }

    // --- PATCH Endpoints (Now with full implementation) ---

    [HttpPatch("students/{id}/profile")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateStudentProfile(Guid id, [FromForm] UpdateStudentProfileDto studentDto)
    {
        if (!User.IsInRole("Admin") && id.ToString() != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return StatusCode(403, ApiResponse<object>.FailResponse("Not authorized."));
        }

        var user = await _userRepository.GetByIdAsync(id);
        if (user is not Student student)
        {
            return NotFound(ApiResponse<object>.FailResponse("Student not found."));
        }

        _mapper.Map(studentDto, student);
        await _userRepository.UpdateAsync(student);
        await _userRepository.SaveChangesAsync();

        return Ok(ApiResponse<object>.SuccessResponse(null, "Student profile updated successfully."));
    }

    [HttpPatch("teachers/{id}/profile")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateTeacherProfile(Guid id, [FromBody] UpdateTeacherProfileDto teacherDto)
    {
        if (!User.IsInRole("Admin") && id.ToString() != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return StatusCode(403, ApiResponse<object>.FailResponse("Not authorized."));
        }

        var user = await _userRepository.GetByIdAsync(id);
        if (user is not Teacher teacher)
        {
            return NotFound(ApiResponse<object>.FailResponse("Teacher not found."));
        }

        _mapper.Map(teacherDto, teacher);
        await _userRepository.UpdateAsync(teacher);
        await _userRepository.SaveChangesAsync();

        return Ok(ApiResponse<object>.SuccessResponse(null, "Teacher profile updated successfully."));
    }

    [HttpPatch("staff/{id}/profile")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateStaffProfile(Guid id, [FromBody] UpdateStaffProfileDto staffDto)
    {
        if (!User.IsInRole("Admin") && id.ToString() != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return StatusCode(403, ApiResponse<object>.FailResponse("Not authorized."));
        }

        var user = await _userRepository.GetByIdAsync(id);
        if (user is not Staff staff)
        {
            return NotFound(ApiResponse<object>.FailResponse("Staff not found."));
        }

        _mapper.Map(staffDto, staff);
        await _userRepository.UpdateAsync(staff);
        await _userRepository.SaveChangesAsync();

        return Ok(ApiResponse<object>.SuccessResponse(null, "Staff profile updated successfully."));
    }

    [HttpPatch("admin/{id}/profile")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateAdminProfile(Guid id, [FromBody] UpdateAdminProfileDto adminDto)
    {
        if (!User.IsInRole("Admin") && id.ToString() != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return StatusCode(403, ApiResponse<object>.FailResponse("Not authorized."));
        }

        var user = await _userRepository.GetByIdAsync(id);
        if (user is not Admin admin)
        {
            return NotFound(ApiResponse<object>.FailResponse("Admin not found."));
        }

        _mapper.Map(adminDto, admin);
        await _userRepository.UpdateAsync(admin);
        await _userRepository.SaveChangesAsync();

        return Ok(ApiResponse<object>.SuccessResponse(null, "Admin profile updated successfully."));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteUser(Guid id)
    {
        var success = await _userService.DeleteUserAsync(id);
        if (!success)
        {
            return NotFound(ApiResponse<object>.FailResponse("Delete failed. User not found."));
        }

        return Ok(ApiResponse<object>.SuccessResponse(null, "User deleted successfully."));
    }
}