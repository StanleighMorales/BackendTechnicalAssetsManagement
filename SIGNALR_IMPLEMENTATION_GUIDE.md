# SignalR Real-Time Notifications - Complete Implementation Guide

## Overview

This system uses **ONE SignalR Hub** (`NotificationHub`) that both your **Web App** and **Mobile App** connect to for real-time notifications.

## How It Works

### Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                         SignalR Hub                              │
│                    /notificationHub                              │
│                                                                   │
│  Groups:                                                          │
│  • user_{userId}     → Individual users (students)               │
│  • admin_staff       → All admins and staff                      │
└─────────────────────────────────────────────────────────────────┘
         ↑                                    ↑
         │                                    │
    ┌────┴────┐                          ┌───┴────┐
    │  Web    │                          │ Mobile │
    │  App    │                          │  App   │
    │ (Admin) │                          │(Student)│
    └─────────┘                          └────────┘
```

### Notification Flow

#### Scenario 1: Student Creates Request (Mobile → Web)

```
1. Student (Mobile App)
   ↓
   POST /api/v1/lentItems
   {
     "itemId": "...",
     "userId": "student-123",
     "status": "Pending"
   }
   ↓
2. Backend (LentItemsService.AddAsync)
   • Creates lent item
   • Calls: _notificationService.SendNewPendingRequestNotificationAsync()
   ↓
3. NotificationService
   • Sends to group: "admin_staff"
   • Event: "ReceiveNewPendingRequest"
   ↓
4. Web App (Admin/Staff)
   • Receives notification
   • Shows alert: "New request from John Doe for 'Laptop'"
   • Updates Pending & Reservation tab UI
```

#### Scenario 2: Admin Approves Request (Web → Mobile)

```
1. Admin (Web App)
   ↓
   PATCH /api/v1/lentItems/{id}
   {
     "lentItemsStatus": "Approved"
   }
   ↓
2. Backend (LentItemsService.UpdateStatusAsync)
   • Updates status: Pending → Approved
   • Calls: _notificationService.SendApprovalNotificationAsync()
   ↓
3. NotificationService
   • Sends to group: "user_{student-123}"
   • Event: "ReceiveApprovalNotification"
   ↓
4. Mobile App (Student)
   • Receives notification
   • Shows alert: "Your request for 'Laptop' has been approved!"
   • Updates UI
```

## Backend Implementation

### 1. NotificationHub (Already Implemented)

**File:** `src/Hubs/NotificationHub.cs`

**Available Methods:**
- `JoinUserGroup(userId)` - Join user-specific group
- `LeaveUserGroup(userId)` - Leave user-specific group
- `JoinAdminStaffGroup()` - Join admin/staff group (for web app)
- `LeaveAdminStaffGroup()` - Leave admin/staff group

### 2. NotificationService (Already Implemented)

**File:** `src/Services/NotificationService.cs`

**Available Methods:**

#### `SendNewPendingRequestNotificationAsync()`
- **When:** Student creates a new request
- **Sends to:** `admin_staff` group
- **Event:** `ReceiveNewPendingRequest`
- **Data:**
  ```json
  {
    "type": "new_pending_request",
    "lentItemId": "guid",
    "itemName": "Laptop",
    "borrowerName": "John Doe",
    "reservedFor": "2025-11-27T10:00:00",
    "message": "New request from John Doe for 'Laptop'",
    "timestamp": "2025-11-26T14:30:00"
  }
  ```

#### `SendApprovalNotificationAsync()`
- **When:** Admin approves a pending request
- **Sends to:** `user_{userId}` group AND `admin_staff` group
- **Event:** `ReceiveApprovalNotification`
- **Data:**
  ```json
  {
    "type": "approval",
    "lentItemId": "guid",
    "itemName": "Laptop",
    "borrowerName": "John Doe",
    "message": "Your request for 'Laptop' has been approved!",
    "timestamp": "2025-11-26T14:35:00"
  }
  ```

#### `SendStatusChangeNotificationAsync()`
- **When:** Any status change (Borrowed, Returned, etc.)
- **Sends to:** `user_{userId}` group AND `admin_staff` group
- **Event:** `ReceiveStatusChangeNotification`
- **Data:**
  ```json
  {
    "type": "status_change",
    "lentItemId": "guid",
    "itemName": "Laptop",
    "oldStatus": "Approved",
    "newStatus": "Borrowed",
    "message": "Status changed from Approved to Borrowed for 'Laptop'",
    "timestamp": "2025-11-26T15:00:00"
  }
  ```

### 3. Integration in LentItemsService

**When request is created:**
```csharp
// In AddAsync() method
await _notificationService.SendNewPendingRequestNotificationAsync(
    lentItem.Id,
    lentItem.ItemName ?? "Unknown Item",
    lentItem.BorrowerFullName ?? "Unknown Borrower",
    lentItem.ReservedFor
);
```

**When request is approved:**
```csharp
// In UpdateStatusAsync() method
if (entity.Status?.Equals("Pending", StringComparison.OrdinalIgnoreCase) == true)
{
    await _notificationService.SendApprovalNotificationAsync(
        entity.Id,
        entity.UserId,
        entity.ItemName ?? item.ItemName,
        entity.BorrowerFullName ?? "Unknown"
    );
}
```

## Frontend Implementation

### Web App (React)

#### 1. Create SignalR Service

**File:** `src/services/signalrService.ts`

```typescript
import * as signalR from '@microsoft/signalr';

