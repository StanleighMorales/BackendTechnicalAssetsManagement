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
    // FIX: Change the generic type from BaseProfileDto to object
    public async Task<ActionResult<ApiResponse<object>>> GetMyProfile()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString))
        {
            // Change FailResponse to use object generic type
            var response = ApiResponse<object>.FailResponse("Invalid Token.");
            return Unauthorized(response);
        }

        // userProfile is the concrete derived DTO (e.g., GetStudentProfileDto)
        var userProfile = await _userService.GetUserProfileByIdAsync(Guid.Parse(userIdString));
        if (userProfile == null)
        {
            // Change FailResponse to use object generic type
            var notFoundResponse = ApiResponse<object>.FailResponse("User profile not found.");
            return NotFound(notFoundResponse);
        }

        // Change SuccessResponse to use object generic type
        var successResponse = ApiResponse<object>.SuccessResponse(userProfile, "User profile retrieved successfully.");

        // The ActionResult return type must also match the object generic type
        return Ok(successResponse);
    }

    [HttpGet]
    [Authorize(Policy = "AdminOrStaff")]
    // Change the return type here to use 'object'
    public async Task<ActionResult<ApiResponse<IEnumerable<object>>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        // And also here when creating the response
        var response = ApiResponse<IEnumerable<object>>.SuccessResponse(users, "Users retrieved successfully.");
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
        try
        {
            ImageConverterUtils.ValidateImage(studentDto.ProfilePicture);
            ImageConverterUtils.ValidateImage(studentDto.FrontStudentIdPicture);
            ImageConverterUtils.ValidateImage(studentDto.BackStudentIdPicture);
        }
        catch (ArgumentException ex)
        {
            // If validation fails, return a 400 Bad Request with the error message
            return BadRequest(ApiResponse<object>.FailResponse(ex.Message));
        }
        // ----------------------------------------------------

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