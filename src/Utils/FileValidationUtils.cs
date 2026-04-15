using Microsoft.AspNetCore.Http;

namespace BackendTechnicalAssetsManagement.src.Utils
{
    /// <summary>
    /// Centralized utility for validating file uploads with security-focused checks.
    /// Implements server-side validation for file types, MIME types, and magic bytes.
    /// </summary>
    public static class FileValidationUtils
    {
        // Allowed file extensions for imports
        private static readonly string[] AllowedExtensions = { ".xlsx", ".xls", ".csv" };

        // Allowed MIME types for imports
        private static readonly string[] AllowedMimeTypes =
        {
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", // .xlsx
            "application/vnd.ms-excel",                                           // .xls
            "text/csv",                                                           // .csv
            "application/csv",                                                    // .csv (alternative)
            "text/comma-separated-values"                                         // .csv (alternative)
        };

        // Magic bytes (file signatures) for validation
        private static readonly Dictionary<string, byte[][]> MagicBytes = new()
        {
            // .xlsx files start with PK (ZIP format)
            { ".xlsx", new[] { new byte[] { 0x50, 0x4B, 0x03, 0x04 }, new byte[] { 0x50, 0x4B, 0x05, 0x06 }, new byte[] { 0x50, 0x4B, 0x07, 0x08 } } },
            
            // .xls files (OLE2 format)
            { ".xls", new[] { new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } } },
            
            // .csv files (text-based, no specific magic bytes - we'll check for printable ASCII/UTF-8)
            { ".csv", Array.Empty<byte[]>() }
        };

        /// <summary>
        /// Validates an uploaded file for import operations.
        /// Performs extension, MIME type, and magic byte validation.
        /// </summary>
        /// <param name="file">The uploaded file to validate</param>
        /// <returns>Tuple containing validation result and error message if validation fails</returns>
        public static async Task<(bool IsValid, string? ErrorMessage)> ValidateImportFileAsync(IFormFile file)
        {
            // 1. Check if file exists and has content
            if (file == null || file.Length == 0)
            {
                return (false, "No file uploaded or file is empty.");
            }

            // 2. Check file size (5MB limit as configured in Program.cs)
            const long maxFileSize = 5 * 1024 * 1024; // 5MB
            if (file.Length > maxFileSize)
            {
                return (false, $"File size exceeds the maximum allowed size of {maxFileSize / (1024 * 1024)}MB.");
            }

            // 3. Validate file extension
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(fileExtension) || !AllowedExtensions.Contains(fileExtension))
            {
                return (false, $"Invalid file extension. Only {string.Join(", ", AllowedExtensions)} files are allowed.");
            }

            // 4. Validate MIME type (Content-Type header)
            if (string.IsNullOrEmpty(file.ContentType) || !AllowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
            {
                return (false, $"Invalid file type. Only Excel (.xlsx, .xls) and CSV (.csv) files are allowed.");
            }

            // 5. Validate magic bytes (file signature) to prevent file extension spoofing
            var magicByteValidation = await ValidateMagicBytesAsync(file, fileExtension);
            if (!magicByteValidation.IsValid)
            {
                return (false, magicByteValidation.ErrorMessage);
            }

            return (true, null);
        }

        /// <summary>
        /// Validates the file's magic bytes (file signature) to ensure the file content matches its extension.
        /// This prevents attackers from renaming malicious files with allowed extensions.
        /// </summary>
        /// <param name="file">The uploaded file</param>
        /// <param name="extension">The file extension</param>
        /// <returns>Tuple containing validation result and error message if validation fails</returns>
        private static async Task<(bool IsValid, string? ErrorMessage)> ValidateMagicBytesAsync(IFormFile file, string extension)
        {
            if (!MagicBytes.ContainsKey(extension))
            {
                return (false, "Unsupported file type for magic byte validation.");
            }

            var expectedSignatures = MagicBytes[extension];

            // CSV files are text-based and don't have specific magic bytes
            // We'll perform a basic text validation instead
            if (extension == ".csv")
            {
                return await ValidateCsvContentAsync(file);
            }

            // Read the first 8 bytes of the file for signature checking
            const int signatureSize = 8;
            var buffer = new byte[signatureSize];

            using (var stream = file.OpenReadStream())
            {
                var bytesRead = await stream.ReadAsync(buffer, 0, signatureSize);
                
                if (bytesRead < 4)
                {
                    return (false, "File is too small or corrupted.");
                }

                // Check if the file starts with any of the expected signatures
                foreach (var signature in expectedSignatures)
                {
                    if (buffer.Take(signature.Length).SequenceEqual(signature))
                    {
                        return (true, null);
                    }
                }
            }

            return (false, $"File content does not match the expected format for {extension} files. The file may be corrupted or renamed.");
        }

        /// <summary>
        /// Validates CSV file content by checking if it contains valid text data.
        /// </summary>
        /// <param name="file">The uploaded CSV file</param>
        /// <returns>Tuple containing validation result and error message if validation fails</returns>
        private static async Task<(bool IsValid, string? ErrorMessage)> ValidateCsvContentAsync(IFormFile file)
        {
            const int sampleSize = 512; // Read first 512 bytes for validation
            var buffer = new byte[sampleSize];

            using (var stream = file.OpenReadStream())
            {
                var bytesRead = await stream.ReadAsync(buffer, 0, sampleSize);
                
                if (bytesRead == 0)
                {
                    return (false, "CSV file is empty.");
                }

                // Check if the content is valid text (ASCII or UTF-8)
                // CSV files should contain printable characters, commas, newlines, etc.
                var validTextCharacters = 0;
                var totalCharacters = bytesRead;

                for (int i = 0; i < bytesRead; i++)
                {
                    var b = buffer[i];
                    // Check for printable ASCII, tabs, newlines, carriage returns, commas
                    if ((b >= 0x20 && b <= 0x7E) || b == 0x09 || b == 0x0A || b == 0x0D || b == 0x2C)
                    {
                        validTextCharacters++;
                    }
                }

                // At least 90% of the sampled bytes should be valid text characters
                var validPercentage = (double)validTextCharacters / totalCharacters;
                if (validPercentage < 0.9)
                {
                    return (false, "CSV file contains invalid or binary data. Please ensure the file is a valid CSV text file.");
                }
            }

            return (true, null);
        }

        /// <summary>
        /// Returns a standardized HTTP 415 Unsupported Media Type error message.
        /// </summary>
        /// <returns>Error message string</returns>
        public static string GetUnsupportedMediaTypeMessage()
        {
            return $"Unsupported Media Type. Only {string.Join(", ", AllowedExtensions)} files are allowed for import operations.";
        }
    }
}
