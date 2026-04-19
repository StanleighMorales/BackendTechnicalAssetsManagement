# Archive Users API

Manages archived (soft-deleted) user accounts. Users land here after being archived via the Users API.

---

## Authentication

All endpoints require a Bearer token with **Admin or Staff** role. Permanent deletion is restricted to **Admin** only.

```
Authorization: Bearer <your_token>
```

---

## Base URL

```
/api/v1/archiveusers
```

---

## Endpoints

### 1. Get All Archived Users

```
GET /api/v1/archiveusers
```

Returns all archived user accounts.

**Auth:** Admin or Staff

```http
GET /api/v1/archiveusers
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                      |
| ------ | ---------------------------- |
| `200`  | Archived users list returned |

---

### 2. Get Archived User by ID

```
GET /api/v1/archiveusers/{id}
```

Returns a single archived user by their GUID.

**Auth:** Admin or Staff

**Path Parameter**

| Parameter | Type | Description        |
| --------- | ---- | ------------------ |
| `id`      | guid | Archived user's ID |

```http
GET /api/v1/archiveusers/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning             |
| ------ | ------------------- |
| `200`  | Archived user found |
| `404`  | Not found           |

---

### 3. Restore Archived User

```
DELETE /api/v1/archiveusers/restore/{archiveUserId}
```

Moves the user account back to the active users table.

**Auth:** Admin or Staff

**Path Parameter**

| Parameter       | Type | Description        |
| --------------- | ---- | ------------------ |
| `archiveUserId` | guid | Archived user's ID |

```http
DELETE /api/v1/archiveusers/restore/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                   |
| ------ | ------------------------- |
| `200`  | User restored             |
| `400`  | User not found in archive |

---

### 4. Permanently Delete Archived User

```
DELETE /api/v1/archiveusers/{id}
```

Permanently removes an archived user. This action is irreversible.

**Auth:** Admin only

**Path Parameter**

| Parameter | Type | Description        |
| --------- | ---- | ------------------ |
| `id`      | guid | Archived user's ID |

```http
DELETE /api/v1/archiveusers/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                  |
| ------ | ------------------------ |
| `200`  | User permanently deleted |
| `404`  | Not found                |

---

## Response Shape

```json
{
  "success": true,
  "message": "Archived users retrieved successfully.",
  "data": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "username": "jdelacruz",
      "firstName": "Juan",
      "lastName": "Dela Cruz",
      "email": "juan@example.com",
      "role": "Student",
      "archivedAt": "2026-04-15 10:00"
    }
  ],
  "errors": null
}
```
