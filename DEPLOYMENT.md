# Deployment Guide - Railway

This guide outlines the steps to deploy the **Backend Technical Assets Management** application to [Railway](https://railway.app/).

## Prerequisites
- A [Railway](https://railway.app/) account.
- [GitHub CLI](https://cli.github.com/) or Git installed.
- The project pushed to a GitHub repository.

## Database Architecture

### Hybrid Database Approach
The application uses **environment-based database provider selection**:

- **Development (Local)**: SQL Server / SQL Server LocalDB
- **Production (Railway)**: PostgreSQL

This is automatically handled in `Program.cs`:
```csharp
if (builder.Environment.IsDevelopment())
{
    // Local development → SQL Server
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    // Production → Railway Postgres
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));
}
```

### Required Packages
Both database providers are included in the project:
- `Microsoft.EntityFrameworkCore.SqlServer` (v8.0.20) - For local development
- `Npgsql.EntityFrameworkCore.PostgreSQL` (v8.0.0) - For Railway production

## Configuration Changes
The following changes have been made to support Railway:
1.  **Dockerfile**: Added to build the application in a container.
2.  **Program.cs**:
    - Configured to listen on the `PORT` environment variable.
    - Added `ForwardedHeaders` middleware for correct HTTPS handling.
    - Added `/health` endpoint for health checks.
    - **Hybrid database provider selection** based on environment.
3.  **CORS Configuration**: Unified policy supporting production origins, localhost, and mobile emulators.

## Deployment Steps

### 1. Create a New Project on Railway
1.  Log in to Railway.
2.  Click **New Project** > **Deploy from GitHub repo**.
3.  Select your repository.
4.  Click **Deploy Now**.

### 2. Add PostgreSQL Database
Railway makes it easy to add a PostgreSQL database:
1.  In your Railway project, click **New** > **Database** > **Add PostgreSQL**.
2.  Railway will automatically create a PostgreSQL instance and generate connection variables.
3.  The database will be linked to your project.

### 3. Configure Environment Variables
Once the project is created, go to the **Variables** tab and add/verify the following:

#### Required Variables

| Variable | Description | Example Value |
| :--- | :--- | :--- |
| `ASPNETCORE_ENVIRONMENT` | Environment name | `Production` |
| `ConnectionStrings__DefaultConnection` | PostgreSQL Connection String | `Host=postgres.railway.internal;Port=5432;Database=railway;Username=postgres;Password=***;SSL Mode=Require;Trust Server Certificate=true` |
| `AppSettings__Token` | Secret key for JWT tokens | `YourSuperSecretKeyHere...` (min 32 characters) |
| `AllowedOrigins__0` | Frontend URL (first) | `https://your-frontend.vercel.app` |
| `AllowedOrigins__1` | Frontend URL (second, optional) | `https://your-frontend.pages.dev` |

#### PostgreSQL Connection String Format

Railway provides several PostgreSQL connection variables. You can either:

**Option A: Use Railway's DATABASE_URL (Recommended)**
Railway automatically sets `DATABASE_URL`. You can reference it:
```
ConnectionStrings__DefaultConnection=${{Postgres.DATABASE_URL}}
```

**Option B: Construct manually from Railway variables**
```
ConnectionStrings__DefaultConnection=Host=${{Postgres.PGHOST}};Port=${{Postgres.PGPORT}};Database=${{Postgres.PGDATABASE}};Username=${{Postgres.PGUSER}};Password=${{Postgres.PGPASSWORD}};SSL Mode=Require;Trust Server Certificate=true
```

**Option C: Use internal Railway hostname (faster)**
```
ConnectionStrings__DefaultConnection=Host=postgres.railway.internal;Port=5432;Database=railway;Username=postgres;Password=${{Postgres.PGPASSWORD}};SSL Mode=Require;Trust Server Certificate=true
```

> **Important Notes:**
> - PostgreSQL connection strings use `Host=` (not `Server=`)
> - Use `SSL Mode=Require` for Railway PostgreSQL
> - Railway's internal network (`postgres.railway.internal`) is faster than public URLs
> - The `ASPNETCORE_ENVIRONMENT=Production` variable is **critical** - it tells the app to use PostgreSQL instead of SQL Server

#### CORS Configuration

For `AllowedOrigins`, Railway uses indexed environment variables:
```
AllowedOrigins__0=https://your-frontend.vercel.app
AllowedOrigins__1=https://your-frontend.pages.dev
AllowedOrigins__2=https://your-custom-domain.com
```

This automatically maps to a JSON array in .NET configuration.

### 4. Run Database Migrations

After deploying, you need to apply Entity Framework migrations to create the database schema.

**Option A: Using Railway CLI (Recommended)**
```bash
# Install Railway CLI
npm i -g @railway/cli

# Login to Railway
railway login

# Link to your project
railway link

# Run migrations
railway run dotnet ef database update
```

**Option B: Using a one-time deployment script**
Add a migration command to your Dockerfile or create a startup script that runs migrations automatically.

**Option C: Connect to Railway PostgreSQL locally**
```bash
# Get connection string from Railway dashboard
# Run migrations from your local machine
dotnet ef database update --connection "Host=...;Port=...;Database=railway;..."
```

### 5. Configure Health Check
Railway automatically detects the exposed port, but you should configure the health check path to ensure zero-downtime deployments.
1.  Go to **Settings** > **Deployments**.
2.  Set **Healthcheck Path** to `/health`.
3.  Set **Healthcheck Timeout** to `300` (seconds) if your app takes time to start, though defaults are usually fine.

### 6. Verify Deployment
- Check the **Deployments** tab to see the build logs.
- Once active, click the generated URL.
- Visit `/health` to verify the app is running (should return `Healthy`).
- Visit `/swagger` to see the API documentation (if enabled in production).

## Local Development vs Production

### Local Development (SQL Server)
```bash
# .env file
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection=Server=(localdb)\\mssqllocaldb;Database=TechnicalAssetsDB;Trusted_Connection=true;MultipleActiveResultSets=true
AppSettings__Token=YourLocalDevToken

# Run migrations
dotnet ef database update

# Start the app
dotnet run
```

### Production (Railway PostgreSQL)
```bash
# Railway Environment Variables
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=Host=postgres.railway.internal;Port=5432;Database=railway;Username=postgres;Password=***;SSL Mode=Require;Trust Server Certificate=true
AppSettings__Token=YourProductionSecureToken
AllowedOrigins__0=https://your-frontend.vercel.app

# Migrations run via Railway CLI or startup script
# App runs automatically on Railway
```

## Troubleshooting

### Database Connection Issues

**Error: "Cannot connect to PostgreSQL"**
- ✅ Verify `ASPNETCORE_ENVIRONMENT=Production` is set
- ✅ Check PostgreSQL service is running in Railway
- ✅ Verify connection string format (use `Host=`, not `Server=`)
- ✅ Ensure `SSL Mode=Require` is included
- ✅ Check Railway logs for detailed error messages

**Error: "Npgsql not found"**
- ✅ Package is already installed in `.csproj`
- ✅ Run `dotnet restore` if needed
- ✅ Verify Railway is building from the correct Dockerfile

**Error: "Database does not exist"**
- ✅ Run migrations: `railway run dotnet ef database update`
- ✅ Check PostgreSQL database name matches connection string

### Environment Detection Issues

**App uses SQL Server in production**
- ✅ Ensure `ASPNETCORE_ENVIRONMENT=Production` is set in Railway
- ✅ Check Railway deployment logs to confirm environment
- ✅ Verify no `appsettings.Production.json` overriding the environment

**App uses PostgreSQL in development**
- ✅ Ensure `ASPNETCORE_ENVIRONMENT=Development` in `.env` or `launchSettings.json`
- ✅ Check your local environment variables

### General Issues

**App Crashes after ~7 mins**
- ✅ App not binding to correct port → Fixed by using `PORT` environment variable
- ✅ Health check failing → Verify `/health` endpoint returns `Healthy`
- ✅ Database connection timeout → Check connection string and PostgreSQL service status

**CORS Errors**
- ✅ Add your frontend URL to `AllowedOrigins__0` in Railway
- ✅ Verify the URL matches exactly (including https://)
- ✅ Check browser console for specific CORS error details

**Migrations Not Applied**
- ✅ Run migrations manually via Railway CLI
- ✅ Check Railway logs for migration errors
- ✅ Verify connection string has write permissions

## Monitoring and Maintenance

### Health Check Endpoint
```bash
# Check if app is healthy
curl https://your-app.railway.app/health

# Expected response
Healthy
```

### Database Health
The health check automatically monitors:
- **Development**: SQL Server connectivity
- **Production**: PostgreSQL connectivity (when enabled)

### Logs
View logs in Railway dashboard:
1. Go to your project
2. Click on your service
3. View **Deployments** > **Logs**

### Database Backups
Railway PostgreSQL includes automatic backups:
1. Go to PostgreSQL service in Railway
2. Click **Backups** tab
3. Configure backup retention as needed

## Security Best Practices

- ✅ Use strong JWT tokens (min 32 characters)
- ✅ Never commit `.env` files (already in `.gitignore`)
- ✅ Use Railway's secret management for sensitive data
- ✅ Enable SSL for PostgreSQL connections (`SSL Mode=Require`)
- ✅ Restrict CORS to specific frontend origins
- ✅ Regularly update NuGet packages
- ✅ Monitor Railway logs for suspicious activity
- ✅ Use Railway's private networking for database connections

## Useful Railway Commands

```bash
# Install Railway CLI
npm i -g @railway/cli

# Login
railway login

# Link to project
railway link

# View logs
railway logs

# Run commands in Railway environment
railway run <command>

# Open Railway dashboard
railway open

# Deploy manually
railway up
```

## Additional Resources

- [Railway Documentation](https://docs.railway.app/)
- [Railway PostgreSQL Guide](https://docs.railway.app/databases/postgresql)
- [Entity Framework Core Migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
- [ASP.NET Core Deployment](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/)

## Summary

Your application now supports:
- ✅ **Hybrid database approach**: SQL Server (dev) + PostgreSQL (prod)
- ✅ **Automatic environment detection**: No manual switching needed
- ✅ **Railway-optimized**: Port binding, health checks, forwarded headers
- ✅ **CORS configured**: Supports production frontends and local development
- ✅ **Production-ready**: SSL, health monitoring, proper middleware order
