# Users API

Manages user accounts — retrieving profiles, updating information, importing students, RFID registration, and archiving.

---

## Authentication

All endpoints require a Bearer token unless marked otherwise.

```
Authorization: Bearer <your_token>
```

---

## Base URL

```
/api/v1/users
```

---

## Endpoints

### 1. Get All Users

```
GET /api/v1/users
```

Returns a list of all users.

**Auth:** Admin or Staff

```http
GET /api/v1/users
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning             |
| ------ | ------------------- |
| `200`  | Users list returned |

---

### 2. Get User Profile by ID

```
GET /api/v1/users/{id}
```

Returns a specific user's profile. Resource-based authorization applies — users can only view their own profile unless they are Admin/Staff.

**Auth:** Required (any authenticated user)

**Path Parameter**

| Parameter | Type | Description |
| --------- | ---- | ----------- |
| `id`      | guid | User's ID   |

```http
GET /api/v1/users/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                             |
| ------ | ----------------------------------- |
| `200`  | Profile returned                    |
| `403`  | Not authorized to view this profile |
| `404`  | User not found                      |

---

### 3. Update Student Profile

```
PATCH /api/v1/users/students/profile/{id}
```

Updates a student's profile. Admin can update any student; a Student can only update their own.

**Auth:** Admin or Student

**Path Parameter**

| Parameter | Type | Description  |
| --------- | ---- | ------------ |
| `id`      | guid | Student's ID |

**Request Body** (multipart/form-data) — all fields optional

| Field                   | Type   | Description             |
| ----------------------- | ------ | ----------------------- |
| `firstName`             | string |                         |
| `lastName`              | string |                         |
| `middleName`            | string |                         |
| `email`                 | string |                         |
| `phoneNumber`           | string |                         |
| `course`                | string |                         |
| `section`               | string |                         |
| `year`                  | string |                         |
| `street`                | string |                         |
| `cityMunicipality`      | string |                         |
| `province`              | string |                         |
| `postalCode`            | string |                         |
| `profilePicture`        | file   | Replaces existing photo |
| `frontStudentIdPicture` | file   |                         |
| `backStudentIdPicture`  | file   |                         |

```http
PATCH /api/v1/users/students/profile/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
Content-Type: multipart/form-data

section=B
year=4
```

**Responses**

| Status | Meaning           |
| ------ | ----------------- |
| `200`  | Profile updated   |
| `400`  | Validation error  |
| `404`  | Student not found |
| `500`  | Server error      |

---

### 4. Update Teacher Profile

```
PATCH /api/v1/users/teachers/profile/{id}
```

Updates a teacher's profile. Admin can update any teacher; a Teacher can only update their own.

**Auth:** Admin or Teacher

**Path Parameter**

| Parameter | Type | Description  |
| --------- | ---- | ------------ |
| `id`      | guid | Teacher's ID |

**Request Body** (JSON) — all fields optional

| Field         | Type   | Description |
| ------------- | ------ | ----------- |
| `firstName`   | string |             |
| `lastName`    | string |             |
| `middleName`  | string |             |
| `email`       | string |             |
| `phoneNumber` | string |             |
| `department`  | string |             |

```http
PATCH /api/v1/users/teachers/profile/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
Content-Type: application/json

{
  "department": "Computer Science"
}
```

**Responses**

| Status | Meaning           |
| ------ | ----------------- |
| `200`  | Profile updated   |
| `403`  | Not authorized    |
| `404`  | Teacher not found |

---

### 5. Update Admin/Staff Profile

```
PATCH /api/v1/users/admin-or-staff/profile/{id}
```

Updates the profile of an Admin or Staff user. Users can only update their own profile.

**Auth:** SuperAdmin, Admin, or Staff

**Path Parameter**

| Parameter | Type | Description |
| --------- | ---- | ----------- |
| `id`      | guid | User's ID   |

**Request Body** (JSON) — all fields optional

| Field         | Type   | Description |
| ------------- | ------ | ----------- |
| `firstName`   | string |             |
| `lastName`    | string |             |
| `middleName`  | string |             |
| `email`       | string |             |
| `phoneNumber` | string |             |
| `position`    | string | Staff only  |

```http
PATCH /api/v1/users/admin-or-staff/profile/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
Content-Type: application/json

{
  "position": "Lab Technician"
}
```

