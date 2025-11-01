using BackendTechnicalAssetsManagement.src.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Utils
{
    public class BarcodeGenerator
    {
        private const string ItemPrefix = "ITEM-";
        private const string LentItemsFormPrefix = "LENT-";

        public static string GenerateItemBarcode(string serialNumber)
        {
            // Add checks for null/empty serialNumber here if necessary
            return ItemPrefix + serialNumber;
        }

        /// <summary>
        /// Generates a barcode for lent items using the format: LENT-YYYYMMDD-XXX
        /// where XXX is a 3-digit daily sequence number starting from 001
        /// </summary>
        /// <param name="dbContext">Database context to check existing barcodes</param>
        /// <param name="date">The date for the barcode (defaults to today)</param>
        /// <returns>Generated barcode string</returns>
        public static async Task<string> GenerateLentItemBarcode(AppDbContext dbContext, DateTime? date = null)
        {
            var targetDate = date ?? DateTime.UtcNow.Date;
            var dateString = targetDate.ToString("yyyyMMdd");
            var barcodePrefix = $"{LentItemsFormPrefix}{dateString}-";

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
            var sequenceString = nextSequence.ToString("D3"); // 3-digit format with leading zeros

            return $"{barcodePrefix}{sequenceString}";
        }
    }
}
