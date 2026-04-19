# Archive Items API

Manages archived (soft-deleted) inventory items. Items land here after being archived via the Items API.

---

## Authentication

All endpoints require a Bearer token with **Admin or Staff** role. Restore and delete are restricted to **Admin** only.

```
Authorization: Bearer <your_token>
```

---

## Base URL

```
/api/v1/archiveitems
```

---

## Endpoints

### 1. Get All Archived Items

```
GET /api/v1/archiveitems
```

Returns all items currently in the archive.

**Auth:** Admin or Staff

```http
GET /api/v1/archiveitems
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                      |
| ------ | ---------------------------- |
| `200`  | Archived items list returned |

---

### 2. Get Archived Item by ID

```
GET /api/v1/archiveitems/{id}
```

Returns a single archived item by its GUID.

**Auth:** Admin or Staff

**Path Parameter**

| Parameter | Type | Description        |
| --------- | ---- | ------------------ |
| `id`      | guid | Archived item's ID |

```http
GET /api/v1/archiveitems/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning             |
| ------ | ------------------- |
| `200`  | Archived item found |
| `404`  | Not found           |

---

### 3. Restore Archived Item

```
DELETE /api/v1/archiveitems/restore/{id}
```

Moves the item back to the active items table.

**Auth:** Admin only

**Path Parameter**

| Parameter | Type | Description        |
| --------- | ---- | ------------------ |
| `id`      | guid | Archived item's ID |

```http
DELETE /api/v1/archiveitems/restore/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                 |
| ------ | ----------------------- |
| `200`  | Item restored           |
| `404`  | Archived item not found |

---

### 4. Permanently Delete Archived Item

```
DELETE /api/v1/archiveitems/{id}
```

Permanently removes an archived item. This action is irreversible.

**Auth:** Admin only

**Path Parameter**

| Parameter | Type | Description        |
| --------- | ---- | ------------------ |
| `id`      | guid | Archived item's ID |

```http
DELETE /api/v1/archiveitems/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                  |
| ------ | ------------------------ |
| `200`  | Item permanently deleted |
| `404`  | Not found                |

---

## Response Shape

```json
{
  "success": true,
  "message": "Archived items retrieved successfully.",
  "data": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "serialNumber": "SN-00123",
      "itemName": "HDMI Cable",
      "itemType": "Cable",
      "category": "Electronics",
      "condition": "Good",
      "archivedAt": "2026-04-10 14:00"
    }
  ],
  "errors": null
}
```
