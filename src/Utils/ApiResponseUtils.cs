namespace BackendTechnicalAssetsManagement.src.Utils
{
    public class ApiResponseUtils<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        // Static factory method for a success response
        public static ApiResponseUtils<T> SuccessResponse(T data, string message = "Request successful.")
        {
            return new ApiResponseUtils<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Errors = null
            };
        }

        // Static factory method for a failure response
        public static ApiResponseUtils<T> FailResponse(string message, List<string>? errors = null)
        {
            return new ApiResponseUtils<T>
            {
                Success = false,
                Message = message,
                Data = default(T), // No data on failure
                Errors = errors
            };
        }
    }
}
