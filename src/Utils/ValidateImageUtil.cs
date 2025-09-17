namespace BackendTechnicalAssetsManagement.src.Utils
{
    public class ValidateImageUtil
    {
        public static void ValidateImage(IFormFile? image)
        {
            if (image == null) return;

            if (image.Length > 2 * 1024 * 1024)
            {
                throw new ArgumentException("Image file size cannot exceed 2MB.");
            }

            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif", ".webp" };
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Invalid image file type. Allowed types are: " + string.Join(", ", allowedExtensions));
            }
        }
    }
}
