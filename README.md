# Backend Technical Assets Management

A comprehensive ASP.NET Core Web API for managing technical assets, lending operations, and user management.

## 🚀 Quick Start

**Already set up?** Just run:
```bash
make dev    # Development mode with hot reload
```

**First time here?** Jump to [First Time Setup](#-first-time-setup) below.

The API will be available at: **http://localhost:5278**

## ✨ Features

- **Item Management**: Track technical assets with serial numbers (auto-uppercase), barcodes, and images
- **Lending System**: Manage item borrowing and returns with barcode scanning
- **User Management**: Support for multiple user roles (Admin, Staff, Teacher, Student)
- **Stock Tracking**: Real-time inventory levels grouped by item name
- **Excel Import**: Bulk import items from Excel files
- **Archive System**: Soft delete for users and items
- **Authentication**: JWT-based authentication with role-based access control

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT Bearer tokens
- **Testing**: xUnit with Moq
- **Documentation**: Swagger/OpenAPI

## 📋 Prerequisites

- .NET 8 SDK - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or SQL Server LocalDB
- Make (see installation below)
- (Optional) Visual Studio 2022, VS Code, or Rider

## 🎯 First Time Setup

### 1. Install Make

**Windows** (PowerShell as Administrator):
```powershell
winget install GnuWin32.Make
```
Then **restart your terminal**.

**Linux**:
```bash
sudo apt-get install build-essential  # Ubuntu/Debian
```

**macOS**:
```bash
brew install make
```

### 2. Clone and Navigate
```bash
git clone <your-repo-url>
cd BackendTechnicalAssetsManagement
```

### 3. Configure Database

Update `appsettings.json` if needed:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TechnicalAssetsDB;Trusted_Connection=true;MultipleActiveResultSets=true"
}
```

### 4. Setup and Run
```bash
make restore    # Restore packages
make migrate    # Setup database
make dev        # Run with hot reload
```

### 5. Verify
Open **http://localhost:5278/swagger** - you should see the API documentation.

### 6. Create First User

Use Swagger or the `.http` file:
```http
POST http://localhost:5278/api/v1/auth/register
Content-Type: application/json

{
  "username": "admin",
  "password": "YourSecurePassword123!",
  "email": "admin@example.com",
  "firstName": "Admin",
  "lastName": "User",
  "userRole": "Admin"
}
```

## 🏃 Common Commands

```bash
# Development
make dev         # Run with hot reload (recommended for development)
make run         # Run normally
make build       # Build project

# Database
make migrate     # Apply database migrations
make migration NAME=YourMigrationName  # Create new migration
make db-drop     # Drop database
make db-update   # Update to latest migration

# Testing
make test        # Run all tests
make test-watch  # Run tests in watch mode
make test-coverage  # Run with coverage

# Maintenance
make clean       # Clean build artifacts
make restore     # Restore NuGet packages
make help        # Show all available commands
```

### Manual Commands (without Make)

```bash
# Run
dotnet run --project BackendTechnicalAssetsManagement/BackendTechnicalAssetsManagement.csproj --launch-profile http

# Run with hot reload
dotnet watch run --project BackendTechnicalAssetsManagement/BackendTechnicalAssetsManagement.csproj --launch-profile http

# Build
dotnet build BackendTechnicalAssetsManagement/BackendTechnicalAssetsManagement.csproj

# Test
dotnet test BackendTechnicalAssetsManagementTest/BackendTechnicalAssetsManagementTest.csproj

# Migrations
dotnet ef migrations add MigrationName --project BackendTechnicalAssetsManagement/BackendTechnicalAssetsManagement.csproj
dotnet ef database update --project BackendTechnicalAssetsManagement/BackendTechnicalAssetsManagement.csproj
```

## 📊 API Endpoints

### Summary
- `GET /api/v1/summary` - Complete system summary with stock information

### Items
- `GET /api/v1/items` - Get all items
- `POST /api/v1/items` - Create new item
- `GET /api/v1/items/{id}` - Get item by ID
- `PUT /api/v1/items/{id}` - Update item
- `DELETE /api/v1/items/{id}` - Delete item

### Lent Items
- `GET /api/v1/lentItems` - Get all lent items
- `POST /api/v1/lentItems` - Create lending transaction
- `PATCH /api/v1/lentItems/return/item/{barcode}` - Return item by barcode
- `GET /api/v1/lentItems/date/{date}` - Get lent items by date

### Users
- `POST /api/v1/auth/register` - Register new user
- `POST /api/v1/auth/login` - Login
- `GET /api/v1/users` - Get all users
- `GET /api/v1/users/{id}` - Get user by ID
- `PUT /api/v1/users/{id}` - Update user
- `DELETE /api/v1/users/{id}` - Archive user

See full documentation at `/swagger` when running the application.

## 🔐 Authentication

The API uses JWT Bearer tokens. Include the token in requests:
```
Authorization: Bearer {your-jwt-token}
```

## 📦 Stock Tracking

The system automatically tracks inventory levels for items with the same name:

**Example**: If you have 10 "HDMI Cable" items with unique serial numbers:
- Total: 10
- Available: 7 (Status = Available)
- Borrowed: 3 (Status = Unavailable)

Stock counts update automatically when items are borrowed or returned.

## 📥 Excel Import

Import multiple items from Excel files with support for:
- Item details (name, type, make, model)
- Images (file paths or URLs)
- Condition tracking
- Automatic barcode generation
- All imported items automatically set to "Available" status

See [IMPORT_GUIDE.md](IMPORT_GUIDE.md) for Excel format and instructions.

## 🧪 Testing

Run all tests:
```bash
make test
```

See [TESTING_SETUP_GUIDE.md](TESTING_SETUP_GUIDE.md) for detailed testing information.

## 🎯 Project Structure

```
BackendTechnicalAssetsManagement/
├── Makefile                 # Command shortcuts
├── src/
│   ├── Classes/            # Domain models
│   ├── Controllers/        # API endpoints
│   ├── Services/           # Business logic
│   ├── Repository/         # Data access
│   ├── DTOs/              # Data transfer objects
│   └── Utils/             # Utilities and helpers
├── Migrations/            # EF Core migrations
└── Test/                  # Unit tests
```

## 🔧 Troubleshooting

### Port Already in Use
Stop any running instances or check Task Manager for `BackendTechnicalAssetsManagement.exe`

### Build Errors
```bash
make clean
make restore
make build
```

### Database Issues
```bash
make db-drop
make migrate
```

### Migration Failed
Verify SQL Server is running and check connection string in `appsettings.json`

## 📚 Additional Documentation

- **[IMPORT_GUIDE.md](IMPORT_GUIDE.md)** - Excel import functionality
- **[TESTING_SETUP_GUIDE.md](TESTING_SETUP_GUIDE.md)** - Testing guide
- **[INSTALL_MAKE.md](INSTALL_MAKE.md)** - Detailed Make installation

## 🤝 Contributing

1. Create a feature branch
2. Make your changes
3. Run tests: `make test`
4. Submit a pull request

## 📝 License

[Your License Here]

---

**Made with ❤️ for efficient asset management**
