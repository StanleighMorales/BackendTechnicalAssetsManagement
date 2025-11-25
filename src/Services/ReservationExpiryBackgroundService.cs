using BackendTechnicalAssetsManagement.src.IService;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class ReservationExpiryBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReservationExpiryBackgroundService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(15); // Check every 15 minutes

        public ReservationExpiryBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<ReservationExpiryBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Reservation Expiry Background Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CancelExpiredReservations();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while canceling expired reservations.");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("Reservation Expiry Background Service is stopping.");
        }

        private async Task CancelExpiredReservations()
        {
            using var scope = _serviceProvider.CreateScope();
            var lentItemsService = scope.ServiceProvider.GetRequiredService<ILentItemsService>();

            var canceledCount = await lentItemsService.CancelExpiredReservationsAsync();

            if (canceledCount > 0)
            {
                _logger.LogInformation($"Canceled {canceledCount} expired reservation(s).");
            }
        }
    }
}
