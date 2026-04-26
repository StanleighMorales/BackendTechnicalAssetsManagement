using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;

namespace BackendTechnicalAssetsManagement.src.Services
{
    /// <summary>
    /// Background service that checks every minute for approved reservations that are
    /// due within the next 15 minutes and fires a SignalR "due soon" notification so
    /// admin/staff can prepare the item and the borrower knows to come pick it up.
    ///
    /// A reservation is only alerted once — the lentItemId is tracked in a HashSet for
    /// the lifetime of the process so the same reservation does not spam notifications.
    ///
    /// TIMEZONE NOTE: Npgsql.EnableLegacyTimestampBehavior is ON, which means all
    /// DateTime values read from PostgreSQL come back as DateTimeKind.Unspecified.
    /// The frontend sends ReservedFor as a local-time string with no timezone suffix
    /// (e.g. "2026-04-25T15:30:00"), so the DB stores local wall-clock time.
    /// We must compare using DateTime.Now (local), NOT DateTime.UtcNow, so both
    /// sides of the comparison are in the same frame of reference.
    /// </summary>
    public class ReservationDueSoonBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReservationDueSoonBackgroundService> _logger;

        // Tracks which reservations have already had a "due soon" notification sent
        // so we don't fire the same alert every minute.
        private readonly HashSet<Guid> _alreadyNotified = new();

        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

        // Warn this far ahead of ReservedFor (15 minutes)
        private readonly TimeSpan _warningWindow = TimeSpan.FromMinutes(15);

        public ReservationDueSoonBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<ReservationDueSoonBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Reservation Due-Soon Background Service is starting.");

            // Run the first check immediately on startup — no delay — so that
            // reservations already inside the 15-minute window are caught right away.
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckDueSoonReservationsAsync();
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while checking due-soon reservations.");
                }

                try
                {
                    await Task.Delay(_checkInterval, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }

            _logger.LogInformation("Reservation Due-Soon Background Service is stopping.");
        }

        private async Task CheckDueSoonReservationsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var lentItemsRepo    = scope.ServiceProvider.GetRequiredService<ILentItemsRepository>();
            var userRepo         = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

            // Use local wall-clock time — ReservedFor is stored as local time (no UTC offset)
            // because the frontend sends "2026-04-25T15:30:00" with no timezone suffix and
            // Npgsql.EnableLegacyTimestampBehavior stores it as-is (Unspecified kind).
            var nowLocal = DateTime.Now;

            // Window: alert for reservations due between (now - 30 min) and (now + 15 min).
            // The trailing 30-minute lookback ensures we still fire if the service was down
            // or the check happened to run just after the reservation time passed.
            var windowStart = nowLocal.AddMinutes(-30);
            var windowEnd   = nowLocal.Add(_warningWindow);

            _logger.LogInformation(
                "Due-soon check: local now={Now:yyyy-MM-dd HH:mm:ss}, window [{Start:HH:mm:ss} – {End:HH:mm:ss}]",
                nowLocal, windowStart, windowEnd);

            // Lightweight query — only scalar fields needed
            var allActive = await lentItemsRepo.GetAllLightAsync();

            var dueSoon = allActive
                .Where(li =>
                {
                    if (li.Status != "Approved" || !li.ReservedFor.HasValue)
                        return false;

                    if (_alreadyNotified.Contains(li.Id))
                        return false;

                    // ReservedFor comes back as Unspecified — compare raw ticks (wall-clock)
                    var reservedForTicks = li.ReservedFor.Value.Ticks;
                    return reservedForTicks >= windowStart.Ticks && reservedForTicks <= windowEnd.Ticks;
                })
                .ToList();

            _logger.LogInformation(
                "Due-soon check complete: {Count} reservation(s) in window.", dueSoon.Count);

            foreach (var reservation in dueSoon)
            {
                _alreadyNotified.Add(reservation.Id);

                // Resolve the borrower's current photo from the Users table.
                // This is always up-to-date even if the denormalized field on LentItems
                // was null at the time the reservation was created.
                string? guestImageUrl = reservation.GuestImageUrl;
                string? frontStudentIdPictureUrl = reservation.FrontStudentIdPictureUrl;

                if (reservation.UserId.HasValue &&
                    string.IsNullOrEmpty(frontStudentIdPictureUrl) &&
                    string.IsNullOrEmpty(guestImageUrl))
                {
                    var user = await userRepo.GetByIdAsync(reservation.UserId.Value);
                    if (user is Student student)
                    {
                        frontStudentIdPictureUrl = student.FrontStudentIdPictureUrl;
                    }
                }

                await notificationService.SendReservationDueSoonNotificationAsync(
                    reservation.Id,
                    reservation.UserId,
                    reservation.ItemName ?? "Unknown Item",
                    reservation.BorrowerFullName ?? "Unknown",
                    reservation.ReservedFor!.Value,
                    reservation.BorrowerRole ?? "Unknown",
                    guestImageUrl,
                    frontStudentIdPictureUrl);

                _logger.LogInformation(
                    "Due-soon alert sent for reservation {Id} — {ItemName} due at {ReservedFor}",
                    reservation.Id, reservation.ItemName, reservation.ReservedFor!.Value);
            }
        }
    }
}
