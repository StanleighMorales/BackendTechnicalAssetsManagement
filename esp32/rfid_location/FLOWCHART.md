# RFID Location Tracker - Flowchart

## Overview
ESP32 device deployed at a fixed location that tracks borrowed items by updating their room location when scanned.

## System Flow

```mermaid
flowchart TD
    Start([Start]) --> Init[Initialize PN532 RFID Reader]
    Init --> CheckPN532{PN532 Found?}
    CheckPN532 -->|No| Error1[Display Error: Check Wiring]
    CheckPN532 -->|Yes| DisplayLocation[Display: Location Label]
    
    DisplayLocation --> WiFiConnect[Connect to WiFi]
    WiFiConnect --> Login[Login to API<br/>POST /api/v1/auth/login-mobile]
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
    LentCheck -->|Yes| UpdateLocation[PATCH /api/v1/lentItems/{id}<br/>room: LOCATION_LABEL]
    
    DisplayNoBorrow --> WaitScan
    
    UpdateLocation --> UpdateCheck{Update Success?}
    UpdateCheck -->|200 OK| DisplaySuccess[Display: Room Updated to Location]
    UpdateCheck -->|401| ReLogin2[Re-login]
    UpdateCheck -->|404| DisplayNotFound[Display: Lent Item Not Found]
    UpdateCheck -->|Other| DisplayError[Display: Unexpected Error]
    
    DisplaySuccess --> WaitScan
    ReLogin2 --> WaitScan
    DisplayNotFound --> WaitScan
    DisplayError --> WaitScan
    
    Error1 --> Halt([Halt])
    Error2 --> Halt
```

## Deployment Model

Each ESP32 unit is deployed at a specific physical location:
- **Fixed Location**: Device is installed at a specific room/cabinet
- **Location Label**: Configured via `LOCATION_LABEL` constant
- **Automatic Tracking**: Updates borrowed item location when scanned

## Use Cases

1. **Room Tracking**: Place device in each classroom to track where items are used
2. **Cabinet Tracking**: Place device near storage areas to track item returns
3. **Lab Tracking**: Place device in labs to monitor equipment location
4. **Building Tracking**: Place devices at building entrances/exits

## Key Features

- **Fixed Location Tracking**: Each device represents a specific physical location
- **Cooldown Protection**: Prevents duplicate scans within 2 seconds
- **Auto Re-login**: Automatically re-authenticates when token expires
- **WiFi Recovery**: Reconnects to WiFi if connection is lost
- **Borrowed Items Only**: Only tracks items that are currently borrowed

## API Endpoints Used

1. `POST /api/v1/auth/login-mobile` - Authenticate and get JWT token
2. `GET /api/v1/items/rfid/{uid}` - Resolve item ID from RFID UID
3. `GET /api/v1/lentItems/borrowed` - Get all active borrowed items
4. `PATCH /api/v1/lentItems/{id}` - Update lent item room location

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
- `LOCATION_LABEL` - Physical location identifier (e.g., "Room 111")
- `SCAN_COOLDOWN_MS` - Cooldown period (2000ms)

## Location Label Examples

- `"Room 111"` - Classroom
- `"Lab A"` - Laboratory
- `"Storage Cabinet 1"` - Storage area
- `"Library Entrance"` - Building entrance
- `"Workshop"` - Work area

## Important Notes

- Only updates location for items that are currently borrowed
- Does not track items that are not in the system
- Location is updated in real-time when item is scanned
- Multiple devices can be deployed for comprehensive tracking

## Data Flow Diagram

```mermaid
flowchart LR
    subgraph ESP32["ESP32 Device"]
        RFID[PN532 RFID Reader]
        Location[Location Label<br/>Room 111]
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
    Location -->|Fixed Location| Logic
    
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
    
    Logic -->|PATCH /lentItems/{id}<br/>room: LOCATION_LABEL<br/>+ JWT| LentAPI
    LentAPI -->|UPDATE room field| LentItems
    LentItems -->|Confirmation| LentAPI
    LentAPI -->|200 OK| Logic
    
    WiFi -.->|Network| Auth
    WiFi -.->|Network| ItemAPI
    WiFi -.->|Network| LentAPI
```

## Data Entities

### Input Data
- **Item RFID UID**: Hexadecimal string from tag (e.g., "F8E7D6C5")
- **Location Label**: Fixed string configured in device (e.g., "Room 111")

### Transmitted Data
- **Login Request**: `{ identifier, password }`
- **JWT Token**: Bearer token for authentication
- **Location Update**: `{ room: "Room 111" }`

### Received Data
- **Item Object**: `{ id, itemName, ... }`
- **Borrowed Items Array**: `[{ id, item: { id, ... }, borrowerFullName, ... }]`
- **Update Confirmation**: `{ message, data: { ... } }`

### Database Records Updated
- **LentItems Table**: Updates `room` field for matching lent item
  - Only updates records with status "Borrowed" or "Pending"
  - Tracks real-time location of borrowed items

### Location Tracking Flow
1. Device scans item tag
2. Resolves item ID from RFID UID
3. Finds active borrow record for that item
4. Updates borrow record with device's location label
5. Location is now visible to admins and students
