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
        public static string GenerateLentItemBarcode(string lentItemsId)
        {
            // Add checks for null/empty serialNumber here if necessary
            return LentItemsFormPrefix + lentItemsId;
        }
    }
}
