using BackendTechnicalAssetsManagement.src.Hubs;
using BackendTechnicalAssetsManagement.src.IService;
using Microsoft.AspNetCore.SignalR;

namespace BackendTechnicalAssetsManagement.src.Services
{
    /// <summary>
    /// Service for sending real-time notifications via SignalR
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IHubContext<NotificationHub> hubContext, ILogger<NotificationService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        /// <summary>
        /// Send a notification when a lent item status changes from Pending to Approved
        /// </summary>
        public async Task SendApprovalNotificationAsync(Guid lentItemId, Guid? userId, string itemName, string borrowerName)
        {
            try
            {
                var notification = new
                {
                    Type = "approval",
                    LentItemId = lentItemId,
                    ItemName = itemName,
                    BorrowerName = borrowerName,
                    Message = $"Your request for '{itemName}' has been approved!",
                    Timestamp = DateTime.Now
                };

                // Send to specific user if userId is provided
                if (userId.HasValue)
                {
                    await _hubContext.Clients.Group($"user_{userId.Value}")
                        .SendAsync("ReceiveApprovalNotification", notification);
                }

                // Also broadcast to all admins/staff (they might want to see all approvals)
                await _hubContext.Clients.Group("admin_staff")
                    .SendAsync("ReceiveApprovalNotification", notification);

                _logger.LogInformation("Approval notification sent for LentItem {LentItemId}", lentItemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send approval notification for LentItem {LentItemId}", lentItemId);
            }
        }

        /// <summary>
        /// Send a notification when a lent item status changes
        /// </summary>
        public async Task SendStatusChangeNotificationAsync(Guid lentItemId, Guid? userId, string itemName, string oldStatus, string newStatus)
        {
            try
            {
                var notification = new
                {
                    Type = "status_change",
                    LentItemId = lentItemId,
                    ItemName = itemName,
                    OldStatus = oldStatus,
                    NewStatus = newStatus,
                    Message = $"Status changed from {oldStatus} to {newStatus} for '{itemName}'",
                    Timestamp = DateTime.Now
                };

                // Send to specific user if userId is provided
                if (userId.HasValue)
                {
                    await _hubContext.Clients.Group($"user_{userId.Value}")
                        .SendAsync("ReceiveStatusChangeNotification", notification);
                }

                // Also broadcast to all admins/staff
                await _hubContext.Clients.Group("admin_staff")
                    .SendAsync("ReceiveStatusChangeNotification", notification);

                _logger.LogInformation("Status change notification sent for LentItem {LentItemId}: {OldStatus} -> {NewStatus}", 
                    lentItemId, oldStatus, newStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send status change notification for LentItem {LentItemId}", lentItemId);
            }
        }

        /// <summary>
        /// Send a notification to all connected clients
        /// </summary>
        public async Task SendBroadcastNotificationAsync(string message, object? data = null)
        {
            try
            {
                var notification = new
                {
                    Type = "broadcast",
                    Message = message,
                    Data = data,
                    Timestamp = DateTime.Now
                };

                await _hubContext.Clients.All.SendAsync("ReceiveBroadcastNotification", notification);

                _logger.LogInformation("Broadcast notification sent: {Message}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send broadcast notification");
            }
        }
    }
}
