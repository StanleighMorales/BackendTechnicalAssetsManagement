# Technical Assets Management System - Backend API

A comprehensive .NET 8 Web API for managing technical assets, lending operations, and user management in educational or organizational environments.

## 🚀 Features

### Core Functionality
- **Asset Management**: Complete CRUD operations for technical items with barcode generation
- **Lending System**: Track item loans, returns, and lending history
- **User Management**: Multi-role user system (SuperAdmin, Admin, Staff, Student, Teacher)
- **Archive System**: Maintain historical records of items, users, and lending operations
- **Barcode Integration**: Generate and manage barcodes for items and lending operations
- **Statistics & Reporting**: Comprehensive summary and analytics endpoints

### Technical Features
- **JWT Authentication**: Secure token-based authentication with refresh tokens
- **Role-based Authorization**: Granular permission system
- **Health Checks**: Built-in health monitoring for database connectivity
- **Excel Import/Export**: Bulk operations support
- **Image Handling**: Asset image management with MIME type support
- **Background Services**: Automated cleanup tasks
- **Global Exception Handling**: Centralized error management
- **CORS Support**: Cross-origin resource sharing for frontend integration

## 🛠️ Technology Stack

- **.NET 8**: Latest .NET framework
- **Entity Framework Core 8**: ORM with SQL Server support
- **AutoMapper**: Object-to-object mapping
- **BCrypt.Net**: Password hashing
- **JWT Bearer**: Authentication tokens
- **ZXing.Net**: Barcode generation
- **Swagger/OpenAPI**: API documentation
- **Scalar**: Enhanced API documentation UI
- **xUnit**: Testing framework

## 📋 Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

## ⚙️ Installation & Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd BackendTechnicalAssetsManagement
```

### 2. Environment Configuration
Copy the example environment file and configure your settings:
```bash
copy .env.example .env
```

Edit `.env` with your configuration:
```env
# Connection Strings
ConnectionStrings__DefaultConnection=Server=localhost;Database=TechnicalAssetsDB;Trusted_Connection=True;

# JWT Settings
AppSettings__Token=your-super-secure-jwt-secret-key-here

# Logging
Logging__LogLevel__Default=Information
Logging__LogLevel__Microsoft.AspNetCore=Warning
```

### 3. Database Setup
```bash
# Install EF Core tools (if not already installed)
dotnet tool install --global dotnet-ef

# Update database with migrations
dotnet ef database update
```

### 4. Run the Application
```bash
dotnet run
```

The API will be available at:
- **HTTPS**: `https://localhost:7000`
- **HTTP**: `http://localhost:5000`
- **Swagger UI**: `https://localhost:7000/swagger`
- **Scalar API Docs**: `https://localhost:7000/scalar/v1`

## 📚 API Documentation

### Authentication Endpoints
- `POST /api/auth/login` - User login
- `POST /api/auth/refresh` - Refresh JWT token
- `POST /api/auth/logout` - User logout

### Core Resource Endpoints
- `GET|POST|PUT|DELETE /api/items` - Item management
- `GET|POST|PUT|DELETE /api/users` - User management
- `GET|POST|PUT|DELETE /api/lentitems` - Lending operations
- `GET /api/summary` - Statistics and summaries

### Archive Endpoints
- `GET /api/archive/items` - Archived items
- `GET /api/archive/users` - Archived users
- `GET /api/archive/lentitems` - Archived lending records

### Utility Endpoints
- `GET /api/barcode/{id}` - Generate barcode images
- `GET /api/health` - Health check status

## 🏗️ Project Structure

```
src/
├── Authorization/          # Custom authorization handlers
├── Classes/               # Entity models
├── Controllers/           # API controllers
├── Data/                 # Database context and factory
├── DTOs/                 # Data transfer objects
├── Exceptions/           # Custom exceptions
├── Extensions/           # Service and model extensions
├── IRepository/          # Repository interfaces
├── IService/            # Service interfaces
├── Middleware/          # Custom middleware
├── Profiles/            # AutoMapper profiles
├── Repository/          # Data access layer
├── Services/            # Business logic layer
└── Utils/               # Utility classes
```

## 🔐 User Roles & Permissions

- **SuperAdmin**: Full system access, bypasses most restrictions
- **Admin**: Manage users, items, and lending operations
- **Staff**: Handle lending operations and item management
- **Student**: View personal lending history
- **Teacher**: Enhanced student permissions with additional access

## 🗄️ Database Schema

The system uses Entity Framework Code-First approach with the following main entities:

- **Users**: Base user information with role-based inheritance
- **Items**: Technical assets with barcode and image support
- **LentItems**: Active lending records
- **Archive Tables**: Historical data preservation
- **RefreshTokens**: JWT refresh token management

## 🔧 Configuration

### CORS Settings
Configure allowed origins in `appsettings.json`:
```json
{
  "AllowedOrigins": [
    "https://localhost:5173",
    "http://localhost:3000"
  ]
}
```

### Health Checks
Health check endpoint provides database connectivity status and can be extended for additional monitoring.

## 🧪 Testing

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

## 📦 Deployment

### Production Configuration
1. Update connection strings for production database
2. Set secure JWT secrets
3. Configure CORS for production domains
4. Enable HTTPS redirection
5. Set appropriate logging levels

### Docker Support
The application can be containerized using standard .NET Docker practices.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For support and questions:
- Check the API documentation at `/swagger` or `/scalar/v1`
- Review the health check endpoint at `/api/health`
- Examine logs for detailed error information