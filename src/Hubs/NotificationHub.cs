using Microsoft.AspNetCore.SignalR;

namespace BackendTechnicalAssetsManagement.src.Hubs
{
    /// <summary>
    /// SignalR Hub for real-time notifications
    /// Handles live updates for status changes, approvals, and other events
    /// </summary>
    public class NotificationHub : Hub
    {
        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called when a client connects to the hub
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Called when a client disconnects from the hub
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Allow users to join a specific group (e.g., by userId)
        /// This enables targeted notifications to specific users
        /// </summary>
        public async Task JoinUserGroup(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
            _logger.LogInformation("User {UserId} joined group user_{UserId}", userId, userId);
        }

        /// <summary>
        /// Allow users to leave a specific group
        /// </summary>
        public async Task LeaveUserGroup(string userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
            _logger.LogInformation("User {UserId} left group user_{UserId}", userId, userId);
        }

        /// <summary>
        /// Allow admin/staff to join the admin_staff group for receiving pending requests
        /// </summary>
        public async Task JoinAdminStaffGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "admin_staff");
            _logger.LogInformation("Connection {ConnectionId} joined admin_staff group", Context.ConnectionId);
        }

        /// <summary>
        /// Leave the admin_staff group
        /// </summary>
        public async Task LeaveAdminStaffGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "admin_staff");
            _logger.LogInformation("Connection {ConnectionId} left admin_staff group", Context.ConnectionId);
        }
    }
}
