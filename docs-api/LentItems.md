# Lent Items API

Manages borrow transactions — creating, retrieving, updating, and archiving lent item records.

> **Quick distinction:**
>
> - `LentItems` = the transaction record (mutable, tracks current state)
> - `ActivityLog` = the audit trail (immutable, one new row per event)

---

## Authentication

All endpoints require a Bearer token. Some are further restricted to **Admin or Staff** roles.

```
Authorization: Bearer <your_token>
```

---

## Base URL

```
/api/v1/lentItems
```

---

## Endpoints

### 1. Create Lent Item (User/Student)

```
POST /api/v1/lentItems
```

Creates a borrow request for the authenticated user.

**Auth:** Required (any authenticated user)

**Request Body** (JSON)

| Field                 | Type     | Required | Description                                   |
| --------------------- | -------- | -------- | --------------------------------------------- |
| `itemId`              | guid     | Yes      | ID of the item to borrow                      |
| `userId`              | guid     | No       | Borrower's user ID                            |
| `teacherId`           | guid     | No       | Associated teacher ID                         |
| `room`                | string   | Yes      | Room where item will be used                  |
| `subjectTimeSchedule` | string   | Yes      | Subject/time schedule                         |
| `reservedFor`         | datetime | No       | Reservation date/time (`yyyy-MM-ddTHH:mm:ss`) |
| `remarks`             | string   | No       | Additional notes                              |
| `status`              | string   | No       | Initial status (defaults to `Borrowed`)       |

```http
POST /api/v1/lentItems
Authorization: Bearer <token>
Content-Type: application/json

{
  "itemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "room": "Room 204",
  "subjectTimeSchedule": "BSIT 3A - 8:00 AM",
  "reservedFor": "2026-04-20T08:00:00",
  "remarks": "For lab activity"
}
```

**Responses**

| Status | Meaning           |
| ------ | ----------------- |
| `201`  | Lent item created |
| `400`  | Validation error  |
| `401`  | Not authenticated |

---

### 2. Create Lent Item for Guest

```
POST /api/v1/lentItems/guests
```

Creates a borrow record for a guest (non-registered user). The issuing staff member is recorded automatically from the token.

**Auth:** Admin or Staff

**Request Body** (JSON) — includes guest-specific fields

| Field                 | Type     | Required | Description               |
| --------------------- | -------- | -------- | ------------------------- |
| `itemId`              | guid     | Yes      |                           |
| `room`                | string   | Yes      |                           |
| `subjectTimeSchedule` | string   | Yes      |                           |
| `reservedFor`         | datetime | No       |                           |
| `remarks`             | string   | No       |                           |
| `guestImage`          | string   | No       | Guest photo URL or base64 |
| `organization`        | string   | No       |                           |
| `contactNumber`       | string   | No       |                           |
| `purpose`             | string   | No       | Reason for borrowing      |
| `supervisorName`      | string   | No       |                           |

```http
POST /api/v1/lentItems/guests
Authorization: Bearer <token>
Content-Type: application/json

{
  "itemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "room": "Conference Room A",
  "subjectTimeSchedule": "2:00 PM - 4:00 PM",
  "organization": "ABC Corp",
  "purpose": "Product demo",
  "supervisorName": "Maria Santos"
}
```

**Responses**

| Status | Meaning           |
| ------ | ----------------- |
| `201`  | Record created    |
| `401`  | Not authenticated |
| `403`  | Not authorized    |

---

### 3. Get All Lent Items

```
GET /api/v1/lentItems
```

Returns all lent item records.

**Auth:** Admin or Staff

```http
GET /api/v1/lentItems
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning          |
| ------ | ---------------- |
| `200`  | Records returned |

---

### 4. Get All Borrowed Items

```
GET /api/v1/lentItems/borrowed
```

Returns only records with an active borrowed status.

**Auth:** Admin or Staff

```http
GET /api/v1/lentItems/borrowed
Authorization: Bearer <token>
```

---

### 5. Get Lent Item by ID

```
GET /api/v1/lentItems/{id}
```

Returns a single lent item record by its GUID.

**Auth:** Admin or Staff

```http
GET /api/v1/lentItems/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning      |
| ------ | ------------ |
| `200`  | Record found |
| `404`  | Not found    |

---

### 6. Get Lent Items by Date

