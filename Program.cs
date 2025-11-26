/// <summary>
/// Program.cs - Entry point for the Technical Assets Management System API
/// 
/// This file configures and initializes the ASP.NET Core web application with:
/// - JWT Authentication & Authorization
/// - Entity Framework with SQL Server
/// - Swagger/OpenAPI documentation
/// - CORS policies for frontend integration
/// - Dependency injection container setup
/// - Health checks and monitoring
/// - Background services for maintenance tasks
/// </summary>

#region Using Statements
// Core ASP.NET and .NET libraries
using AutoMapper;
using DotNetEnv;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

// Application-specific namespaces
using BackendTechnicalAssetsManagement.src.Authorization;
using BackendTechnicalAssetsManagement.src.BackgroundServices;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.Extensions;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Middleware;
using BackendTechnicalAssetsManagement.src.Repository;
using BackendTechnicalAssetsManagement.src.Services;
using static BackendTechnicalAssetsManagement.src.Authorization.ViewProfileRequirement;
#endregion

#region Environment Configuration
// Load environment variables from .env file for local development
Env.Load();
#endregion

#region Application Builder Setup
var builder = WebApplication.CreateBuilder(args);

// Add environment variables to configuration pipeline
builder.Configuration.AddEnvironmentVariables();

// Configure Kestrel server options
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // Set maximum request body size to 5MB for file uploads (images, Excel files)
    serverOptions.Limits.MaxRequestBodySize = 5 * 1024 * 1024;
});

// Configure port for Railway (only in production)
if (!builder.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.UseUrls($"http://*:{port}");
}
#endregion

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 5 * 1024 * 1024; // 5 MB
});

#region Core Services Configuration
/// <summary>
/// Configure MVC controllers with custom JSON serialization options
/// </summary>
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Convert enums to strings in JSON responses for better readability
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        
        // Allow case-insensitive property matching for incoming JSON
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        
        // Handle circular references in object graphs (important for EF navigation properties)
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Configure form options for file uploads (Excel imports)
builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 5 * 1024 * 1024; // 5 MB
    options.ValueLengthLimit = 5 * 1024 * 1024; // 5 MB
    options.MemoryBufferThreshold = 5 * 1024 * 1024; // 5 MB
});

// Enable API Explorer for Swagger/OpenAPI generation
builder.Services.AddEndpointsApiExplorer();

// Add HTTP context accessor for accessing request context in services
builder.Services.AddHttpContextAccessor();

// Configure Forwarded Headers for Railway Load Balancer
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});
#endregion

#region Authentication & Authorization
/// <summary>
/// Configure JWT-based authentication and role-based authorization
/// </summary>
// Register custom authorization handlers
builder.Services.AddSingleton<IAuthorizationHandler, SuperAdminBypassHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ViewProfileHandler>();

// Define authorization policies
builder.Services.AddAuthorization(options =>
{
    // Policy for operations requiring Admin, SuperAdmin, or Staff roles
    options.AddPolicy("AdminOrStaff", policy =>
        policy.RequireRole("Admin", "SuperAdmin", "Staff"));
});
#endregion
#region API Documentation (Swagger/OpenAPI)
/// <summary>
/// Configure Swagger/OpenAPI documentation with JWT authentication support
/// </summary>
builder.Services.AddSwaggerGen(options =>
{
    // Basic API information
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Backend Technical Assets Management API",
        Version = "v1",
        Description = "RESTful API for managing technical assets, lending operations, and user management"
    });
    
    // Configure JWT Bearer token authentication in Swagger UI
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT token in the format: Bearer {your token}",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    
    // Apply JWT authentication requirement to all endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
#endregion


#region AutoMapper Configuration
/// <summary>
/// Configure AutoMapper for object-to-object mapping between entities and DTOs
/// Automatically discovers and registers all mapping profiles in the assembly
/// </summary>
builder.Services.AddAutoMapper(typeof(Program).Assembly);
#endregion

#region Dependency Injection Registration
/// <summary>
/// Register all application services and repositories with the DI container
/// Using appropriate lifetimes: Scoped for per-request, Singleton for stateless services
/// </summary>

// Repository Layer - Data Access (Scoped: new instance per HTTP request)
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILentItemsRepository, LentItemsRepository>();
builder.Services.AddScoped<IArchiveItemRepository, ArchiveItemsRepository>();
builder.Services.AddScoped<IArchiveLentItemsRepository, ArchiveLentItemsRepository>();
builder.Services.AddScoped<IArchiveUserRepository, ArchiveUserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

// Business Logic Services (Scoped: new instance per HTTP request)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILentItemsService, LentItemsService>();
builder.Services.AddScoped<IArchiveItemsService, ArchiveItemsService>();
builder.Services.AddScoped<IArchiveLentItemsService, ArchiveLentItemsService>();
builder.Services.AddScoped<IArchiveUserService, ArchiveUserService>();
builder.Services.AddScoped<ISummaryService, SummaryService>();
builder.Services.AddScoped<IUserValidationService, UserValidationService>();
builder.Services.AddScoped<IBarcodeGeneratorService, BarcodeGeneratorService>();
builder.Services.AddScoped<IExcelReaderService, ExcelReaderService>();

