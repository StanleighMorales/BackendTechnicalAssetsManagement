using BackendTechnicalAssetsManagement.src.IValidations;
using Microsoft.Extensions.Hosting;

namespace BackendTechnicalAssetsManagement.src.Validations
{
    
    public class SaveImageWithValidationAsync : ISaveImageWithValidationAsync
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public SaveImageWithValidationAsync (IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public async Task<string> SaveNewImageWithValidationAsync(IFormFile image)
        {
            if (image == null || image.Length == 0) return null;
            if (image.Length > 2 * 1024 * 1024) throw new ArgumentException("Image file size cannot exceed 2MB");

            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif", ".webp" };
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Invalid image file type.");
            }

            if (string.IsNullOrEmpty(_hostEnvironment.WebRootPath))
            {
                throw new InvalidOperationException("wwwroot folder is not configured.");
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "items");
            Directory.CreateDirectory(uploadsFolder);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return Path.Combine("images", "items", uniqueFileName).Replace('\\', '/');
        }
    }
}
