# SignalR Live Notification System

## Overview
This system provides real-time notifications when a lent item's status changes from **Pending** to **Approved** using SignalR.

## Components Created

### 1. NotificationHub (`src/Hubs/NotificationHub.cs`)
- SignalR hub that manages WebSocket connections
- Supports user-specific groups for targeted notifications
- Endpoint: `/notificationHub`

### 2. NotificationService (`src/Services/NotificationService.cs`)
- Service for sending notifications through SignalR
- Methods:
  - `SendApprovalNotificationAsync()` - Sends approval notifications
  - `SendStatusChangeNotificationAsync()` - Sends general status change notifications
  - `SendBroadcastNotificationAsync()` - Broadcasts to all connected clients

### 3. Integration in LentItemsService
- Automatically sends notifications when status changes from Pending to Approved
- Integrated in both `UpdateAsync()` and `UpdateStatusAsync()` methods

## Client Connection Examples

### JavaScript/TypeScript (React, Vue, Angular)

```bash
npm install @microsoft/signalr
```

```javascript
import * as signalR from "@microsoft/signalr";

// Create connection
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://your-api-url.com/notificationHub", {
        accessTokenFactory: () => yourJwtToken // Optional: for authenticated connections
    })
    .withAutomaticReconnect()
    .build();

// Listen for approval notifications
connection.on("ReceiveApprovalNotification", (notification) => {
    console.log("Approval received:", notification);
    // notification structure:
    // {
    //   Type: "approval",
    //   LentItemId: "guid",
    //   ItemName: "string",
    //   BorrowerName: "string",
    //   Message: "Your request for 'ItemName' has been approved!",
    //   Timestamp: "datetime"
    // }
    
    // Show toast/alert to user
    alert(notification.Message);
});

// Listen for status change notifications
connection.on("ReceiveStatusChangeNotification", (notification) => {
    console.log("Status changed:", notification);
    // notification structure:
    // {
    //   Type: "status_change",
    //   LentItemId: "guid",
    //   ItemName: "string",
    //   OldStatus: "string",
    //   NewStatus: "string",
    //   Message: "Status changed from X to Y for 'ItemName'",
    //   Timestamp: "datetime"
    // }
});

// Listen for broadcast notifications
connection.on("ReceiveBroadcastNotification", (notification) => {
    console.log("Broadcast:", notification);
});

// Start connection
connection.start()
    .then(() => {
        console.log("SignalR Connected!");
        
        // Join user-specific group (optional, for targeted notifications)
        const userId = "your-user-id-guid";
        connection.invoke("JoinUserGroup", userId);
    })
    .catch(err => console.error("SignalR Connection Error:", err));

// Handle disconnection
connection.onclose(() => {
    console.log("SignalR Disconnected");
});
```

### Flutter/Dart

```yaml
# pubspec.yaml
dependencies:
  signalr_netcore: ^1.3.3
```

```dart
import 'package:signalr_netcore/signalr_client.dart';

// Create connection
final hubConnection = HubConnectionBuilder()
    .withUrl(
      "https://your-api-url.com/notificationHub",
      options: HttpConnectionOptions(
        accessTokenFactory: () async => yourJwtToken, // Optional
      ),
    )
    .withAutomaticReconnect()
    .build();

// Listen for approval notifications
hubConnection.on("ReceiveApprovalNotification", (arguments) {
  final notification = arguments?[0];
  print("Approval received: $notification");
  
  // Show notification to user
  // Use flutter_local_notifications or similar
});

// Listen for status change notifications
hubConnection.on("ReceiveStatusChangeNotification", (arguments) {
  final notification = arguments?[0];
  print("Status changed: $notification");
});

// Start connection
await hubConnection.start();

// Join user-specific group
final userId = "your-user-id-guid";
await hubConnection.invoke("JoinUserGroup", args: [userId]);
```

### C# (.NET Client)

```bash
dotnet add package Microsoft.AspNetCore.SignalR.Client
```

```csharp
using Microsoft.AspNetCore.SignalR.Client;

// Create connection
var connection = new HubConnectionBuilder()
    .WithUrl("https://your-api-url.com/notificationHub", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(yourJwtToken); // Optional
    })
    .WithAutomaticReconnect()
    .Build();

// Listen for approval notifications
connection.On<object>("ReceiveApprovalNotification", (notification) =>
{
    Console.WriteLine($"Approval received: {notification}");
});

// Listen for status change notifications
connection.On<object>("ReceiveStatusChangeNotification", (notification) =>
{
    Console.WriteLine($"Status changed: {notification}");
});

// Start connection
await connection.StartAsync();

// Join user-specific group
var userId = "your-user-id-guid";
await connection.InvokeAsync("JoinUserGroup", userId);
```

## Testing the System

### 1. Start the API
```bash
dotnet run
```

### 2. Connect a Client
Use one of the client examples above to connect to `/notificationHub`

### 3. Trigger a Status Change
Make a PUT request to update a lent item's status from "Pending" to "Approved":

```bash
curl -X PUT https://your-api-url.com/api/v1/lentitems/{id} \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "status": "Approved"
  }'
```

### 4. Observe the Notification
The connected client should receive a real-time notification with the approval details.

## CORS Configuration
The existing CORS policy in `Program.cs` already allows SignalR connections from:
- localhost (any port)
- 127.0.0.1 (any port)
- Android emulator (10.0.2.2)
- Configured production origins

## Security Considerations

### 1. Authentication (Optional)
To require authentication for SignalR connections, add to `NotificationHub.cs`:

```csharp
[Authorize] // Requires JWT authentication
public class NotificationHub : Hub
{
    // ...
}
```

### 2. User-Specific Notifications
The system supports user-specific groups:
- Clients join their user group: `JoinUserGroup(userId)`
- Notifications are sent to: `user_{userId}` group
- Only the specific user receives their notifications

### 3. Admin/Staff Notifications
All approval notifications are also sent to the `admin_staff` group for monitoring.

## Notification Event Types

| Event Name | Description | When Triggered |
|------------|-------------|----------------|
| `ReceiveApprovalNotification` | Item request approved | Status: Pending → Approved |
| `ReceiveStatusChangeNotification` | Any status change | Any status update |
| `ReceiveBroadcastNotification` | System-wide message | Manual broadcast |

## Troubleshooting

### Connection Issues
1. Check CORS settings in `appsettings.json`
2. Verify the hub URL: `/notificationHub`
3. Check firewall/network settings
4. Ensure WebSocket support is enabled

### No Notifications Received
1. Verify client is connected: `connection.state === "Connected"`
2. Check if client joined the correct user group
3. Verify the status change is from "Pending" to "Approved"
4. Check server logs for notification sending

### Performance
- SignalR automatically manages connections
- Reconnection is automatic with `withAutomaticReconnect()`
- Consider scaling with Azure SignalR Service for high traffic

## Next Steps

1. **Add UI Notifications**: Integrate with toast/notification libraries
2. **Notification History**: Store notifications in database for later viewing
3. **Push Notifications**: Combine with Firebase/APNs for mobile push
4. **Email Notifications**: Send email alongside SignalR notification
5. **Notification Preferences**: Let users configure notification settings
