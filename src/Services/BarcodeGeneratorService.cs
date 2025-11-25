using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp;

namespace BackendTechnicalAssetsManagement.src.Services
{
    /// <summary>
    /// Unified barcode service that handles both barcode text generation and barcode image generation.
    /// This consolidates the logic previously split between BarcodeGenerator and BarcodeImageUtil.
    /// </summary>
    public class BarcodeGeneratorService : IBarcodeGeneratorService
    {
        private readonly ILentItemsRepository _lentItemsRepository;
        private const string ItemPrefix = "ITEM-";
        private const string LentItemsFormPrefix = "LENT-";

        // Static flag to skip barcode image generation (for tests)
        // This is a performance optimization - barcode image generation takes ~400-700ms using SkiaSharp
        public static bool SkipImageGeneration { get; set; } = false;

        public BarcodeGeneratorService(ILentItemsRepository lentItemsRepository)
        {
            _lentItemsRepository = lentItemsRepository;
        }

        /// <summary>
        /// Generates a barcode text for items using the format: ITEM-{serialNumber}
        /// </summary>
        public string GenerateItemBarcode(string serialNumber)
        {
            return ItemPrefix + serialNumber;
        }

        /// <summary>
        /// Generates a barcode for lent items using the format: LENT-YYYYMMDD-XXX
        /// where XXX is a 3-digit daily sequence number starting from 001
        /// </summary>
        public async Task<string> GenerateLentItemBarcodeAsync(DateTime? date = null)
        {
            var targetDate = date ?? DateTime.Now.Date;
            var dateString = targetDate.ToString("yyyyMMdd");
            var barcodePrefix = $"{LentItemsFormPrefix}{dateString}-";

            // Get DbContext from repository
            var dbContext = _lentItemsRepository.GetDbContext();

            // Find the highest sequence number for today
            var existingBarcodes = await dbContext.LentItems
                .Where(li => li.Barcode != null && li.Barcode.StartsWith(barcodePrefix))
                .Select(li => li.Barcode)
                .ToListAsync();

            var maxSequence = 0;
            foreach (var barcode in existingBarcodes)
            {
                if (barcode != null && barcode.Length >= barcodePrefix.Length + 3)
                {
                    var sequencePart = barcode.Substring(barcodePrefix.Length);
                    if (int.TryParse(sequencePart, out var sequence))
                    {
                        maxSequence = Math.Max(maxSequence, sequence);
                    }
                }
            }

            var nextSequence = maxSequence + 1;
            var sequenceString = nextSequence.ToString("D3");

            return $"{barcodePrefix}{sequenceString}";
        }

        /// <summary>
        /// Generates a Code 128 barcode image as a PNG byte array for the given text.
        /// Returns null if text is invalid or if SkipImageGeneration flag is set (for testing).
        /// </summary>
        public byte[]? GenerateBarcodeImage(string barcodeText)
        {
            return GenerateBarcodeImageStatic(barcodeText);
        }

        #region Static Helper Methods (for seed data and static contexts)

        /// <summary>
        /// Static helper method to generate item barcode text.
        /// Used by seed data and other static contexts where DI is not available.
        /// </summary>
        public static string GenerateItemBarcodeStatic(string serialNumber)
        {
            return ItemPrefix + serialNumber;
        }

        /// <summary>
        /// Static helper method to generate barcode image.
        /// Used by seed data and other static contexts where DI is not available.
        /// </summary>
        public static byte[]? GenerateBarcodeImageStatic(string barcodeText)
        {
            if (string.IsNullOrEmpty(barcodeText))
            {
                return null;
            }

            // PERFORMANCE OPTIMIZATION: Skip expensive image generation in tests
            // Tests don't need actual images, just the barcode text
            if (SkipImageGeneration)
            {
                return null; // Return null in tests, image will be generated on-demand in production
            }

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 150,
                    Width = 300,
                    Margin = 10
                }
            };

            var skBitmap = writer.Write(barcodeText);

            using (var data = skBitmap.Encode(SKEncodedImageFormat.Png, 80))
            {
                return data.ToArray();
            }
        }

        #endregion
    }
}
