# RFID Cards API

Manages RFID cards in the system before they are assigned to students.

---

## Authentication

Most endpoints require a Bearer token unless marked otherwise.

```
Authorization: Bearer <your_token>
```

---

## Base URL

```
/api/v1/rfids
```

---

## Endpoints

### 1. Register RFID Card

```
POST /api/v1/rfids
```

Registers a new RFID card to the system. Called by ESP32 RFID registration station.

**Auth:** None (AllowAnonymous - IoT device endpoint)

**Request Body** (JSON)

| Field      | Type   | Required | Description                    |
| ---------- | ------ | -------- | ------------------------------ |
| `rfidUid`  | string | Yes      | RFID tag UID (e.g., 04A1B2C3)  |
| `rfidCode` | string | Yes      | Human-readable code (e.g., ABC12345) |

```http
POST /api/v1/rfids
Content-Type: application/json

{
  "rfidUid": "04A1B2C3D4E5F6",
  "rfidCode": "ABC12345"
}
```

**Responses**

| Status | Meaning                                  |
| ------ | ---------------------------------------- |
| `200`  | RFID card registered successfully        |
| `409`  | RFID UID or Code already exists          |

**Success Response (200 OK)**
```json
{
  "success": true,
  "message": "RFID card registered successfully. Code: ABC12345",
  "data": null
}
```

**Error Response (409 Conflict)**
```json
{
  "success": false,
  "message": "RFID UID '04A1B2C3D4E5F6' is already registered.",
  "data": null
}
```

---

### 2. Get All RFID Cards

```
GET /api/v1/rfids
```

Returns a list of all registered RFID cards.

**Auth:** Admin or Staff

```http
GET /api/v1/rfids
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                    |
| ------ | -------------------------- |
| `200`  | RFID cards list returned   |

**Response Data**
```json
{
  "success": true,
  "message": "RFID cards retrieved successfully.",
  "data": [
    {
      "rfidUid": "04A1B2C3D4E5F6",
      "rfidCode": "ABC12345"
    },
    {
      "rfidUid": "04B2C3D4E5F6A7",
      "rfidCode": "XYZ67890"
    }
  ]
}
```

---

### 3. Get RFID Card by UID

```
GET /api/v1/rfids/{rfidUid}
```

Returns details of a specific RFID card.

**Auth:** Admin or Staff

**Path Parameter**

| Parameter  | Type   | Description |
| ---------- | ------ | ----------- |
| `rfidUid`  | string | RFID UID    |

```http
GET /api/v1/rfids/04A1B2C3D4E5F6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning              |
| ------ | -------------------- |
| `200`  | RFID card found      |
| `404`  | RFID card not found  |

**Response Data**
```json
{
  "success": true,
  "message": "RFID card retrieved successfully.",
  "data": {
    "rfidUid": "04A1B2C3D4E5F6",
    "rfidCode": "ABC12345"
  }
}
```

---

### 4. Delete RFID Card

```
DELETE /api/v1/rfids/{rfidUid}
```

Deletes an RFID card from the system. Cannot delete if assigned to a student.

**Auth:** Admin only

**Path Parameter**

| Parameter  | Type   | Description |
| ---------- | ------ | ----------- |
| `rfidUid`  | string | RFID UID    |

```http
DELETE /api/v1/rfids/04A1B2C3D4E5F6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                                  |
| ------ | ---------------------------------------- |
| `200`  | RFID card deleted                        |
| `400`  | Cannot delete (assigned to student)      |
| `404`  | RFID card not found                      |

**Success Response**
```json
{
  "success": true,
  "message": "RFID card deleted successfully.",
  "data": null
}
```

**Error Response (400 Bad Request)**
```json
{
  "success": false,
  "message": "Cannot delete RFID card that is assigned to a student.",
  "data": null
}
```

---

## ESP32 Integration

### RFID Registration Station

The ESP32 sketch `rfid_register.ino` automatically:

1. Scans RFID cards
2. Generates random 8-character codes
3. Registers to backend via `POST /api/v1/rfids`

**Flow:**
```
Scan RFID → Generate Code → POST to Backend → Success/Error
```

**Example Output:**
```
Found RFID card!
UID: 04A1B2C3D4E5F6 (7 bytes)
Generated Code: K3X9M2A7
POST http://192.168.1.4:5289/api/v1/rfids
Payload: {"rfidUid":"04A1B2C3D4E5F6","rfidCode":"K3X9M2A7"}
HTTP 200
✓ RFID card registered successfully!
   RFID card registered successfully. Code: K3X9M2A7

✓ Registration Complete!
   Students can now use this code to register.
```

---

## Workflow

### 1. Register RFID Cards (Admin/Staff)

Use ESP32 registration station to scan and register RFID cards:

```
ESP32 → Scan Card → Generate Code → POST /api/v1/rfids
```

### 2. Distribute Cards to Students

Give physical RFID cards to students with the printed code.

### 3. Students Self-Register

Students use the Flutter app to register their RFID:

```
POST /api/v1/users/students/me/register-rfid
Body: { "rfidCode": "ABC12345" }
```

Backend looks up the RFID in the `Rfids` table and assigns it to the student.

---

## Database Schema

```sql
CREATE TABLE "Rfids" (
    "RfidUid" VARCHAR(100) PRIMARY KEY,
    "RfidCode" VARCHAR(100) UNIQUE NOT NULL
);
```

---

## Testing

### Test with cURL

**Register RFID Card:**
```bash
curl -X POST http://192.168.1.4:5289/api/v1/rfids \
  -H "Content-Type: application/json" \
  -d '{
    "rfidUid": "04A1B2C3D4E5F6",
    "rfidCode": "TEST1234"
  }'
```

**Get All RFID Cards:**
```bash
curl -X GET http://192.168.1.4:5289/api/v1/rfids \
  -H "Authorization: Bearer YOUR_TOKEN"
```

**Delete RFID Card:**
```bash
curl -X DELETE http://192.168.1.4:5289/api/v1/rfids/04A1B2C3D4E5F6 \
  -H "Authorization: Bearer YOUR_TOKEN"
```

---

## Error Handling

| Error | Cause | Solution |
|-------|-------|----------|
| 409 Conflict | RFID UID already exists | Use a different card |
| 409 Conflict | RFID Code already exists | Generate new code |
| 400 Bad Request | Cannot delete assigned RFID | Unassign from student first |
| 404 Not Found | RFID card doesn't exist | Check UID is correct |

---

## SQL Queries

### View all RFID cards
```sql
SELECT * FROM "Rfids" ORDER BY "RfidCode";
```

### Check if RFID is assigned
```sql
SELECT s."StudentIdNumber", s."FirstName", s."LastName", s."RfidCode"
FROM "Students" s
WHERE s."RfidUid" = '04A1B2C3D4E5F6';
```

### Manually add RFID card
```sql
INSERT INTO "Rfids" ("RfidUid", "RfidCode")
VALUES ('04A1B2C3D4E5F6', 'ABC12345');
```

### Delete unassigned RFID card
```sql
DELETE FROM "Rfids"
WHERE "RfidUid" = '04A1B2C3D4E5F6';
```

---

## Summary

The RFID Cards API provides:

✅ **ESP32 Integration** - Automatic registration via IoT device  
✅ **Duplicate Prevention** - Checks for existing UIDs and codes  
✅ **Student Assignment** - Links RFID cards to students  
✅ **Admin Management** - View and delete RFID cards  
✅ **Safety Checks** - Prevents deletion of assigned cards  

This system ensures all RFID cards are tracked before being assigned to students.
