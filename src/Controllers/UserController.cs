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
    /// Updates a student's profile information.
    /// An 'Admin' can update any student profile. A 'Student' can only update their own.
    /// Note: Student ownership is not checked here and must be enforced by the client or a more specific policy.
    /// </summary>
    [HttpPatch("students/profile/{id}")]
    [Authorize(Roles = "Admin,Student")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateStudentProfile(Guid id, [FromForm] UpdateStudentProfileDto studentDto)
    {
        try
        {
            var success = await _userService.UpdateStudentProfileAsync(id, studentDto);

            if (!success)
            {
                return NotFound(ApiResponse<object>.FailResponse("Student not found."));
            }

            return Ok(ApiResponse<object>.SuccessResponse(null, "Student profile updated successfully."));
        }
        catch (ArgumentException ex) // Catches the validation exception from the service
        {
            return BadRequest(ApiResponse<object>.FailResponse(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.FailResponse($"Internal Server Error: {ex.Message}"));
        }
    }

    /// <summary>
    /// Updates a teacher's profile information.
    /// An 'Admin' can update any teacher profile. A 'Teacher' can only update their own profile.
    /// </summary>
    [HttpPatch("teachers/profile/{id}")]
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
    [HttpPatch("admin-or-staff/profile/{id}")] // Using PATCH as per our last discussion
    [Authorize(Roles = "SuperAdmin, Admin, Staff")]
    public async Task<IActionResult> UpdateUserProfile(Guid id, [FromBody] UpdateStaffProfileDto dto)
    {
        // We get the current user's ID securely from the token claims.
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            throw new UnauthorizedAccessException("User not authenticated");
        
        var currentUserId = new Guid(userIdClaim);

        // The controller's job is now just to orchestrate the call.
        // It lives in the "happy path." All error handling is offloaded.
        await _userService.UpdateStaffOrAdminProfileAsync(id, dto, currentUserId);

        // If the line above throws an exception, this line is never reached.
        // The GlobalExceptionHandler takes over.

        // If no exception was thrown, the update was successful.
        return NoContent(); // HTTP 204 is the correct response for a successful update.
    }

    /// <summary>
    /// Archives a user account by its ID, effectively performing a soft delete.
    /// The service layer prevents a user from archiving their own account.
    /// </summary>
    [HttpDelete("archive/{id}")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<ActionResult<ApiResponse<object>>> ArchiveUser(Guid id)
    {
        var currentUserIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(currentUserIdString, out var currentUserId))
        {
            return Unauthorized(ApiResponse<object>.FailResponse("Invalid user token."));
        }

        // The service layer contains the logic to prevent self-archiving.
        var (success, errorMessage) = await _userService.DeleteUserAsync(id, currentUserId);

        if (!success)
        {
            var errorResponse = ApiResponse<object>.FailResponse($"Failed to archive user. {errorMessage}");
            return errorMessage.Contains("not found") ? NotFound(errorResponse) : BadRequest(errorResponse);
        }

        return Ok(ApiResponse<object>.SuccessResponse(null, "User has been successfully archived."));
    }

    /// <summary>
    /// Imports multiple students from an Excel (.xlsx) file with auto-generated usernames and passwords.
    /// Excel file should contain columns: LastName, FirstName, MiddleName (optional)
    /// Returns detailed results including generated credentials for each student.
    /// Access is restricted to users with 'Admin' or 'Staff' roles.
    /// </summary>
    [HttpPost("students/import")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<ActionResult<ApiResponse<ImportStudentsResponseDto>>> ImportStudents(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(ApiResponse<ImportStudentsResponseDto>.FailResponse("No file uploaded."));
            }

            // Validate file extension
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (fileExtension != ".xlsx" && fileExtension != ".xls")
            {
                return BadRequest(ApiResponse<ImportStudentsResponseDto>.FailResponse("Only Excel files (.xlsx or .xls) are allowed."));
            }

            var result = await _userService.ImportStudentsFromExcelAsync(file);
            
            var message = $"Import completed. Success: {result.SuccessCount}, Failed: {result.FailureCount}";
            var response = ApiResponse<ImportStudentsResponseDto>.SuccessResponse(result, message);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<ImportStudentsResponseDto>.FailResponse($"Import failed: {ex.Message}"));
        }
    }

    /// <summary>
    /// Completes student registration by updating remaining required profile information.
    /// This endpoint is used after initial registration to add email, phone, student ID, course details, address, and ID pictures.
    /// Does not update FirstName, MiddleName, or LastName as they are already set during initial registration.
    /// Students can only complete their own registration.
    /// </summary>
    [HttpPatch("students/complete-registration/{id}")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<ApiResponse<object>>> CompleteStudentRegistration(Guid id, [FromForm] CompleteStudentRegistrationDto dto)
    {
        // Ensure student can only update their own profile
        var currentUserIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(currentUserIdString, out var currentUserId) || currentUserId != id)
        {
            return Forbid();
        }

        try
        {
            var success = await _userService.CompleteStudentRegistrationAsync(id, dto);

            if (!success)
            {
                return NotFound(ApiResponse<object>.FailResponse("Student not found."));
            }

            return Ok(ApiResponse<object>.SuccessResponse(null, "Student registration completed successfully."));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<object>.FailResponse(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.FailResponse($"Internal Server Error: {ex.Message}"));
        }
    }

    /// <summary>
    /// Retrieves a student's details by their student ID number.
    /// Access is restricted to users with 'Admin' or 'Staff' roles.
    /// </summary>
    [HttpGet("students/by-id-number/{studentIdNumber}")]
    [Authorize(Policy = "AdminOrStaff")]
    public async Task<ActionResult<ApiResponse<object>>> GetStudentByIdNumber(string studentIdNumber)
    {
        try
        {
            var student = await _userService.GetStudentByIdNumberAsync(studentIdNumber);
            
            if (student == null)
            {
                return NotFound(ApiResponse<object>.FailResponse("Student not found."));
            }

            return Ok(ApiResponse<object>.SuccessResponse(student, "Student retrieved successfully."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.FailResponse($"Internal Server Error: {ex.Message}"));
        }
    }
}