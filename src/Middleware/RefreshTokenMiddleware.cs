using BackendTechnicalAssetsManagement.src.Exceptions;
using BackendTechnicalAssetsManagement.src.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackendTechnicalAssetsManagement.src.Middleware
{
    public class RefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RefreshTokenMiddleware> _logger;

        // Threshold for refreshing: 5 minutes before expiry
        //private static readonly TimeSpan RefreshThreshold = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan RefreshThreshold = TimeSpan.FromSeconds(10);

        public RefreshTokenMiddleware(RequestDelegate next, ILogger<RefreshTokenMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            // Only attempt to refresh if the request looks like it needs authorization 
            // and is not already the refresh endpoint (if you have one)
            // Or if the user is authenticated (HttpContext.User has claims)
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var expirationClaim = context.User.Claims
                    .FirstOrDefault(c => c.Type == "exp")?.Value;

                if (expirationClaim != null && long.TryParse(expirationClaim, out long expTimeSeconds))
                {
                    // Convert Unix timestamp to DateTime
                    var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expTimeSeconds).UtcDateTime;
                    var timeUntilExpiry = expirationTime.Subtract(DateTime.UtcNow);

                    // Check if the token is within the refresh threshold (e.g., less than 5 minutes remaining)
                    // Note: We are relying on the JWT middleware to have successfully validated the token
                    // for the claims to be present. The refresh must be done *before* the token is fully expired.
                    // If the token is fully expired, the JWT middleware will set IsAuthenticated=false.

                    // You might need to relax the `ValidateLifetime` in JwtBearerOptions (ClockSkew = TimeSpan.Zero)
                    // and manually check expiration if you want to refresh tokens that are already technically expired.
                    // For simplicity, we'll refresh when it's *about* to expire.
                    TimeSpan MaxExpiredBuffer = TimeSpan.FromSeconds(-5);
                    bool shouldRefresh =
                                    (timeUntilExpiry > TimeSpan.Zero && timeUntilExpiry <= RefreshThreshold) ||
                                    (timeUntilExpiry < TimeSpan.Zero && timeUntilExpiry >= MaxExpiredBuffer);

                    if (shouldRefresh)
                    {
                        _logger.LogInformation("Access token is nearing/just past expiry. Attempting to refresh.");

                        try
                        {
                            // This calls your AuthService.RefreshToken() logic.
                            // The new access token will be automatically set in the response cookie.
                            await authService.RefreshToken();
                            _logger.LogInformation("Access token successfully refreshed.");
                        }
                        catch (RefreshTokenException ex)
                        {
                            // The user's refresh token in the DB was missing or expired.
                            // Force them to re-login by clearing the access token cookie.
                            context.Response.Cookies.Delete("accessToken");
                            _logger.LogWarning($"Token refresh failed for user: {ex.Message}. Clearing cookie.");

                            // Set status code to 401 and RETURN to stop the pipeline, as authentication failed.
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "An unexpected error occurred during token refresh.");
                            // Continue with the request; the user will get an error/401 later if refresh failed.
                        }
                    }
                }
            }

            // Continue to the next middleware in the pipeline (e.g., Authorization and then Controller)
            await _next(context);
        }
    }

    public static class RefreshTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseRefreshTokenMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RefreshTokenMiddleware>();
        }
    }
}