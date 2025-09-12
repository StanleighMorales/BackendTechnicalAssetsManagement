using AutoMapper;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.Extensions;
using BackendTechnicalAssetsManagement.src.Interfaces.IRepository;
using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Profiles; // Assuming you have this
using BackendTechnicalAssetsManagement.src.Repository;
using BackendTechnicalAssetsManagement.src.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{   //TODO move this thing 
    serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();   

// Manual AutoMapper Registration
builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<ItemMappingProfile>();
    cfg.AddProfile<UserMappingProfile>();
});


// DI Registrations
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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


var app = builder.Build();

// HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();