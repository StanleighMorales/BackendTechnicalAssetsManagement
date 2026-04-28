# RFID Borrow Station - Flowchart

## Overview
ESP32 device that allows students to borrow items by scanning their student ID card and then the item's RFID tag.

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
    LoginCheck -->|Yes| StateStudent[State: WAIT_STUDENT]
    
    StateStudent --> PromptStudent[Display: Scan Student ID Card]
    PromptStudent --> WaitStudent[Wait for RFID Scan]
    
    WaitStudent --> StudentScan{Tag Detected?}
    StudentScan -->|No| WaitStudent
    StudentScan -->|Yes| ReadStudentUID[Read RFID UID]
    
    ReadStudentUID --> CheckCooldown1{Same Tag<br/>Within 2s?}
    CheckCooldown1 -->|Yes| WaitStudent
    CheckCooldown1 -->|No| ResolveStudent[GET /api/v1/users/students/rfid/{uid}]
    
    ResolveStudent --> StudentCheck{Student Found?}
    StudentCheck -->|No - 404| DisplayNotReg1[Display: Student Not Registered]
    StudentCheck -->|No - 401| ReLogin1[Re-login]
    StudentCheck -->|Yes| SaveStudent[Save Student ID]
    
    DisplayNotReg1 --> WaitStudent
    ReLogin1 --> WaitStudent
    
    SaveStudent --> StateItem[State: WAIT_ITEM]
    StateItem --> PromptItem[Display: Scan Item Tag]
    PromptItem --> WaitItem[Wait for RFID Scan]
    
    WaitItem --> ItemScan{Tag Detected?}
    ItemScan -->|No| WaitItem
    ItemScan -->|Yes| ReadItemUID[Read RFID UID]
    
    ReadItemUID --> CheckCooldown2{Same Tag<br/>Within 2s?}
    CheckCooldown2 -->|Yes| WaitItem
    CheckCooldown2 -->|No| ResolveItem[GET /api/v1/items/rfid/{uid}]
    
    ResolveItem --> ItemCheck{Item Found?}
    ItemCheck -->|No - 404| DisplayNotReg2[Display: Item Not Registered]
    ItemCheck -->|No - 401| ReLogin2[Re-login]
    ItemCheck -->|Yes| SaveItem[Save Item ID]
    
    DisplayNotReg2 --> WaitItem
    ReLogin2 --> WaitItem
    
    SaveItem --> StateSubmit[State: SUBMITTING]
    StateSubmit --> GetSchedule[Get Current Day/Time Schedule]
    GetSchedule --> CreateBorrow[POST /api/v1/lentItems<br/>itemId, userId, status: Pending<br/>subjectTimeSchedule]
    
    CreateBorrow --> BorrowCheck{Borrow Success?}
    BorrowCheck -->|201/200| DisplaySuccess[Display: Borrow Request Submitted]
    BorrowCheck -->|400| DisplayRejected[Display: Request Rejected<br/>Item may be borrowed]
    BorrowCheck -->|401| DisplayUnauth[Display: Unauthorized]
    BorrowCheck -->|Other| DisplayError[Display: Unexpected Error]
    
    DisplaySuccess --> Reset[Reset Session]
    DisplayRejected --> Reset
    DisplayUnauth --> Reset
    DisplayError --> Reset
    
    Reset --> StateStudent
    
    Error1 --> Halt([Halt])
    Error2 --> Halt
```

## State Machine

The system operates in three states:

1. **WAIT_STUDENT** - Waiting for student ID card scan
2. **WAIT_ITEM** - Waiting for item RFID tag scan
3. **SUBMITTING** - Creating borrow request

## Key Features

- **Two-Step Process**: Student must scan their ID first, then the item
- **Auto Schedule Detection**: Automatically determines current day and time for schedule
- **Cooldown Protection**: Prevents duplicate scans within 2 seconds
- **Auto Re-login**: Automatically re-authenticates when token expires
- **Session Reset**: Automatically resets after each borrow attempt

## API Endpoints Used

1. `POST /api/v1/auth/login-mobile` - Authenticate and get JWT token
2. `GET /api/v1/users/students/rfid/{uid}` - Resolve student ID from RFID UID
3. `GET /api/v1/items/rfid/{uid}` - Resolve item ID from RFID UID
4. `POST /api/v1/lentItems` - Create new borrow request (status: Pending)

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
- `NTP_SERVER` - Time server for schedule generation
- `GMT_OFFSET_SEC` - Timezone offset (28800 = GMT+8)
- `DEFAULT_ROOM` - Default room value (empty string)
- `SCAN_COOLDOWN_MS` - Cooldown period (2000ms)

## Schedule Format

Generated automatically based on current time:
- Format: `"DayOfWeek HH:MM AM/PM"`
- Example: `"Monday 8:30 AM"`

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
        UserAPI[User API]
        ItemAPI[Item API]
        LentAPI[Lent Items API]
    end
    
    subgraph Database["Database"]
        Users[(Users Table)]
        Items[(Items Table)]
        LentItems[(Lent Items Table)]
        Rfids[(RFID Table)]
    end
    
    RFID -->|Student Card UID| Logic
    RFID -->|Item Tag UID| Logic
    NTP -->|Current Time| Logic
    
    Logic -->|POST credentials| Auth
    Auth -->|JWT Token| Logic
    
    Logic -->|GET /users/students/rfid/{uid}<br/>+ JWT| UserAPI
    UserAPI -->|Query by RFID UID| Rfids
    Rfids -->|User ID| Users
    Users -->|Student Data<br/>id, firstName, lastName| UserAPI
    UserAPI -->|Student Object| Logic
    
    Logic -->|GET /items/rfid/{uid}<br/>+ JWT| ItemAPI
    ItemAPI -->|Query by RFID UID| Items
    Items -->|Item Data<br/>id, itemName| ItemAPI
    ItemAPI -->|Item Object| Logic
    
    Logic -->|POST /lentItems<br/>itemId, userId, status: Pending<br/>subjectTimeSchedule<br/>+ JWT| LentAPI
    LentAPI -->|INSERT Record| LentItems
    LentItems -->|Confirmation| LentAPI
    LentAPI -->|201 Created| Logic
    
    WiFi -.->|Network| Auth
    WiFi -.->|Network| UserAPI
    WiFi -.->|Network| ItemAPI
    WiFi -.->|Network| LentAPI
```

## Data Entities

### Input Data
- **Student RFID UID**: Hexadecimal string (e.g., "04A3B2C1")
- **Item RFID UID**: Hexadecimal string (e.g., "F8E7D6C5")
- **Current Time**: From NTP server (GMT+8)

### Transmitted Data
- **Login Request**: `{ identifier, password }`
- **JWT Token**: Bearer token for authentication
- **Borrow Request**: `{ itemId, userId, status: "Pending", subjectTimeSchedule }`

### Received Data
- **Student Object**: `{ id, firstName, lastName, ... }`
- **Item Object**: `{ id, itemName, ... }`
- **Lent Item Response**: `{ id, status, createdAt, ... }`

### Database Records Created
- **LentItems Table**: New record with status "Pending" awaiting admin approval
