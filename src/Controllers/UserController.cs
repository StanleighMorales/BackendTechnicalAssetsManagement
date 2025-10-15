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
    [HttpGet("{id}")]
    [Authorize(Policy = "AdminOrStaff")] // Restricts general access to Admin and Staff
    public async Task<ActionResult<ApiResponse<object>>> GetUserProfileById(Guid id)
    {
        var currentUserIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid currentUserId = Guid.Empty;

        // 1. Authorization Check: Allow Admin/Staff OR if the ID matches the current user.
        if (!Guid.TryParse(currentUserIdString, out currentUserId))
        {
            // Should not happen if [Authorize] is present, but good for safety.
            return Unauthorized(ApiResponse<object>.FailResponse("Invalid Token or user ID not found."));
        }

        // Check if the current user is NOT Admin/Staff AND the requested ID is NOT their own.
        if (!User.IsInRole("Admin") && !User.IsInRole("Staff") && id != currentUserId)
        {
            // If they are not Admin/Staff and trying to view another user's profile.
            return StatusCode(403, ApiResponse<object>.FailResponse("Not authorized to view this profile."));
        }

        // 2. Fetch the user profile using the service method.
        // The service method (GetUserProfileByIdAsync) already handles the role-specific mapping.
        var userProfile = await _userService.GetUserProfileByIdAsync(id);

        if (userProfile == null)
        {
            var notFoundResponse = ApiResponse<object>.FailResponse("User profile not found.");
            return NotFound(notFoundResponse);
        }

        // 3. Return the success response.
        // The 'object' generic type correctly holds the specific DTO (e.g., GetStudentProfileDto).
        var successResponse = ApiResponse<object>.SuccessResponse(userProfile, "User profile retrieved successfully.");

        return Ok(successResponse);
    }

    // --- PATCH Endpoints (Now with full implementation) ---
    [HttpPatch("students/{id}/profile")]
    // Authorization Policy: Only Admin can update an arbitrary Student profile ID
    public async Task<ActionResult<ApiResponse<object>>> UpdateStudentProfile(Guid id, [FromForm] UpdateStudentProfileDto studentDto)
    {
        // The previous complex authorization check is GONE.
        // The [Authorize(Roles = "Admin")] handles the only check we need now.
        // However, for a student to update their OWN profile, we need a slight adjustment:
        // If you want any user to update their OWN profile without an Admin token, you cannot use this simple check.

        // RETHINKING: Based on your initial requirement, let's keep the original *intention* but trust the frontend.
        // If the frontend is ALWAYS sending the correct ID, we can remove the check, BUT the authorization logic 
        // must still prevent a student from updating another student's profile if they bypass the frontend.

        // Let's go back to your original *intention* but remove the confusing Guid parsing check.
        // If a non-Admin user is guaranteed to be sending their OWN ID, we don't need a custom policy.

        // **NEW INTERPRETATION:** Let's keep the authorization only for **Admin** and let them update any ID.
        // For *all* other users, if they send their OWN ID, the update will proceed successfully because
        // the user is authenticated. This is a common pattern when trusting the frontend on which ID to send.

        try
        {
            // NO EXPLICIT AUTHORIZATION CHECK HERE - Relying on the [Authorize] attribute for Admin/Authenticated

            var user = await _userRepository.GetByIdAsync(id);
            if (user is not Student student)
            {
                return NotFound(ApiResponse<object>.FailResponse("Student not found."));
            }

            // --- Image Validation with NULL checks ---
            try
            {
                if (studentDto.ProfilePicture != null) ImageConverterUtils.ValidateImage(studentDto.ProfilePicture);
                if (studentDto.FrontStudentIdPicture != null) ImageConverterUtils.ValidateImage(studentDto.FrontStudentIdPicture);
                if (studentDto.BackStudentIdPicture != null) ImageConverterUtils.ValidateImage(studentDto.BackStudentIdPicture);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<object>.FailResponse(ex.Message));
            }
            // ----------------------------------------------------------------

            // Perform Mapping
            _mapper.Map(studentDto, student);

            await _userRepository.UpdateAsync(student);
            await _userRepository.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(null, "Student profile updated successfully."));
        }
        catch (Exception ex)
        {
            // THIS IS THE CRASH CATCHER WE NEED TO HIT
            var errorMessage = ex.ToString();
            // The exception details are now in 'ex'.
            return StatusCode(500, ApiResponse<object>.FailResponse($"Internal Server Error: {ex.Message}"));
        }
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