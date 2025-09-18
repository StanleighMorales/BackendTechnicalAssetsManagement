namespace BackendTechnicalAssetsManagement.src.Utils
{
    public class UpdateChecker
    {
        public static void UpdatePropertyIfProvided(ref string existingValue, string? newValue)
        {
            if (!string.IsNullOrEmpty(newValue))
            {
                existingValue = newValue;
            }
        }

        // This generic method works for any nullable value type (like enums, int?, bool?)
        public static void UpdatePropertyIfProvided<T>(ref T existingValue, T? newValue) where T : struct
        {
            if (newValue.HasValue)
            {
                existingValue = newValue.Value;
            }
        }
    }
}
