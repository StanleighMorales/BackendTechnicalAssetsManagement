using BackendTechnicalAssetsManagement.src.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp;

namespace BackendTechnicalAssetsManagement.src.Controllers
{
    [ApiController]
    [Route("api/v1/barcodes")]
    [Authorize]
    public class BarcodeController : Controller
    {
        private readonly IItemService _itemService;
        public BarcodeController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("{barcodeText}")]
        public async Task<IActionResult> GenerateBarcode(string barcodeText)
        {
            // 1. Look up the item using the barcode text value
            // This returns an ItemDto which contains the BarcodeImage as a Base64 STRING
            var itemDto = await _itemService.GetItemByBarcodeAsync(barcodeText);

            if (itemDto == null || string.IsNullOrEmpty(itemDto.BarcodeImage))
            {
                // Note: If you get here, the lookup failed or the image data is missing
                return NotFound($"Barcode image data is missing or item not found for barcode '{barcodeText}'.");
            }

            // 2. Decode the Base64 string back into raw byte[] data
            string base64String = itemDto.BarcodeImage;

            // Remove the 'data:image/png;base64,' prefix added by AutoMapper (if it exists)
            const string prefix = "data:image/png;base64,";
            if (base64String.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                base64String = base64String.Substring(prefix.Length);
            }

            byte[] imageBytes;
            try
            {
                // Convert the pure Base64 part back to byte array
                imageBytes = Convert.FromBase64String(base64String);
            }
            // FIX: Explicitly use System.FormatException
            catch (System.FormatException)
            {
                return BadRequest("Stored BarcodeImage data is corrupt (not valid Base64).");
            }

            // 3. Return the byte array as a PNG file
            // The File() method accepts a byte[] and a MIME type
            return File(imageBytes, "image/png");
        }
    }
}
