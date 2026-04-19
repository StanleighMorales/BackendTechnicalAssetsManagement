# Archive Lent Items API

Manages archived lent item records. Records land here after being archived via the Lent Items API.

---

## Authentication

All endpoints require a Bearer token.

```
Authorization: Bearer <your_token>
```

---

## Base URL

```
/api/v1/archivelentitems
```

---

## Endpoints

### 1. Get All Archived Lent Items

```
GET /api/v1/archivelentitems
```

Returns all archived lent item records.

**Auth:** Required (any authenticated user)

```http
GET /api/v1/archivelentitems
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                           |
| ------ | --------------------------------- |
| `200`  | Archived lent items list returned |

---

### 2. Get Archived Lent Item by ID

```
GET /api/v1/archivelentitems/{id}
```

Returns a single archived lent item record by its GUID.

**Auth:** Required (any authenticated user)

**Path Parameter**

| Parameter | Type | Description                  |
| --------- | ---- | ---------------------------- |
| `id`      | guid | Archived lent item record ID |

```http
GET /api/v1/archivelentitems/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning      |
| ------ | ------------ |
| `200`  | Record found |
| `404`  | Not found    |

---

### 3. Restore Archived Lent Item

```
DELETE /api/v1/archivelentitems/restore/{id}
```

Moves the lent item record back to the active lent items table.

**Auth:** Required (any authenticated user)

**Path Parameter**

| Parameter | Type | Description                  |
| --------- | ---- | ---------------------------- |
| `id`      | guid | Archived lent item record ID |

```http
DELETE /api/v1/archivelentitems/restore/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning         |
| ------ | --------------- |
| `200`  | Record restored |
| `404`  | Not found       |

---

### 4. Permanently Delete Archived Lent Item

```
DELETE /api/v1/archivelentitems/{id}
```

Permanently removes an archived lent item record. This action is irreversible.

**Auth:** Required (any authenticated user)

**Path Parameter**

| Parameter | Type | Description                  |
| --------- | ---- | ---------------------------- |
| `id`      | guid | Archived lent item record ID |

```http
DELETE /api/v1/archivelentitems/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning             |
| ------ | ------------------- |
| `200`  | Permanently deleted |
| `404`  | Not found           |

---

## Response Shape

```json
{
  "success": true,
  "message": "Archived lent items retrieved successfully.",
  "data": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "itemName": "HDMI Cable",
      "borrowerFullName": "Juan Dela Cruz",
      "status": "Returned",
      "lentAt": "2026-04-10 09:00",
      "returnedAt": "2026-04-10 14:00",
      "archivedAt": "2026-04-15 10:00"
    }
  ],
  "errors": null
}
```
