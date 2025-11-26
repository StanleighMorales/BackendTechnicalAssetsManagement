using Microsoft.AspNetCore.SignalR;

namespace BackendTechnicalAssetsManagement.src.Hubs
{
    /// <summary>
    /// SignalR Hub for real-time notifications
    /// Handles live updates for status changes, approvals, and other events
    /// </summary>
    public class NotificationHub : Hub
    {
        /// <summary>
        /// Called when a client connects to the hub
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Called when a client disconnects from the hub
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Allow users to join a specific group (e.g., by userId)
        /// This enables targeted notifications to specific users
        /// </summary>
        public async Task JoinUserGroup(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
        }

        /// <summary>
        /// Allow users to leave a specific group
        /// </summary>
        public async Task LeaveUserGroup(string userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
        }
    }
}
