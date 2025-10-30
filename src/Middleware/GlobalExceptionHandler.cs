using BackendTechnicalAssetsManagement.src.Exceptions;
using BackendTechnicalAssetsManagement.src.Utils;
using System.Net;
using System.Text.Json;

namespace BackendTechnicalAssetsManagement.src.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred: {ErrorMessage}", ex.Message);

                var response = context.Response;
                response.ContentType = "application/json";

                ApiResponse<object> apiResponse;

                switch (ex)
                {
                    // V-- NEW: Handle unauthorized access specifically for a 403 Forbidden --V
                    case UnauthorizedAccessException:
                        response.StatusCode = (int)HttpStatusCode.Forbidden; // 403
                        apiResponse = ApiResponse<object>.FailResponse(ex.Message);
                        break;

                    // V-- NEW: Handle cases where an entity was not found for a 404 Not Found --V
                    case KeyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound; // 404
                        apiResponse = ApiResponse<object>.FailResponse(ex.Message);
                        break;

                    // V-- NEW: Handle invalid arguments/bad requests for a 400 Bad Request --V
                    case ArgumentException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
                        apiResponse = ApiResponse<object>.FailResponse(ex.Message);
                        break;

                    // V-- NEW: Handle invalid operations for a 400 Bad Request --V
                    case InvalidOperationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
                        apiResponse = ApiResponse<object>.FailResponse(ex.Message);
                        break;

                    // Case for your existing custom RefreshTokenException
                    case RefreshTokenException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
                        apiResponse = ApiResponse<object>.FailResponse(ex.Message);
                        break;

                    // Default case for all other unexpected errors
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
                        apiResponse = ApiResponse<object>.FailResponse(
                            "An unexpected internal server error has occurred.",
                            new List<string> { ex.Message }
                        );
                        break;
                }

                var jsonResponse = JsonSerializer.Serialize(apiResponse);
                await response.WriteAsync(jsonResponse);
            }
        }
    }
}