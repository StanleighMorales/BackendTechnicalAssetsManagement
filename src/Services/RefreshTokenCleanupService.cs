using BackendTechnicalAssetsManagement.src.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.BackgroundServices
{
    /// <summary>
    /// A long-running background service responsible for periodically cleaning up the RefreshTokens table.
    /// It runs independently of any HTTP request and ensures that the database does not get cluttered
    /// with old, expired, or revoked tokens, which improves performance and hygiene.
    /// It inherits from BackgroundService, the standard ASP.NET Core base class for hosted services.
    /// </summary>
    public class RefreshTokenCleanupService : BackgroundService
    {
        // ILogger is used for writing log messages, which is essential for monitoring
        // the health and activity of a background service.
        private readonly ILogger<RefreshTokenCleanupService> _logger;

        // IServiceProvider is a core dependency injection container. We need it here to
        // manually create a 'scope' for resolving 'scoped' services like AppDbContext,
        // because this background service itself is a long-lived 'singleton'.
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenCleanupService"/> class.
        /// </summary>
        /// <param name="logger">The logger instance provided by dependency injection.</param>
        /// <param name="serviceProvider">The service provider instance provided by dependency injection.</param>
        public RefreshTokenCleanupService(ILogger<RefreshTokenCleanupService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// This method is called once by the ASP.NET Core host when the application starts.
        /// It contains the main logic for the background task.
        /// </summary>
        /// <param name="stoppingToken">A CancellationToken that is triggered when the application is requested to shut down.</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Refresh Token Cleanup Service is starting.");

            // Delay the first run by 2 minutes so the app finishes starting up
            // before we open a DB connection, avoiding connection pressure at startup.
            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Running refresh token cleanup task.");

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        // Single bulk DELETE — no rows loaded into memory
                        var deletedCount = await dbContext.RefreshTokens
                            .Where(rt => rt.ExpiresAt <= DateTime.UtcNow || rt.IsRevoked)
                            .ExecuteDeleteAsync(stoppingToken);

                        if (deletedCount > 0)
                            _logger.LogInformation($"Removed {deletedCount} old refresh tokens.");
                        else
                            _logger.LogInformation("No old refresh tokens to remove.");
                    }

                    // Run once every 24 hours
                    await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred in the Refresh Token Cleanup Service.");

                    // Back off for 5 minutes before retrying after an error
                    // to avoid hammering the DB when it's under pressure.
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
            }

            _logger.LogInformation("Refresh Token Cleanup Service is stopping gracefully.");
        }
    }
}
