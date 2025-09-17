namespace BackendTechnicalAssetsManagement.src.Utils
{
    public class ImageConverterUtils
    {
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
    }
}
