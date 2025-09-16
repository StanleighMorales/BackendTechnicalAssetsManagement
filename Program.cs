using AutoMapper;
using System.Text.Json.Serialization;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.Extensions;
using BackendTechnicalAssetsManagement.src.Interfaces.IRepository;
using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Profiles;
using BackendTechnicalAssetsManagement.src.Repository;
using BackendTechnicalAssetsManagement.src.Services;
using Microsoft.EntityFrameworkCore;
using BackendTechnicalAssetsManagement.src.Interfaces.IValidations;
using BackendTechnicalAssetsManagement.src.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();

// Manual AutoMapper Registration
builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<ItemMappingProfile>();
    cfg.AddProfile<UserMappingProfile>();
});

#region DI Registrations
// Repository
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserValidationService, UserValidationService>();
#endregion



//Singleton Services
builder.Services.AddSingleton<IPasswordHashingService, PasswordHashingService>();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Custom Extension Method Services
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddSwaggerServices();

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
});
#endregion

var app = builder.Build();

// HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();