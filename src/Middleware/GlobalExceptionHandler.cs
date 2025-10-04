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
                // This tries to execute the next piece of middleware in the pipeline
                // (which will eventually call your controller action).
                await _next(context);
            }
            catch (Exception ex)
            {
                // If any unhandled exception occurs, this block will catch it.
                _logger.LogError(ex, "An unhandled exception has occurred.");

                // Set the response status code to 500 Internal Server Error
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                // Create our standard error response using the wrapper
                var response = ApiResponse<object>.FailResponse(
                    "An unexpected internal server error has occurred. Please try again later.",
                    new List<string> { ex.Message } // In production, you might not want to send the raw exception message
                );

                // Convert the response to a JSON string
                var jsonResponse = JsonSerializer.Serialize(response);

                // Write the JSON response to the client
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
