namespace BackendTechnicalAssetsManagement.src.Utils
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        // Static factory method for a success response
        public static ApiResponse<T> SuccessResponse(T? data, string message = "Request successful.")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Errors = null
            };
        }

        // Static factory method for a failure response
        public static ApiResponse<T> FailResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default(T), // No data on failure
                Errors = errors
            };
        }
    }
}
