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
        /// Send a notification when a new pending request is created
        /// Notifies admin/staff about new requests in the Pending & Reservation tab
        /// </summary>
        public async Task SendNewPendingRequestNotificationAsync(Guid lentItemId, string itemName, string borrowerName, DateTime? reservedFor)
        {
            try
            {
                var notification = new
                {
                    Type = "new_pending_request",
                    LentItemId = lentItemId,
                    ItemName = itemName,
                    BorrowerName = borrowerName,
                    ReservedFor = reservedFor,
                    Message = $"New request from {borrowerName} for '{itemName}'",
                    Timestamp = DateTime.Now
                };

                // Send to all admin/staff members
                await _hubContext.Clients.Group("admin_staff")
                    .SendAsync("ReceiveNewPendingRequest", notification);

                _logger.LogInformation("New pending request notification sent for LentItem {LentItemId} by {BorrowerName}", 
                    lentItemId, borrowerName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send new pending request notification for LentItem {LentItemId}", lentItemId);
            }
        }

        /// <summary>
        /// Send a notification when an item is borrowed instantly via RFID scan.
        /// Notifies the borrower (confirmation) and admin/staff (awareness).
        /// </summary>
        public async Task SendItemBorrowedNotificationAsync(Guid lentItemId, Guid? userId, string itemName, string borrowerName)
        {
            try
            {
                var notification = new
                {
                    Type = "item_borrowed",
                    LentItemId = lentItemId,
                    ItemName = itemName,
                    BorrowerName = borrowerName,
                    Message = $"You have successfully borrowed '{itemName}'.",
                    Timestamp = DateTime.Now
                };

                // Confirm to the borrower
                if (userId.HasValue)
                {
                    await _hubContext.Clients.Group($"user_{userId.Value}")
                        .SendAsync("ReceiveItemBorrowed", notification);
                }

                // Notify admin/staff so they're aware of the new borrow
                await _hubContext.Clients.Group("admin_staff")
                    .SendAsync("ReceiveItemBorrowed", notification);

                _logger.LogInformation("Item borrowed notification sent for LentItem {LentItemId} by {BorrowerName}",
                    lentItemId, borrowerName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send item borrowed notification for LentItem {LentItemId}", lentItemId);
            }
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

        /// <summary>
        /// Send a notification to admin/staff when an approved reservation is due within
        /// the next 15 minutes, so they can prepare the item for pickup.
        /// Also notifies the borrower so they know to come pick up the item.
        /// </summary>
        public async Task SendReservationDueSoonNotificationAsync(Guid lentItemId, Guid? userId, string itemName, string borrowerName, DateTime reservedFor, string borrowerRole, string? guestImageUrl, string? frontStudentIdPictureUrl)
        {
            try
            {
                var notification = new
                {
                    Type = "reservation_due_soon",
                    LentItemId = lentItemId,
                    ItemName = itemName,
                    BorrowerName = borrowerName,
                    BorrowerRole = borrowerRole,
                    GuestImageUrl = guestImageUrl,
                    FrontStudentIdPictureUrl = frontStudentIdPictureUrl,
                    ReservedFor = reservedFor,
                    Message = $"{borrowerName}'s reservation for '{itemName}' is due at {reservedFor.ToLocalTime():h:mm tt}. Please prepare the item for pickup.",
                    Timestamp = DateTime.UtcNow
                };

                // Notify admin/staff so they can prepare the item
                await _hubContext.Clients.Group("admin_staff")
                    .SendAsync("ReceiveReservationDueSoon", notification);

                // Also notify the borrower so they know to come pick up
                if (userId.HasValue)
                {
                    var userNotification = new
                    {
                        Type = "reservation_due_soon",
                        LentItemId = lentItemId,
                        ItemName = itemName,
                        BorrowerName = borrowerName,
                        ReservedFor = reservedFor,
                        Message = $"Your reservation for '{itemName}' is due at {reservedFor.ToLocalTime():h:mm tt}. Please come to pick it up.",
                        Timestamp = DateTime.UtcNow
                    };

                    await _hubContext.Clients.Group($"user_{userId.Value}")
                        .SendAsync("ReceiveReservationDueSoon", userNotification);
                }

                _logger.LogInformation(
                    "Reservation due-soon notification sent for LentItem {LentItemId} (user: {UserId}, item: {ItemName}, due: {ReservedFor})",
                    lentItemId, userId, itemName, reservedFor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send reservation due-soon notification for LentItem {LentItemId}", lentItemId);
            }
        }

        /// <summary>
        /// Send a notification to a user when their reservation expired because they did not
        /// pick up the item within the allowed time window.
        /// Also notifies admin/staff so they are aware the item is back to available.
        /// </summary>
        public async Task SendReservationExpiredNotificationAsync(Guid lentItemId, Guid? userId, string itemName, string borrowerName, DateTime reservedFor)
        {
            try
            {
                var notification = new
                {
                    Type = "reservation_expired",
                    LentItemId = lentItemId,
                    ItemName = itemName,
                    BorrowerName = borrowerName,
                    ReservedFor = reservedFor,
                    Message = $"Your reservation for '{itemName}' has expired because it was not picked up in time. The item is now available again.",
                    Timestamp = DateTime.UtcNow
                };

                // Notify the specific user who made the reservation
                if (userId.HasValue)
                {
                    await _hubContext.Clients.Group($"user_{userId.Value}")
                        .SendAsync("ReceiveReservationExpired", notification);
                }

                // Also notify admin/staff so they know the item is back to available
                await _hubContext.Clients.Group("admin_staff")
                    .SendAsync("ReceiveReservationExpired", notification);

                _logger.LogInformation(
                    "Reservation expired notification sent for LentItem {LentItemId} (user: {UserId}, item: {ItemName})",
                    lentItemId, userId, itemName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send reservation expired notification for LentItem {LentItemId}", lentItemId);
            }
        }
    }
}
