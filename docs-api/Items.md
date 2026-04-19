# Items API

Manages physical inventory items including creation, retrieval, updates, RFID registration, location tracking, and archiving.

---

## Authentication

All endpoints require a Bearer token with **Admin or Staff** role unless marked as `AllowAnonymous`.

```
Authorization: Bearer <your_token>
```

---

## Base URL

```
/api/v1/items
```

---

## Endpoints

### 1. Create Item

```
POST /api/v1/items
```

Creates a new inventory item. Accepts `multipart/form-data` to support image upload.

**Auth:** Admin or Staff

**Request Body** (multipart/form-data)

| Field          | Type   | Required | Description                     |
| -------------- | ------ | -------- | ------------------------------- |
| `serialNumber` | string | Yes      | 3–50 characters, must be unique |
| `itemName`     | string | Yes      |                                 |
| `itemType`     | string | Yes      |                                 |
| `itemMake`     | string | Yes      | Manufacturer/brand              |
| `category`     | string | Yes      | See Category Values below       |
| `condition`    | string | Yes      | See Condition Values below      |
| `itemModel`    | string | No       |                                 |
| `description`  | string | No       |                                 |
| `rfidUid`      | string | No       | Pre-assign an RFID tag          |
| `image`        | file   | No       | Item photo                      |

**Category Values:** `Electronics`, `Furniture`, `Equipment`, `Supplies`, `Other` _(verify against your Enums)_

**Condition Values:** `New`, `Good`, `Fair`, `Poor`, `Damaged` _(verify against your Enums)_

```http
POST /api/v1/items
Authorization: Bearer <token>
Content-Type: multipart/form-data

serialNumber=SN-00123
itemName=HDMI Cable
itemType=Cable
itemMake=Generic
category=Electronics
condition=Good
```

**Responses**

| Status | Meaning                      |
| ------ | ---------------------------- |
| `201`  | Item created                 |
| `400`  | Validation error             |
| `409`  | Serial number already exists |

---

### 2. Get All Items

```
GET /api/v1/items
```

Returns all inventory items.

**Auth:** Admin or Staff

```http
GET /api/v1/items
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning             |
| ------ | ------------------- |
| `200`  | Items list returned |

---

### 3. Get Item by ID

```
GET /api/v1/items/{id}
```

Returns a single item by its GUID.

**Auth:** Admin or Staff

```http
GET /api/v1/items/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning        |
| ------ | -------------- |
| `200`  | Item found     |
| `404`  | Item not found |

---

### 4. Get Item by Serial Number

```
GET /api/v1/items/by-serial/{serialNumber}
```

Looks up an item by its serial number.

**Auth:** Admin or Staff

```http
GET /api/v1/items/by-serial/SN-00123
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning        |
| ------ | -------------- |
| `200`  | Item found     |
| `404`  | Item not found |

---

### 5. Import Items from File

```
POST /api/v1/items/import
```

Bulk-imports items from an Excel (`.xlsx`, `.xls`) or CSV file.

**Auth:** Admin or Staff

**Expected columns:** `SerialNumber`, `ItemName`, `ItemType`, `ItemModel`, `ItemMake`, `Description`, `Category`, `Condition`, `Image`

**Request Body** (multipart/form-data)

| Field  | Type | Required | Description                |
| ------ | ---- | -------- | -------------------------- |
| `file` | file | Yes      | `.xlsx`, `.xls`, or `.csv` |

```http
POST /api/v1/items/import
Authorization: Bearer <token>
Content-Type: multipart/form-data

file=<your_file>
```

**Responses**

| Status | Meaning                         |
| ------ | ------------------------------- |
| `200`  | Import completed (check counts) |
| `400`  | No valid items found            |
| `415`  | Unsupported file type           |
| `500`  | Server error during import      |

**Response Data**

```json
{
  "success": true,
  "message": "Import completed. Success: 10, Failed: 2",
  "data": {
    "successCount": 10,
    "failureCount": 2,
    "errors": ["Row 3: Duplicate serial number", "Row 7: Missing ItemName"]
  }
}
```

---

### 6. Update Item

```
PATCH /api/v1/items/{id}
```

Partially updates an item. Accepts `multipart/form-data` to support image replacement.

**Auth:** Admin or Staff

**Path Parameter**

| Parameter | Type | Description |
| --------- | ---- | ----------- |
| `id`      | guid | Item's ID   |

**Request Body** (multipart/form-data) — all fields optional

| Field         | Type   | Description             |
| ------------- | ------ | ----------------------- |
| `itemName`    | string |                         |
| `itemType`    | string |                         |
| `itemMake`    | string |                         |
| `itemModel`   | string |                         |
| `description` | string |                         |
| `category`    | string |                         |
| `condition`   | string |                         |
| `image`       | file   | Replaces existing image |

```http
PATCH /api/v1/items/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
Content-Type: multipart/form-data

