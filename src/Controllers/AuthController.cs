using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="request">The user's registration details, including username and password.</param>
        /// <returns>An ApiResponse containing the newly created user's public data (without the password).</returns>
        /// <remarks>
        /// On success, returns HTTP 201 Created.
        /// If the username or email already exists, the service will throw an exception,
        /// and the global error handler middleware will return a 4xx or 500 error.
        /// </remarks>
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Register(RegisterUserDto request)
        {
            var newUser = await _authService.Register(request);
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
        public async Task<ActionResult<ApiResponse<string>>> Login(LoginUserDto request)
        {
            string accessToken = await _authService.Login(request);
            var successResponse = ApiResponse<string>.SuccessResponse(accessToken, "Login Successful.");
            return Ok(successResponse);
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
        #endregion
    }
}
