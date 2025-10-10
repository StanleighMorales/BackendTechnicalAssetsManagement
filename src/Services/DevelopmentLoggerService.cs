namespace BackendTechnicalAssetsManagement.src.Services
{
    public interface IDevelopmentLoggerService
    {
        void LogTokenSent(TimeSpan expiryDuration, string tokenType);
        void LogTokenAlmostExpired(string tokenType, TimeSpan expiryDuration, TimeSpan threshold);
        // void LogTokenCountdown(TimeSpan expiryDuration, string tokenType); // <-- Commented out/Removed
    }

    // Implement the service
    public class DevelopmentLoggerService : IDevelopmentLoggerService
    {
        private readonly ILogger<DevelopmentLoggerService> _logger;
        private static readonly TimeSpan LogExpirationTime = TimeSpan.FromSeconds(30); // 30s as requested

        public DevelopmentLoggerService(ILogger<DevelopmentLoggerService> logger)
        {
            _logger = logger;
        }

        // NEW METHOD: Log when a new token is set
        public void LogTokenSent(TimeSpan expiryDuration, string tokenType)
        {
            _logger.LogWarning($"[TOKEN SENT] New {tokenType} token issued. Expires in {expiryDuration.TotalMinutes:F0}m {expiryDuration.Seconds:F0}s.");

            // Immediately start the 'almost expired' warning countdown
            LogTokenAlmostExpired(tokenType, expiryDuration, LogExpirationTime);
        }

        // NEW METHOD: Fire-and-forget task to log 'almost expired' at the correct time
        public void LogTokenAlmostExpired(string tokenType, TimeSpan expiryDuration, TimeSpan threshold)
        {
            // Calculate the delay needed to log the warning
            // If expiry is 15m and warning is 30s before, delay is 14m 30s.
            var delayTime = expiryDuration.Subtract(threshold);

            // Ensure delay time is positive (i.e., token is valid for longer than the warning time)
            if (delayTime <= TimeSpan.Zero)
            {
                _logger.LogWarning($"[TOKEN WARNING] {tokenType} token is valid for less than {threshold.TotalSeconds} seconds and nearing expiry now!");
                return;
            }

            // Fire-and-forget task
            _ = Task.Run(async () =>
            {
                await Task.Delay(delayTime);
                _logger.LogWarning($"[TOKEN WARNING] {tokenType} token will expire in {threshold.TotalSeconds} seconds (at approx. {DateTime.Now.Add(threshold):HH:mm:ss}).");
            });
        }

        // public void LogTokenCountdown(TimeSpan expiryDuration, string tokenType) { ... } // REMOVE OR COMMENT OUT
    }
}