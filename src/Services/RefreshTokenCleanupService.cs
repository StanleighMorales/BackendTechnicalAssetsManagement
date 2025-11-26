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

            // This loop runs for the entire lifetime of the application. It will only exit
            // when the stoppingToken is cancelled (i.e., when the application is shutting down).
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Running refresh token cleanup task.");

                    // IMPORTANT: We must create a new 'scope' to resolve our DbContext.
                    // AppDbContext is a 'scoped' service, meaning it's designed to live for a short time (like an HTTP request).
                    // This background service is a 'singleton' (lives forever). To safely use a scoped service inside a
                    // singleton, we create a new scope for each unit of work.
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        // Safely resolve a new instance of AppDbContext from our temporary scope.
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        // Build a query to find all refresh tokens that meet the criteria for deletion:
                        // 1. The token's expiration date is in the past (it's expired).
                        // 2. The token has been manually revoked (e.g., via logout or token rotation).
                        var tokensToRemove = await dbContext.RefreshTokens
                            .Where(rt => rt.ExpiresAt <= DateTime.UtcNow || rt.IsRevoked)
                            .ToListAsync(stoppingToken);

                        // Check if the query found any tokens to delete.
                        if (tokensToRemove.Any())
                        {
                            // Use RemoveRange for an efficient bulk-delete operation.
                            dbContext.RefreshTokens.RemoveRange(tokensToRemove);
                            // Commit the changes to the database.
                            await dbContext.SaveChangesAsync(stoppingToken);

                            _logger.LogInformation($"Removed {tokensToRemove.Count} old refresh tokens.");
                        }
                        else
                        {
                            _logger.LogInformation("No old refresh tokens to remove.");
                        }
                    } // The scope (and the DbContext instance) is automatically disposed of here.

                    // Wait 24 hours before the next cleanup cycle
                    await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    // This exception is expected when the application is shutting down. The Task.Delay is cancelled
                    // by the 'stoppingToken', which throws this exception. We catch it and break the loop
                    // to allow for a graceful shutdown.
                    break;
                }
                catch (Exception ex)
                {
                    // This is a safety net. If any other unexpected error occurs during the cleanup process,
                    // we log it and the loop will continue on its next cycle. This prevents the entire
                    // background service from crashing due to a transient database error, for example.
                    _logger.LogError(ex, "An error occurred in the Refresh Token Cleanup Service.");
                }
            }

            _logger.LogInformation("Refresh Token Cleanup Service is stopping gracefully.");
        }
    }
}