```
GET /api/v1/lentItems/date/{dateTime}
```

Returns all lent items for a specific date or datetime.

**Auth:** Admin or Staff

**Path Parameter**

| Parameter  | Type   | Description             | Example                               |
| ---------- | ------ | ----------------------- | ------------------------------------- |
| `dateTime` | string | Date or datetime string | `2026-04-19` or `2026-04-19T08:00:00` |

```http
GET /api/v1/lentItems/date/2026-04-19
Authorization: Bearer <token>

GET /api/v1/lentItems/date/2026-04-19T08:00:00
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                  |
| ------ | ------------------------ |
| `200`  | Records found            |
| `400`  | Invalid date format      |
| `404`  | No records for that date |

---

### 7. Update Lent Item

```
PATCH /api/v1/lentItems/{id}
```

Updates a lent item record — used by staff to approve, deny, cancel, or mark as returned.

**Auth:** Admin or Staff

**Path Parameter**

| Parameter | Type | Description         |
| --------- | ---- | ------------------- |
| `id`      | guid | Lent item record ID |

**Request Body** (JSON) — all fields optional

| Field                 | Type     | Description                                              |
| --------------------- | -------- | -------------------------------------------------------- |
| `itemId`              | guid     | Change the associated item                               |
| `userId`              | guid     |                                                          |
| `teacherId`           | guid     |                                                          |
| `room`                | string   |                                                          |
| `subjectTimeSchedule` | string   |                                                          |
| `reservedFor`         | datetime |                                                          |
| `remarks`             | string   |                                                          |
| `status`              | string   | `Borrowed`, `Returned`, `Approved`, `Denied`, `Canceled` |
| `returnedAt`          | datetime | Set when item is returned                                |

```http
PATCH /api/v1/lentItems/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
Content-Type: application/json

{
  "status": "Approved"
}
```

```http
# Mark as returned
PATCH /api/v1/lentItems/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
Content-Type: application/json

{
  "status": "Returned",
  "returnedAt": "2026-04-19T14:30:00"
}
```

**Responses**

| Status | Meaning                       |
| ------ | ----------------------------- |
| `200`  | Updated successfully          |
| `404`  | Record not found / no changes |

---

### 8. Hide from History

```
PATCH /api/v1/lentItems/hide/{lent-item-id}
```

Hides a lent item record from the borrower's history view. The record still exists in the system.

**Auth:** Required (any authenticated user — only own records)

**Path Parameter**

| Parameter      | Type | Description         |
| -------------- | ---- | ------------------- |
| `lent-item-id` | guid | Lent item record ID |

```http
PATCH /api/v1/lentItems/hide/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                     |
| ------ | --------------------------- |
| `200`  | Hidden from history         |
| `401`  | Not authenticated           |
| `404`  | Not found or not authorized |

---

### 9. Archive Lent Item

```
DELETE /api/v1/lentItems/archive/{id}
```

Moves a lent item record to the archive.

**Auth:** Admin or Staff

```http
DELETE /api/v1/lentItems/archive/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning               |
| ------ | --------------------- |
| `200`  | Archived successfully |
| `400`  | Archive failed        |
| `404`  | Record not found      |

---

## Status Values

| Value      | Description                               |
| ---------- | ----------------------------------------- |
| `Borrowed` | Item is currently borrowed / request made |
| `Returned` | Item has been returned                    |
| `Reserved` | Item is reserved for a future date        |
| `Approved` | Borrow request approved by staff          |
| `Denied`   | Borrow request denied                     |
| `Canceled` | Request was canceled                      |

---

## Response Shape

```json
{
  "success": true,
  "message": "Item retrieved successfully.",
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "item": {
      "id": "...",
      "itemName": "HDMI Cable",
      "serialNumber": "SN-00123"
    },
    "userId": "a1b2c3d4-...",
    "borrowerFullName": "Juan Dela Cruz",
    "borrowerRole": "Student",
    "studentIdNumber": "2021-00123",
    "room": "Room 204",
    "subjectTimeSchedule": "BSIT 3A - 8:00 AM",
    "reservedFor": "2026-04-20 08:00",
    "lentAt": "2026-04-19 09:10",
    "returnedAt": null,
    "status": "Borrowed",
    "remarks": "For lab activity",
    "isHiddenFromUser": false
  },
  "errors": null
}
```
