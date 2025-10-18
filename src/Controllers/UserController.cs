using AutoMapper;
using BackendTechnicalAssetsManagement.src.Authorization;
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

/// <summary>
/// Manages user-related operations, including retrieving user profiles, updating information, and archiving accounts.
/// All endpoints require authentication.
/// </summary>
[Route("api/v1/users")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IUserRepository userRepository, IMapper mapper, IAuthorizationService authorizationService)
    {
        _userService = userService;
        _userRepository = userRepository;
        _mapper = mapper;
        _authorizationService = authorizationService;
    }

    /// <summary>
    /// Retrieves a list of all users.
    /// Access is restricted to users with 'Admin' or 'Staff' roles.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "AdminOrStaff")]
    public async Task<ActionResult<ApiResponse<IEnumerable<object>>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        var response = ApiResponse<IEnumerable<object>>.SuccessResponse(users, "Users retrieved successfully.");
        return Ok(response);
    }

    /// <summary>
    /// Retrieves a specific user's profile by their unique ID.
    /// Implements resource-based authorization to determine if the requester has permission to view the target profile.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> GetUserProfileById(Guid id)
    {
        var userToView = await _userRepository.GetByIdAsync(id);
        if (userToView == null)
        {
            return NotFound(ApiResponse<object>.FailResponse("User profile not found."));
        }

        // Use the authorization service to apply custom 'ViewProfileRequirement' rules.
        // This checks if the current user can view the specific 'userToView' resource.
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, userToView, new ViewProfileRequirement());

        if (!authorizationResult.Succeeded)
        {
            return Forbid(); // Return 403 Forbidden if authorization rules fail.
        }

        var userProfile = await _userService.GetUserProfileByIdAsync(id);
        var successResponse = ApiResponse<object>.SuccessResponse(userProfile, "User profile retrieved successfully.");
        return Ok(successResponse);
    }

    /// <summary>
    /// Retrieves a list of all users with the 'Student' role.
    /// Access is restricted to users with 'Admin' or 'Staff' roles.
    /// </summary>
    [HttpGet("students")]
    [Authorize(Policy = "AdminOrStaff")]
    public async Task<ActionResult<ApiResponse<IEnumerable<StudentDto>>>> GetAllStudents()
    {
        var students = await _userService.GetAllStudentsAsync();
        var response = ApiResponse<IEnumerable<StudentDto>>.SuccessResponse(students, "Students retrieved successfully.");
        return Ok(response);
    }

    /// <summary>
    /// Retrieves a list of all users with the 'Teacher' role.
    /// Access is restricted to users with 'Admin' or 'Staff' roles.
    /// </summary>
    [HttpGet("teachers")]
    [Authorize(Policy = "AdminOrStaff")]
    public async Task<ActionResult<ApiResponse<IEnumerable<TeacherDto>>>> GetAllTeachers()
    {
        var teachers = await _userService.GetAllTeachersAsync();
        var response = ApiResponse<IEnumerable<TeacherDto>>.SuccessResponse(teachers, "Teachers retrieved successfully.");
        return Ok(response);
    }

    /// <summary>
    /// Updates a student's profile information.
    /// An 'Admin' can update any student profile. A 'Student' can only update their own.
    /// Note: Student ownership is not checked here and must be enforced by the client or a more specific policy.
    /// </summary>
    [HttpPatch("students/profile{id}")]
    [Authorize(Roles = "Admin,Student")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateStudentProfile(Guid id, [FromForm] UpdateStudentProfileDto studentDto)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is not Student student)
            {
                return NotFound(ApiResponse<object>.FailResponse("Student not found."));
            }

            // Centralized image validation to ensure files are valid before processing.
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

            _mapper.Map(studentDto, student);

            await _userRepository.UpdateAsync(student);
            await _userRepository.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(null, "Student profile updated successfully."));
        }
        catch (Exception ex)
        {
            // General exception handler for unexpected errors during the update process.
            return StatusCode(500, ApiResponse<object>.FailResponse($"Internal Server Error: {ex.Message}"));
        }
    }

    /// <summary>
    /// Updates a teacher's profile information.
    /// An 'Admin' can update any teacher profile. A 'Teacher' can only update their own profile.
    /// </summary>
    [HttpPatch("teachers/profile{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateTeacherProfile(Guid id, [FromBody] UpdateTeacherProfileDto teacherDto)
    {
        // Enforce that a non-admin user can only update their own profile.
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

    /// <summary>
    /// Updates the profile of the currently authenticated 'Admin' or 'Staff' user.
    /// </summary>
    [HttpPatch("profile/admin-or-staff")]
    [Authorize(Policy = "AdminOrStaff")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateMyProfile([FromBody] UpdateStaffProfileDto dto)
    {
        // Retrieve the user ID from the token to ensure users can only update themselves.
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

        if (user.UserRole == UserRole.Staff || user.UserRole == UserRole.Admin)
        {
            var success = await _userService.UpdateStaffOrAdminProfileAsync(userId, dto);
            if (success)
            {
                return Ok(ApiResponse<object>.SuccessResponse(null, $"{user.UserRole} profile updated successfully."));
            }
        }
        else
        {
            return BadRequest(ApiResponse<object>.FailResponse("Invalid update request for your user role."));
        }

        return StatusCode(500, ApiResponse<object>.FailResponse("Failed to update user profile."));
    }

    /// <summary>
    /// Archives a user account by its ID, effectively performing a soft delete.
    /// The service layer prevents a user from archiving their own account.
    /// </summary>
    [HttpDelete("archive/{id}")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteUser(Guid id)
    {
        var currentUserIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(currentUserIdString, out var currentUserId))
        {
            return Unauthorized(ApiResponse<object>.FailResponse("Invalid user token."));
        }

        // The service layer contains the logic to prevent self-archiving.
        var success = await _userService.DeleteUserAsync(id, currentUserId);

        if (!success)
        {
            return BadRequest(ApiResponse<object>.FailResponse("Failed to archive user. The user may not exist or the action is not permitted."));
        }

        return Ok(ApiResponse<object>.SuccessResponse(null, "User has been successfully archived."));
    }
}