FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy only the backend csproj first (for caching)
COPY BackendTechnicalAssetsManagement.csproj ./
RUN dotnet restore "./BackendTechnicalAssetsManagement.csproj"

# Copy the rest of the backend source code
COPY . ./

# Publish ONLY the backend project
RUN dotnet publish "./BackendTechnicalAssetsManagement.csproj" -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app ./

# Install dependencies for SkiaSharp and curl for health checks
# SkiaSharp requires libfontconfig1 and other graphics libraries
RUN apt-get update && apt-get install -y \
    curl \
    libfontconfig1 \
    libfreetype6 \
    libx11-6 \
    libxcb1 \
    libxext6 \
    libxrender1 \
    libharfbuzz0b \
    libgdiplus \
    && rm -rf /var/lib/apt/lists/*

# Railway uses PORT env var; ASP.NET must listen on it
ENV ASPNETCORE_URLS=http://+:${PORT}

# Create non-privileged user
ARG UID=10001
RUN adduser \
    --disabled-password \
    --gecos "" \
    --home "/nonexistent" \
    --shell "/sbin/nologin" \
    --no-create-home \
    --uid "${UID}" \
    appuser

USER appuser

# Start app
ENTRYPOINT ["dotnet", "BackendTechnicalAssetsManagement.dll"]
