# RFID Item Registration Station - Flowchart

## Overview
ESP32 device that allows administrators to register RFID tags to existing items in the system.

## System Flow

```mermaid
flowchart TD
    Start([Start]) --> Init[Initialize PN532 RFID Reader]
    Init --> CheckPN532{PN532 Found?}
    CheckPN532 -->|No| Error1[Display Error: Check Wiring]
    CheckPN532 -->|Yes| WiFiConnect[Connect to WiFi]
    
    WiFiConnect --> Login[Login to API<br/>POST /api/v1/auth/login-mobile]
    Login --> LoginCheck{Login Success?}
    LoginCheck -->|No| Error2[Display Error: Login Failed]
    LoginCheck -->|Yes| ModeSelect[Mode: MANUAL]
    
    ModeSelect --> DisplayInstructions[Display Instructions:<br/>1. Type M and Enter<br/>2. Scan RFID tag<br/>3. Enter Item ID<br/>4. Press Enter]
    
    DisplayInstructions --> WaitCommand[Wait for Serial Command]
    WaitCommand --> CommandCheck{Command<br/>Received?}
    CommandCheck -->|No| WaitCommand
    CommandCheck -->|M| StartManual[Start Manual Registration]
    CommandCheck -->|Other| WaitCommand
    
    StartManual --> DisplayWaiting[Display: Waiting for RFID Tag]
    DisplayWaiting --> WaitScan[Wait for RFID Tag Scan]
    
    WaitScan --> CheckSerial{Serial Input?}
    CheckSerial -->|M| AlreadyManual[Display: Already in Manual Mode]
    CheckSerial -->|Other| WaitScan
    AlreadyManual --> WaitCommand
    
    WaitScan --> ScanDetect{Tag Detected?}
    ScanDetect -->|No| CheckSerial
    ScanDetect -->|Yes| ReadUID[Read RFID UID]
    
    ReadUID --> DisplayUID[Display: Found RFID Tag<br/>Show UID]
    DisplayUID --> PromptItemID[Display: Enter Item ID GUID]
    
    PromptItemID --> WaitItemID[Wait for Serial Input<br/>60 second timeout]
    WaitItemID --> ItemIDCheck{Item ID<br/>Received?}
    ItemIDCheck -->|Timeout| DisplayTimeout[Display: Timeout]
    ItemIDCheck -->|Yes| DisplayRegistering[Display: Registering...]
    
    DisplayTimeout --> Delay1[Wait 2 seconds]
    Delay1 --> WaitCommand
    
    DisplayRegistering --> RegisterAPI[POST /api/v1/items/{itemId}/register-rfid<br/>rfidUid]
    
    RegisterAPI --> RegisterCheck{Registration<br/>Success?}
    RegisterCheck -->|200 OK| DisplaySuccess[Display: RFID Registered Successfully]
    RegisterCheck -->|401| ReLogin[Display: Unauthorized<br/>Re-login]
    RegisterCheck -->|404| DisplayNotFound[Display: Item Not Found<br/>Check Item ID]
    RegisterCheck -->|409| DisplayConflict[Display: RFID Already Assigned]
    RegisterCheck -->|Other| DisplayError[Display: Unexpected Error]
    
    DisplaySuccess --> DisplayComplete[Display: Registration Complete]
    ReLogin --> Delay2[Wait 3 seconds]
    DisplayNotFound --> Delay2
    DisplayConflict --> Delay2
    DisplayError --> Delay2
    
    DisplayComplete --> Delay2[Wait 3 seconds]
    Delay2 --> WaitCommand
    
    Error1 --> Halt([Halt])
    Error2 --> Halt
```

## Registration Mode

Currently supports **MANUAL** mode only:

### Manual Mode Flow
1. Admin types 'M' in Serial Monitor
2. System prompts to scan RFID tag
3. Admin scans the item's RFID tag
4. System displays the UID
5. Admin enters the Item ID (GUID) via Serial Monitor
6. System registers the RFID to the item
7. Returns to waiting for next command

## Key Features

- **Manual Item ID Entry**: Admin provides the exact Item ID (GUID) for registration
- **Serial Command Interface**: All interaction via Serial Monitor
- **60-Second Timeout**: Prevents hanging if admin doesn't enter Item ID
- **Duplicate Detection**: Prevents registering same RFID to multiple items (409 Conflict)
- **Auto Re-login**: Automatically re-authenticates when token expires
- **3-Second Cooldown**: Prevents accidental duplicate registrations

## API Endpoints Used

1. `POST /api/v1/auth/login-mobile` - Authenticate and get JWT token
2. `POST /api/v1/items/{itemId}/register-rfid` - Register RFID UID to item

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

## Serial Commands

- `M` - Activate Manual registration mode

## Error Handling

- **401 Unauthorized**: Token expired, system re-logs in
- **404 Not Found**: Item ID doesn't exist in database
- **409 Conflict**: RFID already assigned to another item
- **Timeout**: No Item ID entered within 60 seconds

## Data Flow Diagram

```mermaid
flowchart LR
    subgraph ESP32["ESP32 Device"]
        RFID[PN532 RFID Reader]
        Serial[Serial Monitor]
        WiFi[WiFi Module]
        Logic[Application Logic]
    end
    
    subgraph Backend["Backend API"]
        Auth[Auth Service]
        ItemAPI[Item API]
    end
    
    subgraph Database["Database"]
        Items[(Items Table)]
    end
    
    subgraph Admin["Administrator"]
        User[Admin User]
    end
    
    RFID -->|Item Tag UID| Logic
    User -->|Item ID GUID| Serial
    Serial -->|Item ID| Logic
    
    Logic -->|POST credentials| Auth
    Auth -->|JWT Token| Logic
    
    Logic -->|POST /items/{itemId}/register-rfid<br/>rfidUid<br/>+ JWT| ItemAPI
    ItemAPI -->|Query Item by ID| Items
    Items -->|Item Exists?| ItemAPI
    ItemAPI -->|Check RFID Duplicate| Items
    Items -->|RFID Available?| ItemAPI
    ItemAPI -->|UPDATE rfidUid field| Items
    Items -->|Confirmation| ItemAPI
    ItemAPI -->|200 OK / 404 / 409| Logic
    
    Logic -->|Status Message| Serial
    Serial -->|Display Result| User
    
    WiFi -.->|Network| Auth
    WiFi -.->|Network| ItemAPI
```

## Data Entities

### Input Data
- **Item RFID UID**: Hexadecimal string from tag (e.g., "F8E7D6C5B4A3")
- **Item ID**: GUID from admin input (e.g., "a1b2c3d4-e5f6-7890-abcd-ef1234567890")

### Transmitted Data
- **Login Request**: `{ identifier, password }`
- **JWT Token**: Bearer token for authentication
- **RFID Registration**: `{ rfidUid: "F8E7D6C5B4A3" }`

### Received Data
- **Registration Confirmation**: `{ message, data: { ... } }`

### Database Records Updated
- **Items Table**: Updates `rfidUid` field for specified item
  - Links RFID tag to existing item record
  - Enables item to be scanned for borrowing/returning

### Manual Registration Process
1. Admin creates item in system (via web/mobile app)
2. System generates Item ID (GUID)
3. Admin types 'M' in Serial Monitor
4. Admin scans physical RFID tag on item
5. Admin enters Item ID from system
6. Device links RFID UID to Item ID
7. Item is now scannable for all operations

### Response Codes
- **200 OK**: RFID successfully registered to item
- **401 Unauthorized**: Token expired, re-login required
- **404 Not Found**: Item ID doesn't exist in database
- **409 Conflict**: RFID already assigned to another item