// Utility Services (Singleton: single instance for application lifetime)
builder.Services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddSingleton<IDevelopmentLoggerService, DevelopmentLoggerService>();
#endregion

#region Background Services
/// <summary>
/// Register hosted background services for automated maintenance tasks
/// </summary>
// Cleanup expired refresh tokens periodically
builder.Services.AddHostedService<RefreshTokenCleanupService>();

// Cancel expired reservations periodically
builder.Services.AddHostedService<ReservationExpiryBackgroundService>();
#endregion

#region Database Configuration
/// <summary>
/// Configure Entity Framework DbContext with environment-based provider selection
/// </summary>
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Choose DB provider based on environment
if (builder.Environment.IsDevelopment())
{
    // Local development → SQL Server
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    // Production → Railway Postgres
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));
}
#endregion

#region Health Checks
/// <summary>
/// Configure health checks for monitoring application and database status
/// </summary>
// Validate that connection string is configured
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("The database connection string 'DefaultConnection' was not found in the configuration.");
}

// Add health checks for database connectivity
var healthChecks = builder.Services.AddHealthChecks();
if (builder.Environment.IsDevelopment())
{
    healthChecks.AddSqlServer(connectionString, name: "SQL Server");
}
else
{
    // healthChecks.AddNpgSql(connectionString, name: "PostgreSQL");
}
#endregion

#region Custom Extension Services
/// <summary>
/// Register authentication services using custom extension method
/// Includes JWT configuration, token validation, and authentication middleware setup
/// </summary>
builder.Services.AddAuthServices(builder.Configuration);
#endregion



#region CORS Configuration
/// <summary>
/// Configure Cross-Origin Resource Sharing (CORS) policies for frontend applications
/// Unified policy supports production origins, localhost, and mobile emulators (10.0.2.2)
/// </summary>
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    // Unified policy for all frontends (React, Flutter, mobile)
    options.AddPolicy("AllowFrontends", policy =>
    {
        // Dynamic origin validation for development and production
        policy.SetIsOriginAllowed(origin =>
        {
            var uri = new Uri(origin);
            
            // Allow localhost and 127.0.0.1 on any port (development)
            if (uri.Host == "localhost" || uri.Host == "127.0.0.1")
                return true;
            
            // Allow Android emulator (10.0.2.2)
            if (origin.StartsWith("http://10.0.2.2") || origin.StartsWith("https://10.0.2.2"))
                return true;
            
            // Allow configured production origins
            if (allowedOrigins != null && allowedOrigins.Contains(origin))
                return true;
            
            return false;
        })
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials(); // Required for JWT cookies/auth headers
    });
});
#endregion

#region Application Pipeline Configuration
/// <summary>
/// Build the application and configure the HTTP request pipeline
/// Order of middleware is critical for proper functionality
/// </summary>
var app = builder.Build();

#region Development Environment Configuration
/// <summary>
/// Configure development-only features: Swagger UI and Scalar API documentation
/// </summary>
if (app.Environment.IsDevelopment())
{
    // Configure Swagger with custom route template
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });

    // Configure Swagger UI
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.RoutePrefix = "swagger"; // Available at /swagger
    });

    // Configure Scalar API documentation (modern alternative to Swagger UI)
    app.MapScalarApiReference(options =>
    {
        options.Title = "Backend Technical Assets Management API";
        options.Theme = ScalarTheme.DeepSpace; // Dark theme for better developer experience
    });
}
#endregion

#region Security & Error Handling Middleware
/// <summary>
/// Configure security and error handling middleware (order matters!)
/// </summary>
// Use Forwarded Headers Middleware (Must be before HttpsRedirection if we want to trust the proto)
app.UseForwardedHeaders();

// Redirect HTTP to HTTPS for security
app.UseHttpsRedirection();

// Global exception handling middleware (catches all unhandled exceptions)
app.UseMiddleware<GlobalExceptionHandler>();
#endregion

#region CORS Middleware
/// <summary>
/// Apply CORS policy BEFORE authentication to allow preflight requests
/// Single policy handles React, Flutter, mobile emulators, and production origins
/// </summary>
app.UseCors("AllowFrontends");
#endregion

#region Static Files & Authentication
/// <summary>
/// Configure static file serving and authentication/authorization pipeline
/// </summary>
// Serve static files (images, documents, etc.)
app.UseStaticFiles();

// Authentication middleware (validates JWT tokens)
app.UseAuthentication();

// Authorization middleware (enforces role-based access control)
app.UseAuthorization();
#endregion

#region Endpoint Mapping
/// <summary>
/// Map controller endpoints to handle HTTP requests
/// </summary>
app.MapControllers();

// Map Health Check Endpoint
app.MapHealthChecks("/health");

// Root endpoint for deployment verification
app.MapGet("/", () => Results.Json(new
{
    service = "Backend Technical Assets Management API",
    status = "running",
    version = "v1",
    documentation = app.Environment.IsDevelopment() ? "/swagger" : "Contact administrator",
    health = "/health",
    api = "/api/v1"
}));
#endregion

#endregion

#region Application Startup
/// <summary>
/// Start the application and begin listening for HTTP requests
/// </summary>

// Add super admin if none exists (for production deployment)
// await SuperAdminSeeder.AddSuperAdminIfNeeded(app.Services);

app.Run();
#endregion