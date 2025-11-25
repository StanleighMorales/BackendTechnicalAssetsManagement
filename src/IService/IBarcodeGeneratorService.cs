namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IBarcodeGeneratorService
    {
        /// <summary>
        /// Generates a barcode text for lent items using the format: LENT-YYYYMMDD-XXX
        /// </summary>
        Task<string> GenerateLentItemBarcodeAsync(DateTime? date = null);
        
        /// <summary>
        /// Generates a barcode text for items using the format: ITEM-{serialNumber}
        /// </summary>
        string GenerateItemBarcode(string serialNumber);
        
        /// <summary>
        /// Generates a Code 128 barcode image as a PNG byte array for the given text.
        /// Returns null if text is invalid or if SkipImageGeneration flag is set (for testing).
        /// </summary>
        byte[]? GenerateBarcodeImage(string barcodeText);
    }
}