**Responses**

| Status | Meaning           |
| ------ | ----------------- |
| `204`  | Updated (no body) |
| `401`  | Not authenticated |

---

### 6. Archive User

```
DELETE /api/v1/users/archive/{id}
```

Soft-deletes a user account. A user cannot archive their own account.

**Auth:** Admin or Staff

**Path Parameter**

| Parameter | Type | Description |
| --------- | ---- | ----------- |
| `id`      | guid | User's ID   |

```http
DELETE /api/v1/users/archive/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning                    |
| ------ | -------------------------- |
| `200`  | User archived              |
| `400`  | Cannot archive own account |
| `401`  | Invalid token              |
| `404`  | User not found             |

---

### 7. Import Students from File

```
POST /api/v1/users/students/import
```

Bulk-imports students from an Excel (`.xlsx`, `.xls`) or CSV file. Auto-generates usernames and passwords.

**Auth:** Admin or Staff

**Expected columns:** `LastName`, `FirstName`, `MiddleName` (optional)

**Request Body** (multipart/form-data)

| Field  | Type | Required | Description                |
| ------ | ---- | -------- | -------------------------- |
| `file` | file | Yes      | `.xlsx`, `.xls`, or `.csv` |

```http
POST /api/v1/users/students/import
Authorization: Bearer <token>
Content-Type: multipart/form-data

file=<your_file>
```

**Responses**

| Status | Meaning                         |
| ------ | ------------------------------- |
| `200`  | Import completed (check counts) |
| `415`  | Unsupported file type           |
| `500`  | Server error                    |

**Response Data**

```json
{
  "success": true,
  "message": "Import completed. Success: 15, Failed: 1",
  "data": {
    "successCount": 15,
    "failureCount": 1,
    "students": [
      {
        "username": "jdelacruz",
        "generatedPassword": "Temp@1234",
        "fullName": "Juan Dela Cruz"
      }
    ],
    "errors": ["Row 5: Missing FirstName"]
  }
}
```

---

### 8. Get Student by ID Number

```
GET /api/v1/users/students/by-id-number/{studentIdNumber}
```

Looks up a student by their school ID number.

**Auth:** Admin or Staff

```http
GET /api/v1/users/students/by-id-number/2021-00123
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning           |
| ------ | ----------------- |
| `200`  | Student found     |
| `404`  | Student not found |
| `500`  | Server error      |

---

### 9. Register RFID to Student

```
POST /api/v1/users/students/{id}/register-rfid
```

Assigns a scanned RFID UID to a student. Called by the ESP32 in student RFID registration mode.

**Auth:** None (AllowAnonymous — IoT device endpoint)

**Path Parameter**

| Parameter | Type | Description  |
| --------- | ---- | ------------ |
| `id`      | guid | Student's ID |

**Request Body** (JSON)

| Field     | Type   | Required | Description  |
| --------- | ------ | -------- | ------------ |
| `rfidUid` | string | Yes      | RFID tag UID |

```http
POST /api/v1/users/students/3fa85f64-5717-4562-b3fc-2c963f66afa6/register-rfid
Content-Type: application/json

{
  "rfidUid": "A1B2C3D4"
}
```

**Responses**

| Status | Meaning                                  |
| ------ | ---------------------------------------- |
| `200`  | RFID registered                          |
| `404`  | Student not found                        |
| `409`  | RFID already assigned to another student |

---

### 10. Get Student by RFID

```
GET /api/v1/users/students/rfid/{rfidUid}
```

Resolves a student by their ID card RFID UID. Used by the ESP32 borrow scanner.

**Auth:** None (AllowAnonymous — IoT device endpoint)

```http
GET /api/v1/users/students/rfid/A1B2C3D4
```

**Responses**

| Status | Meaning                            |
| ------ | ---------------------------------- |
| `200`  | Student found                      |
| `404`  | No student registered to this RFID |

---

## Response Shape

```json
{
  "success": true,
  "message": "User profile retrieved successfully.",
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "username": "jdelacruz",
    "firstName": "Juan",
    "lastName": "Dela Cruz",
    "email": "juan@example.com",
    "role": "Student",
    "studentIdNumber": "2021-00123",
    "course": "BSIT",
    "section": "A",
    "year": "3"
  },
  "errors": null
}
```
