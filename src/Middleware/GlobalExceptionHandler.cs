using BackendTechnicalAssetsManagement.src.Exceptions;
using BackendTechnicalAssetsManagement.src.Utils;
using System.Net;
using System.Text.Json;

namespace BackendTechnicalAssetsManagement.src.Middleware
{
    /// <summary>
    /// A global middleware for catching unhandled exceptions that occur during a request.
    /// This ensures that the application always returns a clean, structured JSON error response
    /// instead of crashing or sending an unformatted server error page.
    /// </summary>
    public class GlobalExceptionHandler
    {
        // _next represents the next middleware in the application's request pipeline.
        private readonly RequestDelegate _next;

        // _logger is used for logging the exception details for debugging purposes.
        private readonly ILogger<GlobalExceptionHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionHandler"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">The logger for this middleware.</param>
        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// The main method of the middleware, called for every HTTP request.
        /// </summary>
        /// <param name="context">The HttpContext for the current request.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // This attempts to execute the rest of the middleware pipeline.
                // If everything downstream (controllers, services, etc.) is successful,
                // this middleware does nothing else, and the request completes normally.
                await _next(context);
            }
            catch (Exception ex)
            {
                // If any middleware or application code downstream throws an exception
                // that is not caught, this 'catch' block will execute.

                // Log the full exception details, including the stack trace. This is crucial for debugging.
                _logger.LogError(ex, "An unhandled exception has occurred: {ErrorMessage}", ex.Message);

                // Prepare the HTTP response to be sent back to the client.
                var response = context.Response;
                response.ContentType = "application/json";

                // We will use our standard ApiResponse wrapper for all error responses.
                ApiResponse<object> apiResponse;

                // This is the intelligent "dispatcher" logic. It checks the specific TYPE of the exception
                // to determine how to respond. This is much more reliable than checking string messages.
                switch (ex)
                {
                    // Case 1: Was the exception a 'RefreshTokenException'?
                    // This is a known, controlled error scenario that we defined. It represents an
                    // authorization failure, not a server bug.
                    case RefreshTokenException:
                        // Set the HTTP status code to 401 Unauthorized, which is the correct
                        // response for this type of authentication/authorization error.
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        // Create a failure response using the message from the exception.
                        apiResponse = ApiResponse<object>.FailResponse(ex.Message);
                        break;

                    // You can add more custom exception handlers here in the future. For example:
                    // case ValidationException:
                    //     response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
                    //     apiResponse = ApiResponse<object>.FailResponse("Validation failed.", ex.Errors);
                    //     break;

                    // Case 2 (Default): The exception was NOT one of our known custom types.
                    // This means it was an unexpected and unhandled error (e.g., NullReferenceException,
                    // database connection error, etc.). This is a true server-side bug.
                    default:
                        // Set the HTTP status code to 500 Internal Server Error.
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        // Create a generic failure response. For security, we don't send the raw
                        // exception details to the client in production, only the generic message.
                        apiResponse = ApiResponse<object>.FailResponse(
                            "An unexpected internal server error has occurred.",
                            new List<string> { ex.Message } // In production, you might choose to hide this raw message.
                        );
                        break;
                }

                // Convert our standardized ApiResponse object into a JSON string.
                var jsonResponse = JsonSerializer.Serialize(apiResponse);

                // Write the JSON string to the response stream being sent to the client.
                await response.WriteAsync(jsonResponse);
            }
        }
    }
}