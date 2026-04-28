# RFID Card Registration Station - Flowchart

## Overview
ESP32 device that allows administrators to register student RFID cards by generating unique registration codes.

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
    LoginCheck -->|Yes| Ready[Display: Waiting for RFID Cards]
    
    Ready --> WaitScan[Wait for RFID Card Scan]
    WaitScan --> ScanDetect{Card Detected?}
    ScanDetect -->|No| WaitScan
    ScanDetect -->|Yes| ReadUID[Read RFID UID]
    
    ReadUID --> DisplayFound[Display: Found RFID Card<br/>Show UID and Length]
    DisplayFound --> GenerateCode[Generate Random 8-Character<br/>Alphanumeric Code]
    
    GenerateCode --> DisplayCode[Display: Generated Code]
    DisplayCode --> CheckWiFi{WiFi<br/>Connected?}
    
    CheckWiFi -->|No| Reconnect[Reconnect to WiFi]
    Reconnect --> ReconnectCheck{Reconnect<br/>Success?}
    ReconnectCheck -->|No| DisplayWiFiError[Display: WiFi Reconnection Failed]
    ReconnectCheck -->|Yes| ReLogin[Re-login to API]
    
    DisplayWiFiError --> Delay1[Wait 5 seconds]
    Delay1 --> WaitScan
    
    CheckWiFi -->|Yes| RegisterAPI[POST /api/v1/rfids<br/>rfidUid, rfidCode]
    ReLogin --> RegisterAPI
    
    RegisterAPI --> RegisterCheck{Registration<br/>Success?}
    RegisterCheck -->|200 OK| DisplaySuccess[Display: RFID Card Registered<br/>Show Success Message]
    RegisterCheck -->|401| ReLogin2[Display: Token Expired<br/>Re-login]
    RegisterCheck -->|409| DisplayDuplicate[Display: RFID Already Registered<br/>Card in System]
    RegisterCheck -->|-1| DisplayConnError[Display: Connection Error<br/>Cannot Reach Server]
    RegisterCheck -->|Other| DisplayError[Display: Unexpected Error<br/>Show Response]
    
    DisplaySuccess --> DisplayComplete[Display: Registration Complete<br/>Students Can Use Code]
    DisplayDuplicate --> DisplayComplete
    DisplayConnError --> DisplayComplete
    DisplayError --> DisplayComplete
    
    ReLogin2 --> DisplayComplete
    
    DisplayComplete --> Cooldown[Wait 5 Seconds<br/>Prevent Duplicates]
    Cooldown --> WaitScan
    
    Error1 --> Halt([Halt])
    Error2 --> Halt
```

## Registration Process

1. **Card Detection**: System waits for RFID card to be scanned
2. **UID Reading**: Reads the unique identifier from the card
3. **Code Generation**: Generates random 8-character alphanumeric code
4. **API Registration**: Sends UID and code to backend
5. **Confirmation**: Displays success or error message
6. **Cooldown**: Waits 5 seconds before accepting next card

## Key Features

- **Auto Code Generation**: Creates unique 8-character alphanumeric codes
- **Duplicate Prevention**: 5-second cooldown prevents accidental re-scans
- **WiFi Recovery**: Automatically reconnects if WiFi drops
- **Auto Re-login**: Re-authenticates when token expires
- **Duplicate Detection**: Prevents registering same card twice (409 Conflict)
- **Connection Monitoring**: Detects and reports server connection issues

## Registration Code Format

- **Length**: 8 characters
- **Character Set**: 0-9, A-Z (uppercase)
- **Example**: `A3K9M2X7`
- **Purpose**: Students use this code to link RFID card to their account

## API Endpoints Used

1. `POST /api/v1/auth/login-mobile` - Authenticate and get JWT token
2. `POST /api/v1/rfids` - Register RFID card with generated code

## Hardware Configuration

- **PN532 RFID Reader** (I2C Mode)
  - VCC → 3.3V
  - GND → GND
  - SDA → GPIO 21
  - SCL → GPIO 22
  - IRQ → GPIO 4
  - RSTO → GPIO 2

## Configuration Constants

- `WIFI_SSID` - WiFi network name (ssid variable)
- `WIFI_PASSWORD` - WiFi password (password variable)
- `API_BASE_URL` - Backend API URL (apiBaseUrl variable)
- `API_IDENTIFIER` - Admin username (adminIdentifier variable)
- `API_PASSWORD` - Admin password (adminPassword variable)

## Error Handling

- **401 Unauthorized**: Token expired, system re-logs in automatically
- **409 Conflict**: Card already registered, displays message
- **-1 Connection Error**: Cannot reach backend server
- **WiFi Disconnection**: Automatically attempts reconnection (max 10 attempts)

## Student Registration Flow

After admin registers the card:
1. Admin gives the 8-character code to the student
2. Student logs into mobile app/web portal
3. Student enters the registration code
4. System links RFID card to student's account
5. Student can now use card for borrowing items

## Cooldown Period

- **Duration**: 5 seconds
- **Purpose**: Prevents duplicate registrations from same card
- **Behavior**: System ignores scans during cooldown period

## Data Flow Diagram

```mermaid
flowchart LR
    subgraph ESP32["ESP32 Device"]
        RFID[PN532 RFID Reader]
        RNG[Random Generator]
        WiFi[WiFi Module]
        Logic[Application Logic]
    end
    
    subgraph Backend["Backend API"]
        Auth[Auth Service]
        RfidAPI[RFID API]
    end
    
    subgraph Database["Database"]
        Rfids[(RFID Table)]
    end
    
    RFID -->|Card UID| Logic
    RNG -->|8-char Code| Logic
    
    Logic -->|POST credentials| Auth
    Auth -->|JWT Token| Logic
    
    Logic -->|POST /rfids<br/>rfidUid, rfidCode<br/>+ JWT| RfidAPI
    RfidAPI -->|Check Duplicate| Rfids
    Rfids -->|Exists?| RfidAPI
    RfidAPI -->|INSERT if New| Rfids
    Rfids -->|Confirmation| RfidAPI
    RfidAPI -->|200 OK / 409 Conflict| Logic
    
    WiFi -.->|Network| Auth
    WiFi -.->|Network| RfidAPI
```

## Data Entities

### Input Data
- **RFID Card UID**: Hexadecimal string from card (e.g., "04A3B2C1D5E6F7")

### Generated Data
- **Registration Code**: 8-character alphanumeric (e.g., "A3K9M2X7")
  - Character set: 0-9, A-Z
  - Generated using ESP32 random number generator

### Transmitted Data
- **Login Request**: `{ identifier, password }`
- **JWT Token**: Bearer token for authentication
- **RFID Registration**: `{ rfidUid, rfidCode }`

### Database Records Created
- **Rfids Table**: New record with UID and registration code
  - `rfidUid`: Unique card identifier
  - `rfidCode`: 8-character code for student self-registration
  - `userId`: NULL (linked later when student registers)

### Response Codes
- **200 OK**: Registration successful
- **401 Unauthorized**: Token expired
- **409 Conflict**: RFID already registered
- **-1**: Connection error
