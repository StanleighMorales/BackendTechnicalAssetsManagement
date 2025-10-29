using Microsoft.AspNetCore.Http;
namespace BackendTechnicalAssetsManagement.src.Utils
{
    public class ImageConverterUtils
    {
        private const int MaxImageSizeBytes = 5 * 1024 * 1024; // 5MB
        public static byte[]? ConvertIFormFileToByteArray(IFormFile? formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public static void ValidateImage(IFormFile? image)
            {
                if (image == null) return;

                // 1. Check Size
                if (image.Length > MaxImageSizeBytes)
                {
                    throw new ArgumentException($"Image file size cannot exceed {MaxImageSizeBytes / (1024 * 1024)}MB.");
                }

                // 2. Check Extension (Re-using logic from your existing utilities)
                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif", ".webp" };
                var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                {
                    throw new ArgumentException("Invalid image file type. Allowed types are: " + string.Join(", ", allowedExtensions));
                }
            }
    }
}
