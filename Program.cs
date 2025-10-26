using AutoMapper;
using BackendTechnicalAssetsManagement.src.Authorization;
using BackendTechnicalAssetsManagement.src.BackgroundServices;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.Extensions;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Middleware;
using BackendTechnicalAssetsManagement.src.Repository;
using BackendTechnicalAssetsManagement.src.Services;
using DotNetEnv;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using static BackendTechnicalAssetsManagement.src.Authorization.ViewProfileRequirement;

Env.Load(); // Load .env file


var builder = WebApplication.CreateBuilder(args);
//.env 
builder.Configuration
       .AddEnvironmentVariables();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 5 * 1024 * 1024; // 5 MB
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
// Add Authentication and Authorization
builder.Services.AddSingleton<IAuthorizationHandler, SuperAdminBypassHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ViewProfileHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOrStaff", policy =>
        policy.RequireRole("Admin", "Staff"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
#region SwaggerGen
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Backend Technical Assets Management API",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

});
#endregion


// Manual AutoMapper Registration
//builder.Services.AddAutoMapper(cfg => {
//    cfg.AddProfile<ItemMappingProfile>();
//    cfg.AddProfile<UserMappingProfile>();
//});
builder.Services.AddAutoMapper(typeof(Program).Assembly);

#region DI Registrations
// Repository
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILentItemsRepository, LentItemsRepository>();
builder.Services.AddScoped<IArchiveItemRepository, ArchiveItemsRepository>();
builder.Services.AddScoped<IArchiveLentItemsRepository, ArchiveLentItemsRepository>();
builder.Services.AddScoped<IArchiveUserRepository, ArchiveUserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILentItemsService, LentItemsService>();
builder.Services.AddScoped<IArchiveItemsService, ArchiveItemsService>();
builder.Services.AddScoped<IArchiveLentItemsService, ArchiveLentItemsService>();
builder.Services.AddScoped<IArchiveUserService, ArchiveUserService>();
builder.Services.AddScoped<ISummaryService, SummaryService>();
builder.Services.AddScoped<IUserValidationService, UserValidationService>();

builder.Services.AddSingleton<IDevelopmentLoggerService, DevelopmentLoggerService>();
#endregion

// Background Services
builder.Services.AddHostedService<RefreshTokenCleanupService>();


//Singleton Services
builder.Services.AddSingleton<IPasswordHashingService, PasswordHashingService>();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Health Checks
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("The database connection string 'DefaultConnection' was not found in the configuration.");
}

builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString, name: "Database");

// Custom Extension Method Services
builder.Services.AddAuthServices(builder.Configuration);



#region Cors Policy
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
    options.AddPolicy("AllowFlutterDev", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
                new Uri(origin).Host == "localhost" || new Uri(origin).Host == "127.0.0.1")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
#endregion

var app = builder.Build();

// HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });

    app.UseSwaggerUI(options =>
    {
        // Point Swagger UI to the new JSON endpoint as well
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

    app.MapScalarApiReference(options =>
    {
        options.Title = "Backend Technical Assets Management API";
        options.Theme = ScalarTheme.DeepSpace;
    });
}
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandler>();

app.UseCors("AllowReactApp");
app.UseCors("AllowFlutterDev");

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();