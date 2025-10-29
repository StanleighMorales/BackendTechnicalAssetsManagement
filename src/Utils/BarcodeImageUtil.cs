using SkiaSharp;
using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp;

namespace BackendTechnicalAssetsManagement.src.Utils
{
    public class BarcodeImageUtil
    {
        /// <summary>
        /// Generates a Code 128 barcode image as a PNG byte array for the given text.
        /// </summary>
        /// <param name="text">The data to encode in the barcode (e.g., "ITEM-SN-12345").</param>
        /// <returns>A byte array representing the PNG image, or null if text is invalid.</returns>
        public static byte[]? GenerateBarcodeImageBytes(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
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

            var skBitmap = writer.Write(text);

            using (var data = skBitmap.Encode(SKEncodedImageFormat.Png, 80))
            {
                return data.ToArray();
            }
        }
    }
}
