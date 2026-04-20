# Backend Technical Assets Management

A comprehensive, high-performance ASP.NET Core 8 Web API designed for managing technical assets, streamlining lending operations, scaling user management, and providing real-time data integrations.

## 🚀 Quick Start

**Already set up?** Use the `Makefile` commands to get started:
```bash
make dev    # Development mode with hot reload
```

**First time here?** Jump to the [First Time Setup](#-first-time-setup) section below.

The API will be available at: **http://localhost:5278**
Documentation will be accessible at: **http://localhost:5278/swagger**

## ✨ Features

- **Advanced Item Management**: Track technical assets via serial numbers, automatic barcode generation, and the newly integrated RFID validation system. Supports status tracking, conditions, and images.
- **Lending & Returns**: Robust checkout and return workflows. Implemented robust timezone support and tracking metrics for long-term and short-term borrows.
- **Real-Time Notifications (SignalR)**: Seamless push notifications for user actions, lending state changes, and system alerts via WebSockets.
- **Real-Time Stock Tracking**: Provides real-time dynamic inventory levels, aggregated by item types or tags, taking borrows/returns into account instantaneously.
- **Excel Bulk Import**: Smooth ingestion of large inventory data from Excel files (`.xlsx`), with condition tracking and auto-generation functionalities.
- **Rich User Management Array**: Supports comprehensive Role-Based Access Control (RBAC) (SuperAdmin, Admin, Staff, Guest, Student). Features soft-delete and structured user archiving.
- **Activity Logging & Audits**: Continuous traceability for all core actions via a dedicated activity logging pipeline.
- **Cloud-Ready Data Models**: Optimized for PostgreSQL (Supabase) via Entity Framework Core with seamless schema migrations.

## 🛠️ Technology Stack

- **Framework**: .NET 8.0 / ASP.NET Core Web API
- **Database**: PostgreSQL (leveraging **Supabase**) via Entity Framework Core (Npgsql)
- **Authentication/Security**: JWT Bearer Tokens, Custom Authorization Handlers
- **Real-time Engine**: SignalR WebSockets
- **Object Mapping**: AutoMapper
- **API Documentation**: Swagger/OpenAPI UI & Scalar API Reference
- **Testing Engine**: xUnit, Moq, and integration tests suite

## 🎯 First Time Setup

### 1. Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Make Utility properly installed (`winget install GnuWin32.Make` for Windows, `brew install make` for macOS)
- A **Supabase** Project or local PostgreSQL instance.

### 2. Configuration & Secrets

Duplicate the local environment template and point to your PostgreSQL / Supabase server:
1. Rename `.env.example` to `.env` if not present.
2. Provide your connection string:
```env
# .env Configuration Example
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection="Host=your_supabase_host;Database=postgres;Username=postgres;Password=your_password"
ConnectionStrings__Supabase="Host=your_supabase_host;Database=postgres;Username=postgres;Password=your_password"
Jwt__Key=YourSuperSecretKeyThatIsAtLeast32Bytes!
Jwt__Issuer=TechnicalAssetsAPI
```
Ensure your `appsettings.json` and `appsettings.Development.json` values are mirrored or properly substituted with your `.env` secrets.

### 3. Setup and Run
Use Make to automate setup pipelines if available, or manually run Dotnet CLI commands.
```bash
# Using Dotnet CLI (recommended if Makefile is unconfigured)
dotnet restore
dotnet ef database update
dotnet watch run --launch-profile http
```
*(Optionally, use local `make restore`, `make migrate`, and `make dev`)*

### 4. Verify & Initialize

Open **http://localhost:5278/swagger** — you should be presented with the Scalar/Swagger visual API documentation. To seed your initial administrator:
Use the `/api/v1/auth/register` endpoint to create your first `Admin` or `SuperAdmin` role locally.

## 📊 API & System Architecture

The core directories are modeled to strictly adhere to dependency injection and separation of concerns (`src/` folder):

- **Controllers** (`src/Controllers`): Inbound HTTP routing and endpoint mapping.
- **Services** (`src/Services`): Core Business logic. Translates external DTOs into rich domain rules.
- **Repositories** (`src/Repository`): Abstraction over EF Core contexts for fetching, updating, tracking PostgreSQL states.
- **Hubs** (`src/Hubs`): SignalR Hub endpoints (`/notificationHub`).
- **DTOs** (`src/DTOs`): Immutable data transfer shapes for inputs/outputs.
- **BackgroundServices** (`src/BackgroundServices`): Hosted workloads, like the `RefreshTokenCleanupService` or Reservation expiration managers.

### Important Route Prefixes:
- `GET /api/v1/summary` - Aggregated live stock, active lent items, and data reports.
- `POST /api/v1/items/bulk-import` - Multi-part file upload form for XLSX mass ingestion.
- `GET /api/v1/items`, `api/v1/lentItems`, `api/v1/users` - Standard decoupled resource management logic.

## 🔐 Authentication & Setup

The API uses **JWT Token authorization**. Place the retrieved token into your client headers:
```http
Authorization: Bearer <your_retrieved_jwt_token>
```
To enable Cross-Origin Resource Sharing (CORS), the system specifies an unrestricted policy in development (including mobile app emulator ranges like `10.0.2.2`).

## 📚 Advanced Documentation

For deeper dives into complex logic flows, database structure, and deployment strategies, see our dedicated docs:

- **[RFID System Improvements](docs/RFID_System_Improvements.md)** - Details on bridging hardware scanners to the backend platform.
- **[Deployment & Azure Migrations](docs/Azure_Architecture_Overview.md)** - In-depth cloud infrastructure, scaling via Azure, or Railway.
- **[Testing Strategies](docs/TestChecklist.md)** - Details on running xUnit mocks effectively.
- **[Supabase Guidelines](docs/Supabase_Migration_Checklist.md)** - PostreSQL schema checklists.

## 🧪 Testing

Run standard unit and integration pipelines directly:
```bash
dotnet test
# Or using Make
make test
```

## 🤝 Contributing

1. Create a feature branch based on your modifications.
2. Run formatters and ensure `dotnet test` completes comprehensively.
3. Keep dependency injection explicitly registered in `Program.cs` under its proper scope mapping.
4. Submit your Pull Request.

---
**Made with ❤️ for efficient technical assets management.**
