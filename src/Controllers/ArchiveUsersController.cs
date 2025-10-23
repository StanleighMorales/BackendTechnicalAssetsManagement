using BackendTechnicalAssetsManagement.src.DTOs.Archive.Users; // Import the DTOs
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Utils; // Import the ApiResponse wrapper
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/v1/archiveUsers")]
    [Authorize(Roles = "Admin,Staff")] // Secure all endpoints in this controller for Admin or Staff
    public class ArchiveUsersController : ControllerBase
    {
        private readonly IArchiveUserService _archiveUserService;

        public ArchiveUsersController(IArchiveUserService archiveUserService)
        {
            _archiveUserService = archiveUserService;
        }

        /// <summary>
        /// Retrieves a list of all archived users.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ArchiveUserDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllArchivedUsers()
        {
            var archivedUsers = await _archiveUserService.GetAllArchivedUsersAsync();
            // Wrap the successful response using the static factory method
            var response = ApiResponse<IEnumerable<ArchiveUserDto>>.SuccessResponse(archivedUsers, "Archived users retrieved successfully.");
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific archived user by their archive ID.
        /// </summary>
        /// <param name="id">The GUID of the archived user.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<ArchiveUserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ArchiveUserDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetArchivedUserById(Guid id)
        {
            var archivedUser = await _archiveUserService.GetArchivedUserByIdAsync(id);
            if (archivedUser == null)
            {
                // Use the FailResponse for a not found scenario
                var failResponse = ApiResponse<ArchiveUserDto>.FailResponse($"Archived user with ID {id} not found.");
                return NotFound(failResponse);
            }
            // Use the SuccessResponse for a found scenario
            var successResponse = ApiResponse<ArchiveUserDto>.SuccessResponse(archivedUser, "Archived user found.");
            return Ok(successResponse);
        }
        /// <summary>
        /// Restores an archived user back to the active user list.
        /// </summary>
        /// <param name="archiveUserId">The GUID of the archived user to restore.</param>
        [HttpDelete("restore/{archiveUserId}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RestoreUser(Guid archiveUserId)
        {
            var result = await _archiveUserService.RestoreUserAsync(archiveUserId);
            if (!result)
            {
                var failResponse = ApiResponse<object>.FailResponse($"Failed to restore archived user with ID {archiveUserId}. User may not exist in archive.");
                return BadRequest(failResponse);
            }
            var successResponse = ApiResponse<object>.SuccessResponse(null, "User has been successfully restored.");
            return Ok(successResponse);
        }

        /// <summary>
        /// Permanently deletes a user from the archive. This action is irreversible.
        /// </summary>
        /// <param name="archiveUserId">The GUID of the archived user to delete permanently.</param>
        [HttpDelete("permanent-delete{archiveUserId}")]
        [Authorize(Roles = "Admin")] // Example: Restrict permanent deletion to only Admins
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PermanentDeleteUser(Guid archiveUserId)
        {
            var result = await _archiveUserService.PermanentDeleteArchivedUserAsync(archiveUserId);
            if (!result)
            {
                var failResponse = ApiResponse<object>.FailResponse($"Archived user with ID {archiveUserId} not found.");
                return NotFound(failResponse);
            }
            var successResponse = ApiResponse<object>.SuccessResponse(null, "Archived user has been permanently deleted.");
            return Ok(successResponse);
        }
    }
}