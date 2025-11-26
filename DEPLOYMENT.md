# Deployment Guide - Railway

This guide outlines the steps to deploy the **Backend Technical Assets Management** application to [Railway](https://railway.app/).

## Prerequisites
- A [Railway](https://railway.app/) account.
- [GitHub CLI](https://cli.github.com/) or Git installed.
- The project pushed to a GitHub repository.

## Configuration Changes
The following changes have been made to support Railway:
1.  **Dockerfile**: Added to build the application in a container.
2.  **Program.cs**:
    - Configured to listen on the `PORT` environment variable.
    - Added `ForwardedHeaders` middleware for correct HTTPS handling.
    - Added `/health` endpoint for health checks.

## Deployment Steps

### 1. Create a New Project on Railway
1.  Log in to Railway.
2.  Click **New Project** > **Deploy from GitHub repo**.
3.  Select your repository.
4.  Click **Deploy Now**.

### 2. Configure Environment Variables
Once the project is created, go to the **Variables** tab and add the following:

| Variable | Description | Example Value |
| :--- | :--- | :--- |
| `DefaultConnection` | SQL Server Connection String | `Server=tcp:your-server.database.windows.net...` |
| `JWT_SECRET` | Secret key for JWT tokens | `YourSuperSecretKeyHere...` |
| `JWT_ISSUER` | Token Issuer | `YourApp` |
| `JWT_AUDIENCE` | Token Audience | `YourApp` |
| `AllowedOrigins` | Frontend URLs (JSON array) | `["https://your-frontend.vercel.app"]` |

> **Note:** For `AllowedOrigins`, Railway might require escaping quotes if entered in the raw editor, but usually, the UI handles it. If you have issues, try a simple comma-separated string and update `Program.cs` to parse it, or ensure the JSON format is correct.

### 3. Configure Health Check
Railway automatically detects the exposed port, but you should configure the health check path to ensure zero-downtime deployments.
1.  Go to **Settings** > **Deployments**.
2.  Set **Healthcheck Path** to `/health`.
3.  Set **Healthcheck Timeout** to `300` (seconds) if your app takes time to start, though defaults are usually fine.

### 4. Verify Deployment
- Check the **Deployments** tab to see the build logs.
- Once active, click the generated URL.
- Visit `/health` to verify the app is running (should return `Healthy`).
- Visit `/swagger` to see the API documentation.

## Troubleshooting
- **App Crashes after ~7 mins**: This is often due to the app not binding to the correct port or failing health checks. We've fixed this by explicitly using the `PORT` var and adding the `/health` endpoint.
- **Database Connection Errors**: Ensure your SQL Server allows connections from Railway's IP addresses (or allow all IPs if using Azure SQL/AWS RDS with secure credentials).
