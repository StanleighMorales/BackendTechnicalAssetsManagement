using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BackendTechnicalAssetsManagement.src.Utils
{
    public class ImageFileManager
        
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageFileManager(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public void ValidateImage(IFormFile? image)
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
        public async Task<byte[]> GetImageBytesAsync(IFormFile imageFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            // 1. Generate a unique filename to prevent overwriting files
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            // 2. Define the path where the image will be saved
            //    _hostEnvironment.WebRootPath points to the 'wwwroot' folder
            //    We will save images in a subfolder called 'images'
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);

            // 3. Save the file to the server
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            // 4. Return the unique filename to be stored in the database
            return fileName;
        }

        public void DeleteImage(string fileName)
        {
            // Define the path to the image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);

            // Check if the file exists before trying to delete it
            if (File.Exists(imagePath))
            {
                try
                {
                    File.Delete(imagePath);
                }
                catch (IOException ex)
                {
                    // Log the error, but don't crash the application
                    // This can happen if the file is in use, etc.
                    Console.WriteLine($"Error deleting file {fileName}: {ex.Message}");
                }
            }
        }
    }
}
