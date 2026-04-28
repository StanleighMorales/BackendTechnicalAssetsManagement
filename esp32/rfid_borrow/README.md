# RFID Borrow Station — Setup Guide

ESP32 + PN532 borrow station that waits for a web trigger before accepting scans.
Staff clicks "Start Borrow Session" in the web panel → ESP32 activates → student scans ID card + item tag → borrow request created.

---

## How It Works

```
Web panel: staff clicks "Start Borrow Session"
        ↓
Backend creates a BorrowSession (status: Pending)
        ↓
ESP32 polls GET /api/v1/borrow-sessions/pending every 2s
        ↓
ESP32 finds session → Serial: "✓ Borrow session triggered from web!"
        ↓
Student taps ID card on scanner
        ↓
Student taps item tag on scanner
        ↓
ESP32 posts borrow request → POST /api/v1/lentItems
        ↓
ESP32 closes session → POST /api/v1/borrow-sessions/{id}/complete
        ↓
Web modal shows ✓ with student name + item name
        ↓
ESP32 resets → back to polling
```

No login or JWT required — all endpoints are `AllowAnonymous`.

---

## Hardware

### Parts
- ESP32 development board
- PN532 NFC/RFID module
- Jumper wires

### Wiring — I2C Mode

| PN532 Pin | ESP32 Pin |
|-----------|-----------|
| VCC | 3.3V |
| GND | GND |
| SDA | GPIO 21 |
| SCL | GPIO 22 |
| IRQ | GPIO 4 |
| RSTO | GPIO 2 |

> Make sure the PN532 I2C mode switches are set correctly:
> - Switch 1 → **OFF**
> - Switch 2 → **ON**

---

## Prerequisites

### Arduino IDE Libraries

Install these via **Sketch → Include Library → Manage Libraries**:

| Library | Search term |
|---------|-------------|
| Adafruit PN532 | `Adafruit PN532` |
| ArduinoJson | `ArduinoJson` by Benoit Blanchon |

### Board Support

1. Open **File → Preferences**
2. Add to Additional Boards Manager URLs:
   ```
   https://raw.githubusercontent.com/espressif/arduino-esp32/gh-pages/package_esp32_index.json
   ```
3. Open **Tools → Board → Boards Manager**, search `esp32`, install **esp32 by Espressif Systems**
4. Select your board: **Tools → Board → ESP32 Arduino → ESP32 Dev Module**

---

## Configuration

Open `rfid_borrow.ino` and update the constants at the top:

```cpp
#define WIFI_SSID      "your-wifi-name"
#define WIFI_PASSWORD  "your-wifi-password"
#define API_BASE_URL   "http://192.168.1.x:5289"   // your backend IP
#define DEFAULT_ROOM   "Technical"                  // room sent with borrow request (nullable)
```

### Finding your backend IP

On Windows:
```bash
ipconfig
# Look for IPv4 Address under your WiFi adapter
```

On Mac/Linux:
```bash
ifconfig | grep "inet "
```

The ESP32 and your backend must be on the **same WiFi network**.

---

## Flashing

1. Connect ESP32 to your computer via USB
2. Select the correct port: **Tools → Port → COMx** (Windows) or `/dev/ttyUSB0` (Linux/Mac)
3. Click **Upload** (→ arrow button)
4. If upload fails, hold the **BOOT** button on the ESP32 while clicking Upload, release after "Connecting..."

---

## Verifying It Works

Open **Tools → Serial Monitor**, set baud rate to **115200**.

On successful startup you should see:
```
=== RFID Borrow Station (Session Mode) ===
PN532 firmware v1.6
Connecting to YourWiFi....
Connected — IP: 192.168.1.x
Syncing time with NTP...
Time synced: Monday 8:30 AM

Polling for borrow sessions from web...
```

When a session is triggered from the web:
```
─────────────────────────────────────────
✓ Borrow session triggered from web!
  Session ID: xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
─────────────────────────────────────────

[Step 1] Scan student ID card...
```

After student scans ID card:
```
Scanned tag: 04A3B2C1
GET http://192.168.1.x:5289/api/v1/users/students/rfid/04A3B2C1
HTTP 200
✓ Student: Juan Dela Cruz (ID: xxxxxxxx-...)

[Step 2] Now scan the item tag...
```

After student scans item:
```
Scanned tag: F8E7D6C5
GET http://192.168.1.x:5289/api/v1/items/rfid/F8E7D6C5
HTTP 200
✓ Item: HDMI Cable (ID: xxxxxxxx-...)

POST http://192.168.1.x:5289/api/v1/lentItems
  Student : Juan Dela Cruz
  Item    : HDMI Cable
  Room    : Technical
  Schedule: Monday 8:30 AM
LentItems HTTP 201
✓ Borrow request submitted!

POST http://192.168.1.x:5289/api/v1/borrow-sessions/xxxxxxxx.../complete
Session complete HTTP 200

── Session reset — polling for next request...
```

---

## Troubleshooting

### PN532 not found
```
ERROR: PN532 not found. Check wiring/I2C switches.
```
- Check all 6 wires are connected correctly
- Verify I2C mode switches on the PN532 (SW1=OFF, SW2=ON)
- Try a different 3.3V pin or add a 10µF capacitor between VCC and GND

### WiFi won't connect
- Double-check `WIFI_SSID` and `WIFI_PASSWORD` — they are case-sensitive
- ESP32 only supports 2.4GHz networks, not 5GHz
- Move the ESP32 closer to the router

### Poll error / can't reach backend
```
✗ Poll error: 0
```
- Confirm `API_BASE_URL` has the correct IP and port
- Make sure the backend is running (`dotnet run`)
- Both devices must be on the same network

### Student not registered (404)
```
✗ Student not registered.
```
The student's RFID card hasn't been linked to their account yet.
Use the student RFID registration station or the web admin panel to register their card first.

### Item not registered (404)
```
✗ Item not registered.
```
The item's RFID tag hasn't been linked to the item yet.
Use the item RFID registration station or the web admin panel to register the tag first.

### Borrow request rejected (400)
```
✗ Failed: LentItems error 400
```
The item is already borrowed, reserved, or unavailable.
Check the item's current status in the web panel.

### Session never picked up by ESP32
- Confirm the sketch is running (Serial Monitor shows "Polling for borrow sessions from web...")
- The `BorrowSessions` table must exist in the database — run `scripts/create-borrow-sessions.sql` in Supabase SQL Editor if not done yet
- Check that `API_BASE_URL` matches the running backend

### NTP sync failed
```
Warning: NTP sync failed — schedule will use fallback.
```
The schedule will default to `"Monday 8:00 AM"`. This is cosmetic — the borrow request still works.
Fix by ensuring the ESP32 has internet access (not just LAN).

---

## State Machine Reference

| State | What's happening |
|-------|-----------------|
| `POLLING` | Idle — checking for a web-triggered session every 2s |
| `WAIT_STUDENT` | Session active — waiting for student to tap ID card |
| `WAIT_ITEM` | Student identified — waiting for student to tap item tag |
| `SUBMITTING` | Creating lent item + closing session |

---

## Related Files

| File | Purpose |
|------|---------|
| `rfid_borrow.ino` | This sketch |
| `FLOWCHART.md` | Full state machine and data flow diagrams |
| `docs-api/RfidBorrowSession_WebGuide.md` | Web frontend implementation guide |
| `src/Controllers/BorrowSessionController.cs` | Backend session endpoints |
| `scripts/create-borrow-sessions.sql` | SQL to create the BorrowSessions table |