condition=Fair
```

**Responses**

| Status | Meaning          |
| ------ | ---------------- |
| `200`  | Item updated     |
| `400`  | Validation error |
| `404`  | Item not found   |

---

### 7. RFID Scan (Toggle Status)

```
PATCH /api/v1/items/rfid-scan/{rfidUid}
```

Called by the IoT RFID scanner. Toggles the item's status between `Available` and `Borrowed`.

**Auth:** None (AllowAnonymous — IoT device endpoint)

```http
PATCH /api/v1/items/rfid-scan/A1B2C3D4
```

**Responses**

| Status | Meaning                               |
| ------ | ------------------------------------- |
| `200`  | Status toggled, new status in message |
| `400`  | Bad request                           |
| `404`  | No item registered to this RFID       |

---

### 8. Get Item by RFID

```
GET /api/v1/items/rfid/{rfidUid}
```

Resolves an item by its RFID tag UID. Used by the ESP32 borrow scanner.

**Auth:** None (AllowAnonymous — IoT device endpoint)

```http
GET /api/v1/items/rfid/A1B2C3D4
```

**Responses**

| Status | Meaning                         |
| ------ | ------------------------------- |
| `200`  | Item found                      |
| `404`  | No item registered to this RFID |

---

### 9. Register RFID to Item

```
POST /api/v1/items/{id}/register-rfid
```

Assigns a scanned RFID UID to the specified item. Called by the ESP32 in registration mode.

**Auth:** None (AllowAnonymous — IoT device endpoint)

**Path Parameter**

| Parameter | Type | Description |
| --------- | ---- | ----------- |
| `id`      | guid | Item's ID   |

**Request Body** (JSON)

| Field     | Type   | Required | Description  |
| --------- | ------ | -------- | ------------ |
| `rfidUid` | string | Yes      | RFID tag UID |

```http
POST /api/v1/items/3fa85f64-5717-4562-b3fc-2c963f66afa6/register-rfid
Content-Type: application/json

{
  "rfidUid": "A1B2C3D4"
}
```

**Responses**

| Status | Meaning                               |
| ------ | ------------------------------------- |
| `200`  | RFID registered                       |
| `404`  | Item not found                        |
| `409`  | RFID already assigned to another item |

---

### 10. Update Item Location

```
POST /api/v1/items/{id}/update-location
```

Updates the physical location of an item. Called by the ESP32 location tracker.

**Auth:** None (AllowAnonymous — IoT device endpoint)

**Path Parameter**

| Parameter | Type | Description |
| --------- | ---- | ----------- |
| `id`      | guid | Item's ID   |

**Request Body** (JSON)

| Field      | Type   | Required | Description         |
| ---------- | ------ | -------- | ------------------- |
| `location` | string | Yes      | Location label/name |

```http
POST /api/v1/items/3fa85f64-5717-4562-b3fc-2c963f66afa6/update-location
Content-Type: application/json

{
  "location": "Room 204 - Cabinet A"
}
```

**Responses**

| Status | Meaning          |
| ------ | ---------------- |
| `200`  | Location updated |
| `400`  | Bad request      |
| `404`  | Item not found   |

---

### 11. Archive Item

```
DELETE /api/v1/items/archive/{id}
```

Soft-deletes an item by moving it to the archive. Irreversible from this endpoint — use the Archive Items API to restore.

**Auth:** Admin only

```http
DELETE /api/v1/items/archive/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning        |
| ------ | -------------- |
| `200`  | Item archived  |
| `400`  | Archive failed |
| `404`  | Item not found |

---

## Response Shape

```json
{
  "success": true,
  "message": "Item retrieved successfully.",
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "serialNumber": "SN-00123",
    "itemName": "HDMI Cable",
    "itemType": "Cable",
    "itemMake": "Generic",
    "itemModel": null,
    "description": null,
    "category": "Electronics",
    "condition": "Good",
    "status": "Available",
    "rfidUid": "A1B2C3D4",
    "location": "Room 204 - Cabinet A",
    "imageUrl": "https://..."
  },
  "errors": null
}
```
