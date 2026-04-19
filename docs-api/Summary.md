# Summary API

Provides statistical summaries and dashboard data for the system.

---

## Authentication

All endpoints require a Bearer token with **Admin or Staff** role.

```
Authorization: Bearer <your_token>
```

---

## Base URL

```
/api/v1/summary
```

---

## Endpoints

### 1. Get Overall Summary

```
GET /api/v1/summary
```

Returns a comprehensive snapshot of the system including item counts, lending transaction totals, active user counts, and per-item stock information (available vs borrowed).

**Auth:** Admin or Staff

```http
GET /api/v1/summary
Authorization: Bearer <token>
```

**Responses**

| Status | Meaning          |
| ------ | ---------------- |
| `200`  | Summary returned |

---

## Response Shape

```json
{
  "success": true,
  "message": "Overall summary with stock information retrieved successfully.",
  "data": {
    "totalItems": 120,
    "totalLentItems": 45,
    "totalActiveUsers": 200,
    "stock": [
      {
        "itemName": "HDMI Cable",
        "totalCount": 10,
        "availableCount": 7,
        "borrowedCount": 3
      },
      {
        "itemName": "Projector",
        "totalCount": 5,
        "availableCount": 3,
        "borrowedCount": 2
      }
    ]
  },
  "errors": null
}
```
