# Quick API Test Script
# This script helps you quickly test your API

param(
    [Parameter(Mandatory=$false)]
    [string]$Environment = "local"
)

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Quick API Test" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Set base URL based on environment
$baseUrl = switch ($Environment) {
    "local" { "http://localhost:5278" }
    "production" { Read-Host "Enter production URL" }
    default { "https://localhost:7001" }
}

Write-Host "Testing environment: $Environment" -ForegroundColor Yellow
Write-Host "Base URL: $baseUrl" -ForegroundColor Yellow
Write-Host ""

# Ask if user wants to login
$login = Read-Host "Do you want to login to test authenticated endpoints? (y/n)"

$token = ""
if ($login -eq "y" -or $login -eq "Y") {
    $identifier = Read-Host "Enter email or username"
    $password = Read-Host "Enter password" -AsSecureString
    $passwordPlain = [Runtime.InteropServices.Marshal]::PtrToStringAuto(
        [Runtime.InteropServices.Marshal]::SecureStringToBSTR($password)
    )
    
    Write-Host ""
    Write-Host "Attempting login..." -ForegroundColor Cyan

    # Bypass SSL certificate validation for local dev (works on all PS versions)
    $certBypassCode = @'
using System.Net;
using System.Security.Cryptography.X509Certificates;
public class TrustAllCerts : ICertificatePolicy {
    public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem) { return true; }
}
'@
    if (-not ([System.Management.Automation.PSTypeName]'TrustAllCerts').Type) {
        Add-Type -TypeDefinition $certBypassCode
    }
    [System.Net.ServicePointManager]::CertificatePolicy = New-Object TrustAllCerts
    [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12

    try {
        $loginBody = @{
            identifier = $identifier
            password = $passwordPlain
        } | ConvertTo-Json

        # Use login-mobile which returns token in JSON body (web login uses HttpOnly cookies)
        $loginResponse = Invoke-RestMethod -Uri "$baseUrl/api/v1/auth/login-mobile" `
            -Method POST `
            -ContentType "application/json" `
            -Body $loginBody

        if ($loginResponse.success) {
            Write-Host "Login successful!" -ForegroundColor Green
            $token = $loginResponse.data.accessToken
            if ($token) {
                Write-Host "Token obtained: $($token.Substring(0, [Math]::Min(20, $token.Length)))..." -ForegroundColor Gray
            } else {
                Write-Host "Warning: No token in response." -ForegroundColor Yellow
            }
        } else {
            Write-Host "Login failed: $($loginResponse.message)" -ForegroundColor Red
        }
    }
    catch {
        Write-Host "Login error: $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Running API Tests..." -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Run the main test script
if ([string]::IsNullOrEmpty($token)) {
    & "$PSScriptRoot\test-api-endpoints.ps1" -BaseUrl $baseUrl
} else {
    & "$PSScriptRoot\test-api-endpoints.ps1" -BaseUrl $baseUrl -Token $token
}
