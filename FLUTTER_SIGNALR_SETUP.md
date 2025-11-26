# Flutter SignalR Notification Setup Guide

## Prerequisites

1. **Flutter SDK** installed
2. **Backend running** on accessible URL (not localhost for physical devices)
3. **JWT token** from login endpoint

## Installation

Add to your `pubspec.yaml`:

```yaml
dependencies:
  signalr_netcore: ^1.3.6  # Latest stable version
  flutter_local_notifications: ^16.3.0  # For local notifications
  shared_preferences: ^2.2.2  # For storing token
```

Run:
```bash
flutter pub get
```

## Implementation

### 1. Create SignalR Service

Create `lib/services/signalr_service.dart`:

```dart
import 'package:signalr_netcore/signalr_client.dart';
import 'package:flutter/foundation.dart';

class SignalRService {
  HubConnection? _connection;
  String? _hubUrl;
  String? _accessToken;
  
  // Connection state
  bool get isConnected => _connection?.state == HubConnectionState.Connected;
  
  // Singleton pattern
  static final SignalRService _instance = SignalRService._internal();
  factory SignalRService() => _instance;
  SignalRService._internal();

  /// Initialize connection with API URL and token
  Future<void> initialize(String apiBaseUrl, String accessToken) async {
    _hubUrl = '$apiBaseUrl/notificationHub';
    _accessToken = accessToken;
  }

  /// Connect to SignalR hub
  Future<bool> connect() async {
    if (_hubUrl == null || _accessToken == null) {
      debugPrint('❌ SignalR: Hub URL or token not set');
      return false;
    }

    try {
      _connection = HubConnectionBuilder()
          .withUrl(
            _hubUrl!,
            HttpConnectionOptions(
              accessTokenFactory: () => Future.value(_accessToken),
              logging: (level, message) => debugPrint('SignalR: $message'),
            ),
          )
          .withAutomaticReconnect(
            retryDelays: [0, 2000, 5000, 10000, 30000],
          )
          .build();

      // Connection lifecycle handlers
      _connection!.onclose(({error}) {
        debugPrint('❌ SignalR: Connection closed. Error: $error');
      });

      _connection!.onreconnecting(({error}) {
        debugPrint('🔄 SignalR: Reconnecting... Error: $error');
      });

      _connection!.onreconnected(({connectionId}) {
        debugPrint('✅ SignalR: Reconnected! Connection ID: $connectionId');
      });

      await _connection!.start();
      debugPrint('✅ SignalR: Connected successfully');
      return true;
    } catch (e) {
      debugPrint('❌ SignalR: Connection failed: $e');
      return false;
    }
  }

  /// Join user-specific group
  Future<void> joinUserGroup(String userId) async {
    if (!isConnected) {
      debugPrint('❌ SignalR: Not connected, cannot join user group');
      return;
    }

    try {
      await _connection!.invoke('JoinUserGroup', args: [userId]);
      debugPrint('✅ SignalR: Joined user group: user_$userId');
    } catch (e) {
      debugPrint('❌ SignalR: Failed to join user group: $e');
    }
  }

  /// Join admin/staff group
  Future<void> joinAdminStaffGroup() async {
    if (!isConnected) {
      debugPrint('❌ SignalR: Not connected, cannot join admin group');
      return;
    }

    try {
      await _connection!.invoke('JoinAdminStaffGroup');
      debugPrint('✅ SignalR: Joined admin_staff group');
    } catch (e) {
      debugPrint('❌ SignalR: Failed to join admin group: $e');
    }
  }

  /// Leave user-specific group
  Future<void> leaveUserGroup(String userId) async {
    if (!isConnected) return;

    try {
      await _connection!.invoke('LeaveUserGroup', args: [userId]);
      debugPrint('✅ SignalR: Left user group: user_$userId');
    } catch (e) {
      debugPrint('❌ SignalR: Failed to leave user group: $e');
    }
  }

  /// Leave admin/staff group
  Future<void> leaveAdminStaffGroup() async {
    if (!isConnected) return;

    try {
      await _connection!.invoke('LeaveAdminStaffGroup');
      debugPrint('✅ SignalR: Left admin_staff group');
    } catch (e) {
      debugPrint('❌ SignalR: Failed to leave admin group: $e');
    }
  }

  /// Listen to new pending request notifications (Admin/Staff)
  void onNewPendingRequest(Function(Map<String, dynamic>) callback) {
    _connection?.on('ReceiveNewPendingRequest', (arguments) {
      if (arguments != null && arguments.isNotEmpty) {
        final data = arguments[0] as Map<String, dynamic>;
        debugPrint('📬 New Pending Request: ${data['itemName']}');
        callback(data);
      }
    });
  }

  /// Listen to approval notifications (Student)
  void onApprovalNotification(Function(Map<String, dynamic>) callback) {
    _connection?.on('ReceiveApprovalNotification', (arguments) {
      if (arguments != null && arguments.isNotEmpty) {
        final data = arguments[0] as Map<String, dynamic>;
        debugPrint('✅ Approval Notification: ${data['message']}');
        callback(data);
      }
    });
  }

  /// Listen to status change notifications
  void onStatusChange(Function(Map<String, dynamic>) callback) {
    _connection?.on('ReceiveStatusChangeNotification', (arguments) {
      if (arguments != null && arguments.isNotEmpty) {
        final data = arguments[0] as Map<String, dynamic>;
        debugPrint('🔄 Status Change: ${data['message']}');
        callback(data);
      }
    });
  }

  /// Listen to broadcast notifications
  void onBroadcast(Function(Map<String, dynamic>) callback) {
    _connection?.on('ReceiveBroadcastNotification', (arguments) {
      if (arguments != null && arguments.isNotEmpty) {
        final data = arguments[0] as Map<String, dynamic>;
        debugPrint('📢 Broadcast: ${data['message']}');
        callback(data);
      }
    });
  }

  /// Disconnect from hub
  Future<void> disconnect() async {
    if (_connection != null) {
      await _connection!.stop();
      debugPrint('🔌 SignalR: Disconnected');
    }
  }

  /// Remove all event listeners
  void removeAllListeners() {
    _connection?.off('ReceiveNewPendingRequest');
    _connection?.off('ReceiveApprovalNotification');
    _connection?.off('ReceiveStatusChangeNotification');
    _connection?.off('ReceiveBroadcastNotification');
  }
}
```

