namespace BackendTechnicalAssetsManagement.src.DTOs.User
{
    /// <summary>
    /// DTO for importing users from Excel file
    /// Password will be auto-generated during import
    /// </summary>
    public class ImportUserDto
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
    }

    /// <summary>
    /// Response DTO for bulk student import containing registration results
    /// </summary>
    public class ImportStudentsResponseDto
    {
        public int TotalProcessed { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public List<StudentRegistrationResult> RegisteredStudents { get; set; } = new();
        public List<string> Errors { get; set; } = new();
    }

    /// <summary>
    /// Individual student registration result with generated credentials
    /// </summary>
    public class StudentRegistrationResult
    {
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string GeneratedPassword { get; set; } = string.Empty;
    }
}