class SignalRService {
  private connection: signalR.HubConnection | null = null;

  async connect(token: string): Promise<void> {
    const apiUrl = import.meta.env.VITE_API_URL || 'http://localhost:5278';
    
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${apiUrl}/notificationHub`, {
        accessTokenFactory: () => token,
      })
      .withAutomaticReconnect()
      .build();

    await this.connection.start();
  }

  async joinAdminStaffGroup(): Promise<void> {
    await this.connection?.invoke('JoinAdminStaffGroup');
  }

  async joinUserGroup(userId: string): Promise<void> {
    await this.connection?.invoke('JoinUserGroup', userId);
  }

  on(eventName: string, callback: (data: any) => void): void {
    this.connection?.on(eventName, callback);
  }

  off(eventName: string): void {
    this.connection?.off(eventName);
  }

  async disconnect(): Promise<void> {
    await this.connection?.stop();
  }
}

export const signalRService = new SignalRService();
```

#### 2. Create React Hook

**File:** `src/hooks/useSignalR.ts`

```typescript
import { useEffect, useCallback } from 'react';
import { signalRService } from '../services/signalrService';
import Cookies from 'js-cookie';

export const useSignalR = () => {
  const token = Cookies.get('token');

  useEffect(() => {
    if (!token) return;

    signalRService.connect(token);

    return () => {
      signalRService.disconnect();
    };
  }, [token]);

  const subscribe = useCallback((eventName: string, callback: (data: any) => void) => {
    signalRService.on(eventName, callback);
    return () => signalRService.off(eventName);
  }, []);

  return { subscribe, signalRService };
};
```

#### 3. Listen for Notifications (Admin/Staff)

**File:** `src/components/AdminNotificationListener.tsx`

```typescript
import { useEffect } from 'react';
import { useSignalR } from '../hooks/useSignalR';
import { toast } from 'react-toastify'; // or your notification library

const AdminNotificationListener = () => {
  const { subscribe, signalRService } = useSignalR();

  useEffect(() => {
    // Join admin/staff group
    signalRService.joinAdminStaffGroup();

    // Listen for new pending requests
    const unsubscribe = subscribe('ReceiveNewPendingRequest', (notification) => {
      console.log('New pending request:', notification);
      
      // Show toast notification
      toast.info(notification.message, {
        onClick: () => {
          // Navigate to pending requests tab
          window.location.href = '/pending-requests';
        }
      });

      // Optionally: Trigger a refetch of pending requests
      // queryClient.invalidateQueries(['pendingRequests']);
    });

    return unsubscribe;
  }, [subscribe, signalRService]);

  return null;
};

export default AdminNotificationListener;
```

#### 4. Add to App.tsx

```typescript
import AdminNotificationListener from './components/AdminNotificationListener';

function App() {
  const { user } = useAuth();

  return (
    <>
      {/* Only load for admin/staff */}
      {(user?.role === 'Admin' || user?.role === 'Staff') && (
        <AdminNotificationListener />
      )}
      {/* Your other components */}
    </>
  );
}
```

### Mobile App (Flutter/React Native)

#### Flutter Example

```dart
import 'package:signalr_netcore/signalr_client.dart';

class SignalRService {
  HubConnection? _connection;

  Future<void> connect(String token) async {
    _connection = HubConnectionBuilder()
        .withUrl(
          'http://your-api-url/notificationHub',
          HttpConnectionOptions(
            accessTokenFactory: () => Future.value(token),
          ),
        )
        .withAutomaticReconnect()
        .build();

    await _connection!.start();
  }

  Future<void> joinUserGroup(String userId) async {
    await _connection?.invoke('JoinUserGroup', args: [userId]);
  }

  void on(String methodName, Function(List<Object?>?) callback) {
    _connection?.on(methodName, callback);
  }

  Future<void> disconnect() async {
    await _connection?.stop();
  }
}

// Usage in your app
final signalR = SignalRService();

// Connect
await signalR.connect(token);
await signalR.joinUserGroup(userId);

// Listen for approval notifications
signalR.on('ReceiveApprovalNotification', (arguments) {
  final notification = arguments?[0];
  // Show notification to user
  showNotification(notification['message']);
});
```

## Event Summary

| Event Name | Sent To | Triggered When | Use Case |
|------------|---------|----------------|----------|
| `ReceiveNewPendingRequest` | `admin_staff` | Student creates request | Notify admin/staff of new pending requests |
| `ReceiveApprovalNotification` | `user_{userId}` + `admin_staff` | Admin approves request | Notify student their request was approved |
| `ReceiveStatusChangeNotification` | `user_{userId}` + `admin_staff` | Any status change | Notify about status updates (Borrowed, Returned, etc.) |

## Testing

### 1. Test Backend

```bash
cd BackendTechnicalAssetsManagement
dotnet run
```

### 2. Test Web Connection

Open browser console and run:
```javascript
const connection = new signalR.HubConnectionBuilder()
  .withUrl('http://localhost:5278/notificationHub', {
    accessTokenFactory: () => 'your-jwt-token'
  })
  .build();

await connection.start();
await connection.invoke('JoinAdminStaffGroup');

connection.on('ReceiveNewPendingRequest', (data) => {
  console.log('Received:', data);
});
```

### 3. Test Notification Flow

1. Create a request via API or mobile app
2. Check web app console for notification
3. Approve the request via web app
4. Check mobile app for approval notification

## Troubleshooting

### Connection Issues
- Verify JWT token is valid
- Check CORS settings in `Program.cs`
- Ensure SignalR is configured: `builder.Services.AddSignalR()`
- Verify hub is mapped: `app.MapHub<NotificationHub>("/notificationHub")`

### Not Receiving Notifications
- Confirm you've joined the correct group (`JoinUserGroup` or `JoinAdminStaffGroup`)
- Check event name matches exactly (case-sensitive)
- Verify notification is being sent from backend (check logs)

### Mobile App Issues
- Ensure WebSocket transport is enabled
- Check network permissions
- Verify API URL is correct (not localhost for physical devices)

## Summary

✅ **One Hub** - Both web and mobile use `/notificationHub`  
✅ **Group-Based** - `admin_staff` for admins, `user_{userId}` for students  
✅ **Event-Driven** - Different events for different actions  
✅ **Bidirectional** - Web ↔ Backend ↔ Mobile  
✅ **Real-Time** - Instant notifications without polling  

The system is already implemented in the backend. You just need to add the frontend listeners!