### 2. Create Notification Manager

Create `lib/services/notification_manager.dart`:

```dart
import 'package:flutter_local_notifications/flutter_local_notifications.dart';
import 'package:flutter/foundation.dart';

class NotificationManager {
  static final FlutterLocalNotificationsPlugin _notifications =
      FlutterLocalNotificationsPlugin();

  static Future<void> initialize() async {
    const androidSettings = AndroidInitializationSettings('@mipmap/ic_launcher');
    const iosSettings = DarwinInitializationSettings(
      requestAlertPermission: true,
      requestBadgePermission: true,
      requestSoundPermission: true,
    );

    const settings = InitializationSettings(
      android: androidSettings,
      iOS: iosSettings,
    );

    await _notifications.initialize(
      settings,
      onDidReceiveNotificationResponse: (details) {
        debugPrint('Notification tapped: ${details.payload}');
        // Handle notification tap - navigate to relevant screen
      },
    );
  }

  static Future<void> showNotification({
    required String title,
    required String body,
    String? payload,
  }) async {
    const androidDetails = AndroidNotificationDetails(
      'signalr_channel',
      'Real-time Notifications',
      channelDescription: 'Notifications from SignalR hub',
      importance: Importance.high,
      priority: Priority.high,
      showWhen: true,
    );

    const iosDetails = DarwinNotificationDetails(
      presentAlert: true,
      presentBadge: true,
      presentSound: true,
    );

    const details = NotificationDetails(
      android: androidDetails,
      iOS: iosDetails,
    );

    await _notifications.show(
      DateTime.now().millisecond,
      title,
      body,
      details,
      payload: payload,
    );
  }
}
```

### 3. Integration in Main App

Update your `lib/main.dart`:

```dart
import 'package:flutter/material.dart';
import 'services/signalr_service.dart';
import 'services/notification_manager.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  
  // Initialize local notifications
  await NotificationManager.initialize();
  
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Technical Assets Management',
      home: LoginScreen(),
    );
  }
}
```

### 4. Connect After Login

In your login success handler:

```dart
// After successful login
final signalR = SignalRService();

// Initialize with your API URL and token
await signalR.initialize(
  'http://your-api-url:5278',  // Use actual IP for physical devices
  accessToken,
);

// Connect to hub
final connected = await signalR.connect();

if (connected) {
  // Join appropriate group based on role
  if (userRole == 'Admin' || userRole == 'Staff') {
    await signalR.joinAdminStaffGroup();
    
    // Listen for new pending requests
    signalR.onNewPendingRequest((data) {
      NotificationManager.showNotification(
        title: 'New Pending Request',
        body: '${data['borrowerName']} wants to borrow ${data['itemName']}',
        payload: data['lentItemId'],
      );
    });
  } else if (userRole == 'Student') {
    await signalR.joinUserGroup(userId);
    
    // Listen for approvals
    signalR.onApprovalNotification((data) {
      NotificationManager.showNotification(
        title: 'Request Approved',
        body: data['message'],
        payload: data['lentItemId'],
      );
    });
  }
  
  // Listen for status changes (all users)
  signalR.onStatusChange((data) {
    NotificationManager.showNotification(
      title: 'Status Update',
      body: data['message'],
      payload: data['lentItemId'],
    );
  });
}
```

### 5. Disconnect on Logout

```dart
// On logout
final signalR = SignalRService();
await signalR.disconnect();
```

## Android Configuration

### AndroidManifest.xml

Add permissions in `android/app/src/main/AndroidManifest.xml`:

