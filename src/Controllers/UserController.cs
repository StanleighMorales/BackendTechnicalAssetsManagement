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
using static BackendTechnicalAssetsManagement.src.Classes.Enums;
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
    [HttpGet("students")]
    [Authorize(Policy = "AdminOrStaff")]
    // Use StudentDto here if your UserService is returning StudentDto
    // (If it returns GetStudentProfileDto, use that instead)
    public async Task<ActionResult<ApiResponse<IEnumerable<StudentDto>>>> GetAllStudents()
    {
        var students = await _userService.GetAllStudentsAsync();
        // Adjusted to use StudentDto
        var response = ApiResponse<IEnumerable<StudentDto>>.SuccessResponse(students, "Students retrieved successfully.");
        return Ok(response);
    }

    [HttpGet("teachers")]
    [Authorize(Policy = "AdminOrStaff")]
    public async Task<ActionResult<ApiResponse<IEnumerable<TeacherDto>>>> GetAllTeachers()
    {
        var teachers = await _userService.GetAllTeachersAsync();
        var response = ApiResponse<IEnumerable<TeacherDto>>.SuccessResponse(teachers, "Teachers retrieved successfully.");
        return Ok(response);
    }
    // --- PATCH Endpoints (Now with full implementation) ---
    [HttpPatch("students/profile{id}")]
    [Authorize(Roles = "Admin,Student")]
    // Authorization Policy: Only Admin can update an arbitrary Student profile ID
    public async Task<ActionResult<ApiResponse<object>>> UpdateStudentProfile(Guid id, [FromForm] UpdateStudentProfileDto studentDto)
    {
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

    [HttpPatch("teachers/profile{id}")]
    [Authorize(Roles = "Admin,Teacher")]
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
    [HttpPatch("profile/admin-or-staff")]
    [Authorize(Policy = "AdminOrStaff")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateMyProfile([FromBody] UpdateStaffProfileDto dto)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(ApiResponse<object>.FailResponse("Invalid Token."));
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound(ApiResponse<object>.FailResponse("User not found."));
        }

        // Role Check to ensure the correct DTO is used
        if (user.UserRole == UserRole.Staff || user.UserRole == UserRole.Admin)
        {
            var success = await _userService.UpdateStaffOrAdminProfileAsync(userId, dto);

            if (success)
            {
                // Uniformly use ApiResponse for success responses
                return Ok(ApiResponse<object>.SuccessResponse(null, $"{user.UserRole} profile updated successfully."));
            }
        }
        else
        {
            // If a Teacher or Student tries to use this endpoint
            return BadRequest(ApiResponse<object>.FailResponse("Invalid update request for your user role."));
        }

        return StatusCode(500, ApiResponse<object>.FailResponse("Failed to update user profile."));
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