namespace BackendTechnicalAssetsManagement.src.IService
{
    /// <summary>
    /// Service interface for sending real-time notifications via SignalR
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Send a notification when a lent item status changes from Pending to Approved
        /// </summary>
        Task SendApprovalNotificationAsync(Guid lentItemId, Guid? userId, string itemName, string borrowerName);

        /// <summary>
        /// Send a notification when a lent item status changes
        /// </summary>
        Task SendStatusChangeNotificationAsync(Guid lentItemId, Guid? userId, string itemName, string oldStatus, string newStatus);

        /// <summary>
        /// Send a notification to all connected clients
        /// </summary>
        Task SendBroadcastNotificationAsync(string message, object? data = null);
    }
}
