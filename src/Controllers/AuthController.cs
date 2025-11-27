using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using BackendTechnicalAssetsManagement.src.Services;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    /// <summary>
    /// This controller handles all authentication-related actions,
    /// such as user registration, login, and token management.
    /// </summary>
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, IWebHostEnvironment env, ILogger<AuthController> logger, IUserService userService)
        {
            _authService = authService;
            _env = env;
            _logger = logger;
            _userService = userService;
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

        /// <summary>
        /// Registers a new user in the system.
        /// Enforces role hierarchy: SuperAdmin can create all roles, Admin cannot create SuperAdmin,
        /// Staff can only create Teacher and Student users.
        /// </summary>
        /// <param name="request">The user's registration details, including username and password.</param>
        /// <returns>An ApiResponse containing the newly created user's public data (without the password).</returns>
        /// <remarks>
        /// On success, returns HTTP 201 Created.
        /// If the username or email already exists, the service will throw an exception,
        /// and the global error handler middleware will return a 4xx or 500 error.
        /// </remarks>
        [HttpPost("register")]
        [Authorize(Roles = "SuperAdmin,Admin,Staff")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Register(RegisterUserDto request)
        {
            var currentUserIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(currentUserIdString, out var currentUserId))
            {
                return Unauthorized(ApiResponse<UserDto>.FailResponse("Invalid user token."));
            }

            var newUser = await _authService.Register(request, currentUserId);
            var successResponse = ApiResponse<UserDto>.SuccessResponse(newUser, "User Registered Successfully.");

            return StatusCode(201, successResponse);
        }

        #region Login/Logout
        /// <summary>
        /// Authenticates a user and returns a JSON Web Token (JWT).
        /// </summary>
        /// <param name="request">The user's login credentials (e.g., username and password).</param>
        /// <returns>An ApiResponse containing the JWT access token as a string.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Login(LoginUserDto request) // Change return type
        {
            UserDto user = await _authService.Login(request);
            var response = ApiResponse<UserDto>.SuccessResponse(user, "Login successful.");

            // Optionally log or include debug info only in development
            if (_env.IsDevelopment())
            {
                _logger.LogInformation("User {Email} logged in during development.", user.Email);
            }

            return Ok(response);
        }
        /// <summary>
        /// Authenticates a user and returns a JSON Web Token (JWT) and Refresh Token in the response body.
        /// Intended for use by mobile applications or pure API clients.
        /// </summary>
        /// <param name="request">The user's login credentials (e.g., username and password).</param>
        /// <returns>An ApiResponse containing the user data, access token, and refresh token.</returns>
        [HttpPost("login-mobile")]
        public async Task<ActionResult<ApiResponse<MobileLoginResponseDto>>> LoginMobile(LoginUserDto request)
        {
            var data = await _authService.LoginMobile(request);
            var response = ApiResponse<MobileLoginResponseDto>.SuccessResponse(data, "Mobile login successful.");

            if (_env.IsDevelopment())
            {
                _logger.LogInformation("User {Email} logged in via mobile endpoint.", data.User.Email);
            }

            return Ok(response);
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>An ApiResponse with a success message and no data.</returns>
        /// <remarks>This endpoint requires the user to be authenticated.</remarks>
        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<object>>> Logout()
        {
            await _authService.Logout();
            var successResponse = ApiResponse<object>.SuccessResponse(null, "Logout Successful.");
            return Ok(successResponse);
        }
        #endregion

        #region Refresh Token
        /// <summary>
        /// Generates a new access token using a valid refresh token.
        /// </summary>
        /// <returns>An ApiResponse containing a new JWT access token as a string.</returns>
        /// <remarks>
        /// The refresh token is typically sent via a secure, http-only cookie.
        /// </remarks>
        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<string>>> RefreshToken()
        {
            var newAccessToken = await _authService.RefreshToken();
            var successResponse = ApiResponse<string>.SuccessResponse(newAccessToken, "Token Refreshed Successfully.");
            return Ok(successResponse);
        }

        /// <summary>
        /// Generates a new access token and a new refresh token using a valid refresh token from the request body.
        /// Intended for use by mobile applications or pure API clients.
        /// </summary>
        /// <param name="request">The RefreshTokenRequestDto containing the Refresh Token string.</param>
        /// <returns>An ApiResponse containing a new Access Token and a new Refresh Token.</returns>
        [HttpPost("refresh-token-mobile")] // <-- MOBILE refresh token endpoint (uses JSON body)
        public async Task<ActionResult<ApiResponse<MobileLoginResponseDto>>> RefreshTokenMobile([FromBody] RefreshTokenDto request)
        {
            var data = await _authService.RefreshTokenMobile(request.RefreshToken);
            var successResponse = ApiResponse<MobileLoginResponseDto>.SuccessResponse(data, "Token Refreshed Successfully.");
            return Ok(successResponse);
        }
        #endregion
        [HttpPatch("change-password/{userId}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<object>>> ChangePassword(Guid userId, ChangePasswordDto request)
        {
            await _authService.ChangePassword(userId, request);
            var response = ApiResponse<object>.SuccessResponse(null, "Password has been successfully changed.");
            return Ok(response);
        }
    }
}