```xml
<manifest>
    <!-- Internet permission -->
    <uses-permission android:name="android.permission.INTERNET" />
    
    <!-- Notification permissions (Android 13+) -->
    <uses-permission android:name="android.permission.POST_NOTIFICATIONS"/>
    
    <!-- Wake lock for background notifications -->
    <uses-permission android:name="android.permission.WAKE_LOCK" />
    
    <application>
        <!-- ... -->
    </application>
</manifest>
```

### Network Security Config (for HTTP in development)

Create `android/app/src/main/res/xml/network_security_config.xml`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<network-security-config>
    <domain-config cleartextTrafficPermitted="true">
        <domain includeSubdomains="true">10.0.2.2</domain> <!-- Android emulator -->
        <domain includeSubdomains="true">localhost</domain>
        <domain includeSubdomains="true">192.168.1.x</domain> <!-- Your local IP -->
    </domain-config>
</network-security-config>
```

Reference it in `AndroidManifest.xml`:

```xml
<application
    android:networkSecurityConfig="@xml/network_security_config">
```

## iOS Configuration

### Info.plist

Add to `ios/Runner/Info.plist`:

```xml
<key>NSAppTransportSecurity</key>
<dict>
    <key>NSAllowsArbitraryLoads</key>
    <true/>
</dict>
```

## Environment Configuration

Create `lib/config/app_config.dart`:

```dart
class AppConfig {
  // Development
  static const String devApiUrl = 'http://192.168.1.x:5278';  // Your local IP
  
  // Production
  static const String prodApiUrl = 'https://your-production-api.com';
  
  // Current environment
  static const bool isProduction = bool.fromEnvironment('dart.vm.product');
  
  static String get apiUrl => isProduction ? prodApiUrl : devApiUrl;
}
```

## Testing

### 1. Start Backend
```bash
cd BackendTechnicalAssetsManagement
dotnet run
```

### 2. Get Your Local IP
```bash
# Windows
ipconfig

# Look for IPv4 Address (e.g., 192.168.1.100)
```

### 3. Update Flutter Config
```dart
static const String devApiUrl = 'http://192.168.1.100:5278';
```

### 4. Run Flutter App
```bash
flutter run
```

### 5. Test Notification Flow

**As Student:**
1. Login to app
2. Create a borrow request
3. Admin should receive notification

**As Admin:**
1. Login to app
2. Approve a pending request
3. Student should receive notification

## Troubleshooting

### Connection Failed

**Check:**
- Backend is running and accessible
- Using correct IP address (not localhost for physical devices)
- CORS is configured in backend `Program.cs`
- Network permissions in AndroidManifest.xml

**Test connection:**
```dart
// Add this to test connectivity
try {
  final response = await http.get(Uri.parse('$apiUrl/health'));
  print('API reachable: ${response.statusCode}');
} catch (e) {
  print('API not reachable: $e');
}
```

### Not Receiving Notifications

**Check:**
- SignalR connection is established (`isConnected == true`)
- Joined correct group (user or admin)
- Event names match exactly (case-sensitive)
- Backend is sending notifications (check backend logs)

**Debug:**
```dart
// Enable verbose logging
signalR._connection?.serverTimeoutInMilliseconds = 30000;
signalR._connection?.keepAliveIntervalInMilliseconds = 15000;
```

### Android Notification Not Showing

**Check:**
- Notification permissions granted (Android 13+)
- Channel created properly
- App is not in battery optimization

**Request permission:**
```dart
// For Android 13+
await _notifications
    .resolvePlatformSpecificImplementation<
        AndroidFlutterLocalNotificationsPlugin>()
    ?.requestPermission();
```

## Event Reference

| Event Name | Sent To | Data Structure |
|------------|---------|----------------|
| `ReceiveNewPendingRequest` | `admin_staff` | `{ itemName, borrowerName, reservedFor, lentItemId, timestamp }` |
| `ReceiveApprovalNotification` | `user_{userId}` + `admin_staff` | `{ itemName, borrowerName, message, lentItemId, timestamp }` |
| `ReceiveStatusChangeNotification` | `user_{userId}` + `admin_staff` | `{ itemName, oldStatus, newStatus, message, lentItemId, timestamp }` |
| `ReceiveBroadcastNotification` | All connected clients | `{ type, message, data, timestamp }` |

## Production Checklist

- [ ] Update API URL to production domain
- [ ] Remove `cleartextTrafficPermitted` from Android config
- [ ] Remove `NSAllowsArbitraryLoads` from iOS config
- [ ] Test on physical devices
- [ ] Test reconnection behavior
- [ ] Test background notifications
- [ ] Verify CORS settings for production domain
- [ ] Add error tracking (Sentry, Firebase Crashlytics)
- [ ] Add analytics for notification engagement

## Summary

✅ **SignalR Service** - Connection management with auto-reconnect  
✅ **Notification Manager** - Local notifications for foreground/background  
✅ **Role-Based Groups** - Admin/Staff vs Student notifications  
✅ **Event Handlers** - All 4 notification types supported  
✅ **Platform Config** - Android & iOS permissions  
✅ **Error Handling** - Comprehensive logging and debugging  

Your Flutter app is now ready for real-time notifications! 🎉
