# Auth API

Handles user registration, login, logout, token management, and password changes.

---

## Authentication

Most endpoints require a Bearer token. Exceptions are noted per endpoint.

```
Authorization: Bearer <your_token>
```

---

## Base URL

```
/api/v1/auth
```

---

## Endpoints

### 1. Get My Profile

```
GET /api/v1/auth/me
```

Returns the profile of the currently authenticated user.

**Auth:** Required (any role)

```http
GET /api/v1/auth/me
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning               |
| ------ | --------------------- |
| `200`  | Profile returned      |
| `401`  | Invalid/missing token |
| `404`  | User not found        |

---

### 2. Register User

```
POST /api/v1/auth/register
```

Creates a new user. Role hierarchy is enforced: SuperAdmin can create all roles, Admin cannot create SuperAdmin, Staff can only create Teacher and Student.

**Auth:** Required — `SuperAdmin`, `Admin`, or `Staff`

**Request Body** (JSON)

| Field             | Type   | Required | Description                                             |
| ----------------- | ------ | -------- | ------------------------------------------------------- |
| `username`        | string | Yes      | Unique username                                         |
| `lastName`        | string | Yes      |                                                         |
| `firstName`       | string | Yes      |                                                         |
| `middleName`      | string | No       |                                                         |
| `email`           | string | Yes      | Valid email address                                     |
| `phoneNumber`     | string | Yes      | Exactly 11 digits                                       |
| `role`            | string | Yes      | `SuperAdmin`, `Admin`, `Staff`, `Teacher`, or `Student` |
| `password`        | string | Yes      | Min 8 chars, must include upper, lower, digit, special  |
| `confirmPassword` | string | Yes      | Must match `password`                                   |

Additional fields for `Student` role:

| Field                   | Type   | Required | Description         |
| ----------------------- | ------ | -------- | ------------------- |
| `studentIdNumber`       | string | No       |                     |
| `course`                | string | No       |                     |
| `section`               | string | Yes      |                     |
| `year`                  | string | Yes      |                     |
| `street`                | string | Yes      | Home address        |
| `cityMunicipality`      | string | Yes      |                     |
| `province`              | string | Yes      |                     |
| `postalCode`            | string | Yes      |                     |
| `frontStudentIdPicture` | file   | No       | multipart/form-data |
| `backStudentIdPicture`  | file   | No       | multipart/form-data |
| `profilePicture`        | file   | No       | multipart/form-data |

Additional fields for `Teacher` role:

| Field        | Type   | Required |
| ------------ | ------ | -------- |
| `department` | string | Yes      |

Additional fields for `Staff` role:

| Field      | Type   | Required |
| ---------- | ------ | -------- |
| `position` | string | No       |

```http
POST /api/v1/auth/register
Authorization: Bearer <token>
Content-Type: application/json

{
  "username": "jdelacruz",
  "lastName": "Dela Cruz",
  "firstName": "Juan",
  "email": "juan@example.com",
  "phoneNumber": "09123456789",
  "role": "Student",
  "password": "Password1!",
  "confirmPassword": "Password1!",
  "section": "A",
  "year": "3",
  "street": "123 Main St",
  "cityMunicipality": "Manila",
  "province": "Metro Manila",
  "postalCode": "1000"
}
```

**Responses**

| Status | Meaning                          |
| ------ | -------------------------------- |
| `201`  | User created                     |
| `401`  | Not authenticated                |
| `403`  | Not authorized to create role    |
| `409`  | Username or email already exists |

---

### 3. Login

```
POST /api/v1/auth/login
```

Authenticates a user and returns a JWT access token (set as a cookie for web clients).

**Auth:** None

**Request Body** (JSON)

| Field      | Type   | Required |
| ---------- | ------ | -------- |
| `username` | string | Yes      |
| `password` | string | Yes      |

```http
POST /api/v1/auth/login
Content-Type: application/json

{
  "username": "jdelacruz",
  "password": "Password1!"
}
```

**Responses**

| Status | Meaning             |
| ------ | ------------------- |
| `200`  | Login successful    |
| `401`  | Invalid credentials |

---

### 4. Login (Mobile)

```
POST /api/v1/auth/login-mobile
```

Same as login but returns both the access token and refresh token in the response body — intended for mobile/API clients.

**Auth:** None

**Request Body** — same as Login

```http
POST /api/v1/auth/login-mobile
Content-Type: application/json

{
  "username": "jdelacruz",
  "password": "Password1!"
}
```

**Response Data**

```json
{
  "success": true,
  "message": "Mobile login successful.",
  "data": {
    "user": {
      "id": "...",
      "username": "jdelacruz",
      "email": "juan@example.com",
      "role": "Student"
    },
    "accessToken": "<jwt>",
    "refreshToken": "<refresh_token>"
  }
}
```

---

### 5. Logout

```
POST /api/v1/auth/logout
```

Logs out the current user (clears the auth cookie).

**Auth:** Required (any role)

```http
POST /api/v1/auth/logout
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning           |
| ------ | ----------------- |
| `200`  | Logout successful |
| `401`  | Not authenticated |

---

### 6. Refresh Token

```
POST /api/v1/auth/refresh-token
```

Issues a new access token using the refresh token stored in the HTTP-only cookie. For web clients.

**Auth:** None (uses cookie)

```http
POST /api/v1/auth/refresh-token
```

**Responses**

| Status | Meaning                       |
| ------ | ----------------------------- |
| `200`  | New access token returned     |
| `401`  | Refresh token invalid/expired |

---

### 7. Refresh Token (Mobile)

```
POST /api/v1/auth/refresh-token-mobile
```

Issues a new access token and refresh token using a refresh token from the request body. For mobile/API clients.

**Auth:** None

**Request Body** (JSON)

| Field          | Type   | Required |
| -------------- | ------ | -------- |
| `refreshToken` | string | Yes      |

```http
POST /api/v1/auth/refresh-token-mobile
Content-Type: application/json

{
  "refreshToken": "<your_refresh_token>"
}
```

---

### 8. Change Password

```
PATCH /api/v1/auth/change-password/{userId}
```

Changes the password for the specified user.

**Auth:** Required (any authenticated user)

**Path Parameter**

| Parameter | Type | Description      |
| --------- | ---- | ---------------- |
| `userId`  | guid | Target user's ID |

**Request Body** (JSON)

| Field             | Type   | Required |
| ----------------- | ------ | -------- |
| `currentPassword` | string | Yes      |
| `newPassword`     | string | Yes      |
| `confirmPassword` | string | Yes      |

```http
PATCH /api/v1/auth/change-password/3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
Content-Type: application/json

{
  "currentPassword": "OldPass1!",
  "newPassword": "NewPass1!",
  "confirmPassword": "NewPass1!"
}
```

**Responses**

| Status | Meaning           |
| ------ | ----------------- |
| `200`  | Password changed  |
| `400`  | Validation error  |
| `401`  | Not authenticated |

---

## Response Shape

```json
{
  "success": true,
  "message": "Login successful.",
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "username": "jdelacruz",
    "email": "juan@example.com",
    "role": "Student",
    "token": "<jwt>"
  },
  "errors": null
}
```
