# Health API

Exposes the application's health status and dependency checks.

---

## Authentication

No authentication required.

---

## Base URL

```
/api/v1/health
```

---

## Endpoints

### 1. Get Health Status

```
GET /api/v1/health
```

Runs all registered health checks (e.g., database connectivity) and returns a detailed report.

**Auth:** None

```http
GET /api/v1/health
```

**Responses**

| Status | Meaning                                  |
| ------ | ---------------------------------------- |
| `200`  | All checks healthy                       |
| `503`  | One or more checks degraded or unhealthy |

---

## Response Shape

```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.0123456",
  "entries": {
    "database": {
      "status": "Healthy",
      "duration": "00:00:00.0100000",
      "description": null,
      "data": {}
    }
  }
}
```

> Note: This endpoint returns the raw `HealthReport` object directly, not the standard `ApiResponse<T>` wrapper.
