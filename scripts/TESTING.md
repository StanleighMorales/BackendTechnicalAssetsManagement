# API Testing

## Scripts

| File                     | Description                                                |
| ------------------------ | ---------------------------------------------------------- |
| `quick-test.ps1`         | Interactive script — prompts for login then runs all tests |
| `test-api-endpoints.ps1` | Core test runner, can be called directly with params       |

## Usage

### Interactive (recommended)

```powershell
.\scripts\quick-test.ps1
```

Prompts for environment, optional login, then runs all tests automatically.

### Direct

```powershell
# No auth (public endpoints only)
.\scripts\test-api-endpoints.ps1 -BaseUrl "http://localhost:5278"

# With auth token
.\scripts\test-api-endpoints.ps1 -BaseUrl "http://localhost:5278" -Token "your-jwt-token"

# Skip auth-required endpoints
.\scripts\test-api-endpoints.ps1 -BaseUrl "http://localhost:5278" -SkipAuth
```

> Note: Login uses `/api/v1/auth/login-mobile` to get the token in the response body (the standard `/login` sets it as an HttpOnly cookie).

## Result Statuses

| Status  | Meaning                                                     |
| ------- | ----------------------------------------------------------- |
| PASSED  | Endpoint responded with an expected status code             |
| FAILED  | 404 or 500+ — needs investigation                           |
| WARNING | 400 / 401 / 403 — expected for test data without valid auth |
| SKIPPED | Requires auth token, none provided                          |

## Output

Results are exported to a timestamped CSV in the project root:

```
api-test-results_YYYYMMDD_HHMMSS.csv
```

## Endpoints Covered

### Public

- `GET /` — Root
- `GET /health` — Health check
- `GET /api/v1/health` — Detailed health

### Auth

- `POST /api/v1/auth/login`
- `POST /api/v1/auth/login-mobile`
- `POST /api/v1/auth/register`
- `GET  /api/v1/auth/me`
- `POST /api/v1/auth/logout`
- `POST /api/v1/auth/refresh-token`
- `POST /api/v1/auth/refresh-token-mobile`

### Users

- `GET    /api/v1/users`
- `GET    /api/v1/users/{id}`
- `PATCH  /api/v1/users/students/profile/{id}`
- `PATCH  /api/v1/users/teachers/profile/{id}`
- `PATCH  /api/v1/users/admin-or-staff/profile/{id}`
- `DELETE /api/v1/users/archive/{id}`
- `POST   /api/v1/users/students/import`
- `GET    /api/v1/users/students/by-id-number/{idNumber}`
- `POST   /api/v1/users/students/{id}/register-rfid`
- `GET    /api/v1/users/students/rfid/{rfidUid}`

### Items

- `GET    /api/v1/items`
- `GET    /api/v1/items/{id}`
- `GET    /api/v1/items/by-serial/{serialNumber}`
- `POST   /api/v1/items`
- `POST   /api/v1/items/import`
- `PATCH  /api/v1/items/{id}`
- `PATCH  /api/v1/items/rfid-scan/{rfidUid}`
- `GET    /api/v1/items/rfid/{rfidUid}`
- `POST   /api/v1/items/{id}/register-rfid`
- `POST   /api/v1/items/{id}/update-location`
- `DELETE /api/v1/items/archive/{id}`

### Lent Items

- `GET    /api/v1/lentItems`
- `GET    /api/v1/lentItems/{id}`
- `GET    /api/v1/lentItems/date/{dateTime}`
- `POST   /api/v1/lentItems`
- `POST   /api/v1/lentItems/guests`
- `PATCH  /api/v1/lentItems/{id}`
- `PATCH  /api/v1/lentItems/hide/{id}`
- `DELETE /api/v1/lentItems/archive/{id}`

### Summary

- `GET /api/v1/summary`

### Archives

- `GET    /api/v1/archiveitems`
- `GET    /api/v1/archiveitems/{id}`
- `DELETE /api/v1/archiveitems/restore/{id}`
- `DELETE /api/v1/archiveitems/{id}`
- `GET    /api/v1/archivelentitems`
- `GET    /api/v1/archivelentitems/{id}`
- `DELETE /api/v1/archivelentitems/restore/{id}`
- `DELETE /api/v1/archivelentitems/{id}`
- `GET    /api/v1/archiveusers`
- `GET    /api/v1/archiveusers/{id}`
- `DELETE /api/v1/archiveusers/restore/{id}`
- `DELETE /api/v1/archiveusers/{id}`
