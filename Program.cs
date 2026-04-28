/// <summary>
/// Program.cs - Entry point for the Technical Assets Management System API
/// 
/// This file configures and initializes the ASP.NET Core web application with:
/// - JWT Authentication & Authorization
/// - Entity Framework with Supabase (PostgreSQL)
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
using BackendTechnicalAssetsManagement.src.Filters;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Middleware;
using BackendTechnicalAssetsManagement.src.Repository;
using BackendTechnicalAssetsManagement.src.Services;
using static BackendTechnicalAssetsManagement.src.Authorization.ViewProfileRequirement;
#endregion

#region Environment Configuration
// Load environment variables from .env file for local development
// Use explicit path resolution to handle different working directories (VS, dotnet run, Docker)
var envPath = Path.Combine(AppContext.BaseDirectory, ".env");
if (!File.Exists(envPath))
    envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
if (File.Exists(envPath))
    Env.Load(envPath);
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
    
    // Policy for all authenticated users (read access)
    options.AddPolicy("AllUsers", policy =>
        policy.RequireRole("Admin", "SuperAdmin", "Staff", "Teacher", "Student"));
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

    // Map IFormFile to file upload in Swagger
    options.MapType<IFormFile>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });

    // Add operation filter for file uploads
    options.OperationFilter<FileUploadOperationFilter>();
});
#endregion


#region AutoMapper Configuration
/// <summary>
/// Configure AutoMapper for object-to-object mapping between entities and DTOs
/// Automatically discovers and registers all mapping profiles in the assembly
/// </summary>
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
});
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
builder.Services.AddScoped<IActivityLogRepository, ActivityLogRepository>();

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
builder.Services.AddScoped<IExcelReaderService, ExcelReaderService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IActivityLogService, ActivityLogService>();


// Utility Services (Singleton: single instance for application lifetime)
builder.Services.AddSingleton<IPasswordHashingService>(sp =>
    new PasswordHashingService(sp.GetRequiredService<IConfiguration>()));
builder.Services.AddSingleton<IDevelopmentLoggerService, DevelopmentLoggerService>();
builder.Services.AddSingleton<ISupabaseStorageService, SupabaseStorageService>();

#endregion

#region Background Services
/// <summary>
/// Register hosted background services for automated maintenance tasks
/// </summary>
// Cleanup expired refresh tokens periodically
builder.Services.AddHostedService<RefreshTokenCleanupService>();

// Cancel expired reservations periodically
builder.Services.AddHostedService<ReservationExpiryBackgroundService>();

// Alert admin/staff (and borrower) when an approved reservation is due within 15 minutes
builder.Services.AddHostedService<ReservationDueSoonBackgroundService>();
#endregion

#region Database Configuration
/// <summary>
/// Configure Entity Framework DbContext with Supabase (PostgreSQL)
/// </summary>
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var connectionString = builder.Configuration.GetConnectionString("Supabase")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    // Log available configuration keys for debugging
    var allKeys = builder.Configuration.AsEnumerable()
        .Where(x => x.Key.Contains("Connection", StringComparison.OrdinalIgnoreCase))
        .Select(x => x.Key);
    
    var availableKeys = string.Join(", ", allKeys);
    throw new InvalidOperationException(
        $"Supabase connection string is not configured. " +
        $"Looking for 'ConnectionStrings:Supabase' or 'ConnectionStrings:DefaultConnection'. " +
        $"Available connection keys: {availableKeys}");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        // Automatically retry transient failures (dropped connections, network blips).
        // This handles the SocketException (10054) that occurs when Supabase/PostgreSQL
        // kills idle connections after ~15 minutes and the pool tries to reuse them.
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorCodesToAdd: null
        );

        // Keep connections alive so the server doesn't close them while idle.
        // KeepAlive sends a TCP keepalive every 30 seconds on idle connections.
        npgsqlOptions.CommandTimeout(60);
    }));
#endregion

#region Health Checks
/// <summary>
/// Configure health checks for monitoring application and database status
/// </summary>
builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString, name: "Supabase PostgreSQL");
#endregion

#region Custom Extension Services
/// <summary>
/// Register authentication services using custom extension method
/// Includes JWT configuration, token validation, and authentication middleware setup
/// </summary>
builder.Services.AddAuthServices(builder.Configuration);
#endregion

#region SignalR Configuration
/// <summary>
/// Configure SignalR for real-time notifications.
/// AddJsonProtocol with CamelCaseNamingPolicy ensures property names sent over
/// the hub match the camelCase convention used by the REST API and the frontend.
/// </summary>
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy =
            System.Text.Json.JsonNamingPolicy.CamelCase;
    });
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

#region API Documentation Configuration
/// <summary>
/// Configure API documentation: Swagger UI and Scalar
/// </summary>
// Configure Swagger (use default route)
app.UseSwagger(options =>
{
    options.RouteTemplate = "swagger/{documentName}/swagger.json";
});

// Configure Swagger UI
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend Technical Assets Management API v1");
});

// Configure Scalar API documentation
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Backend Technical Assets Management API")
        .WithTheme(ScalarTheme.Purple)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");
});
#endregion

#region Security & Error Handling Middleware
/// <summary>
/// Configure security and error handling middleware (order matters!)
/// </summary>
// Use Forwarded Headers Middleware (Must be before HttpsRedirection if we want to trust the proto)
app.UseForwardedHeaders();

// Redirect HTTP to HTTPS for security (skip in development to avoid CORS issues with Swagger)
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

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

// Map SignalR Hub
app.MapHub<BackendTechnicalAssetsManagement.src.Hubs.NotificationHub>("/notificationHub");

// Root endpoint for deployment verification
app.MapGet("/", () => Results.Json(new
{
    service = "Backend Technical Assets Management API",
    status = "running",
    version = "v1",
    documentation = app.Environment.IsDevelopment() ? "/swagger" : "Contact administrator",
    health = "/health",
    api = "/api/v1",
    signalr = "/notificationHub"
}));
#endregion

#endregion

#region Application Startup
/// <summary>
/// Start the application and begin listening for HTTP requests
/// </summary>

app.Run();
#endregion