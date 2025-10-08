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
        [HttpGet("{text}")]

        public IActionResult GenerateBarcode(string text)
        {
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

            var skBitmap = writer.Write(text);

            using (var data = skBitmap.Encode(SKEncodedImageFormat.Png, 80))
            {
                var imageBytes = data.ToArray();
                return File(imageBytes, "image/png");
            }
        }
    }
}
