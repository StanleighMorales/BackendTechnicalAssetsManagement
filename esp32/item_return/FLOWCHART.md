# Item Return Station - Flowchart

## Overview
ESP32 device that allows students to return borrowed items by scanning the item's RFID tag.

## System Flow

```mermaid
flowchart TD
    Start([Start]) --> Init[Initialize PN532 RFID Reader]
    Init --> CheckPN532{PN532 Found?}
    CheckPN532 -->|No| Error1[Display Error: Check Wiring]
    CheckPN532 -->|Yes| WiFiConnect[Connect to WiFi]
    
    WiFiConnect --> SyncTime[Sync Time with NTP Server]
    SyncTime --> Login[Login to API<br/>POST /api/v1/auth/login-mobile]
    Login --> LoginCheck{Login Success?}
    LoginCheck -->|No| Error2[Display Error: Login Failed]
    LoginCheck -->|Yes| Ready[Display: Ready to Scan]
    
    Ready --> WaitScan[Wait for RFID Tag Scan]
    WaitScan --> ScanDetect{Tag Detected?}
    ScanDetect -->|No| WaitScan
    ScanDetect -->|Yes| ReadUID[Read RFID UID]
    
    ReadUID --> CheckCooldown{Same Tag<br/>Within 2s?}
    CheckCooldown -->|Yes| WaitScan
    CheckCooldown -->|No| ResolveItem[GET /api/v1/items/rfid/{uid}]
    
    ResolveItem --> ItemCheck{Item Found?}
    ItemCheck -->|No - 404| DisplayNotReg[Display: Item Not Registered]
    ItemCheck -->|No - 401| ReLogin1[Re-login]
    ItemCheck -->|Yes| FindLent[GET /api/v1/lentItems/borrowed]
    
    ReLogin1 --> WaitScan
    DisplayNotReg --> WaitScan
    
    FindLent --> LentCheck{Active Borrow<br/>Found?}
    LentCheck -->|No| DisplayNoBorrow[Display: No Active Borrow Record]
    LentCheck -->|Yes| GetTime[Get Current Timestamp]
    
    DisplayNoBorrow --> WaitScan
    
    GetTime --> TimeCheck{Time Available?}
    TimeCheck -->|No| DisplayTimeError[Display: Cannot Get Time]
    TimeCheck -->|Yes| ReturnItem[PATCH /api/v1/lentItems/{id}<br/>status: Returned<br/>returnedAt: timestamp]
    
    DisplayTimeError --> WaitScan
    
    ReturnItem --> ReturnCheck{Return Success?}
    ReturnCheck -->|200 OK| DisplaySuccess[Display: Item Returned Successfully]
    ReturnCheck -->|401| ReLogin2[Re-login]
    ReturnCheck -->|404| DisplayNotFound[Display: Lent Item Not Found]
    ReturnCheck -->|Other| DisplayError[Display: Unexpected Error]
    
    DisplaySuccess --> WaitScan
    ReLogin2 --> WaitScan
    DisplayNotFound --> WaitScan
    DisplayError --> WaitScan
    
    Error1 --> Halt([Halt])
    Error2 --> Halt
```

## Key Features

- **Cooldown Protection**: Prevents duplicate scans within 2 seconds
- **Auto Re-login**: Automatically re-authenticates when token expires (401 errors)
- **Time Sync**: Uses NTP server to get accurate timestamp for return time
- **WiFi Recovery**: Reconnects to WiFi if connection is lost

## API Endpoints Used

1. `POST /api/v1/auth/login-mobile` - Authenticate and get JWT token
2. `GET /api/v1/items/rfid/{uid}` - Resolve item ID from RFID UID
3. `GET /api/v1/lentItems/borrowed` - Get all active borrowed items
4. `PATCH /api/v1/lentItems/{id}` - Update lent item status to "Returned"

## Hardware Configuration

- **PN532 RFID Reader** (I2C Mode)
  - VCC → 3.3V
  - GND → GND
  - SDA → GPIO 21
  - SCL → GPIO 22
  - IRQ → GPIO 4
  - RSTO → GPIO 2

## Configuration Constants

- `WIFI_SSID` - WiFi network name
- `WIFI_PASSWORD` - WiFi password
- `API_BASE_URL` - Backend API URL
- `API_IDENTIFIER` - Admin username
- `API_PASSWORD` - Admin password
- `NTP_SERVER` - Time server for timestamp sync
- `GMT_OFFSET_SEC` - Timezone offset (28800 = GMT+8)
- `SCAN_COOLDOWN_MS` - Cooldown period (2000ms)

## Data Flow Diagram

```mermaid
flowchart LR
    subgraph ESP32["ESP32 Device"]
        RFID[PN532 RFID Reader]
        NTP[NTP Client]
        WiFi[WiFi Module]
        Logic[Application Logic]
    end
    
    subgraph Backend["Backend API"]
        Auth[Auth Service]
        ItemAPI[Item API]
        LentAPI[Lent Items API]
    end
    
    subgraph Database["Database"]
        Items[(Items Table)]
        LentItems[(Lent Items Table)]
    end
    
    RFID -->|Item Tag UID| Logic
    NTP -->|Current Time| Logic
    
    Logic -->|POST credentials| Auth
    Auth -->|JWT Token| Logic
    
    Logic -->|GET /items/rfid/{uid}<br/>+ JWT| ItemAPI
    ItemAPI -->|Query by RFID UID| Items
    Items -->|Item Data<br/>id, itemName| ItemAPI
    ItemAPI -->|Item Object| Logic
    
    Logic -->|GET /lentItems/borrowed<br/>+ JWT| LentAPI
    LentAPI -->|Query Active Borrows| LentItems
    LentItems -->|All Borrowed Items| LentAPI
    LentAPI -->|Array of Lent Items| Logic
    
    Logic -->|Filter by Item ID| Logic
    Logic -->|Format Timestamp| Logic
    
    Logic -->|PATCH /lentItems/{id}<br/>status: Returned<br/>returnedAt: timestamp<br/>+ JWT| LentAPI
    LentAPI -->|UPDATE status & returnedAt| LentItems
    LentItems -->|Confirmation| LentAPI
    LentAPI -->|200 OK| Logic
    
    WiFi -.->|Network| Auth
    WiFi -.->|Network| ItemAPI
    WiFi -.->|Network| LentAPI
```

## Data Entities

### Input Data
- **Item RFID UID**: Hexadecimal string from tag (e.g., "F8E7D6C5")
- **Current Time**: From NTP server (GMT+8)

### Transmitted Data
- **Login Request**: `{ identifier, password }`
- **JWT Token**: Bearer token for authentication
- **Return Update**: `{ status: "Returned", returnedAt: "2026-04-23T14:30:00" }`

### Received Data
- **Item Object**: `{ id, itemName, ... }`
- **Borrowed Items Array**: `[{ id, item: { id, ... }, borrowerFullName, ... }]`
- **Update Confirmation**: `{ message, data: { ... } }`

### Database Records Updated
- **LentItems Table**: Updates status and returnedAt fields
  - `status`: Changed from "Borrowed" to "Returned"
  - `returnedAt`: Set to current timestamp (ISO 8601 format)
  - Marks the end of the borrowing period

### Timestamp Format
- **Format**: ISO 8601 without timezone suffix
- **Example**: `"2026-04-23T14:30:00"`
- **Timezone**: GMT+8 (Philippines)
- **Source**: NTP server synchronized time
