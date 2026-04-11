# API Endpoint Testing Script
# Tests all endpoints for 404, 400, 500 errors
# Usage: .\test-api-endpoints.ps1 -BaseUrl "http://your-api-url.com" -Token "your-jwt-token"

param(
    [Parameter(Mandatory=$false)]
    [string]$BaseUrl = "http://localhost:5278",

    [Parameter(Mandatory=$false)]
    [string]$Token = "",

    [Parameter(Mandatory=$false)]
    [switch]$SkipAuth = $false
)

# Color output functions
function Write-Success { param($msg) Write-Host $msg -ForegroundColor Green }
function Write-Failure { param($msg) Write-Host $msg -ForegroundColor Red }
function Write-Info    { param($msg) Write-Host $msg -ForegroundColor Cyan }
function Write-Warn    { param($msg) Write-Host $msg -ForegroundColor Yellow }

# Test results tracking
$script:TotalTests  = 0
$script:PassedTests = 0
$script:FailedTests = 0
$script:SkippedTests = 0
$script:Results = @()

function Test-Endpoint {
    param(
        [string]$Method,
        [string]$Endpoint,
        [string]$Description,
        [bool]$RequiresAuth = $true,
        [object]$Body = $null,
        [hashtable]$Headers = @{},
        [int[]]$ExpectedStatusCodes = @(200, 201, 204),
        [string]$ContentType = "application/json"
    )

    $script:TotalTests++
    $url = "$BaseUrl$Endpoint"

    Write-Info "`n[$script:TotalTests] Testing: $Method $Endpoint"
    Write-Host "    Description: $Description" -ForegroundColor Gray

    if ($RequiresAuth -and [string]::IsNullOrEmpty($Token) -and -not $SkipAuth) {
        Write-Warn "    SKIPPED - Requires authentication token"
        $script:SkippedTests++
        $script:Results += [PSCustomObject]@{
            Method = $Method; Endpoint = $Endpoint
            Status = "SKIPPED"; StatusCode = "N/A"; Message = "Requires authentication"
        }
        return
    }

    try {
        $requestHeaders = $Headers.Clone()
        if ($RequiresAuth -and -not [string]::IsNullOrEmpty($Token)) {
            $requestHeaders["Authorization"] = "Bearer $Token"
        }

        $params = @{
            Uri         = $url
            Method      = $Method
            Headers     = $requestHeaders
            ContentType = $ContentType
            TimeoutSec  = 30
        }

        if ($null -ne $Body) {
            if ($ContentType -eq "application/json") {
                $params["Body"] = ($Body | ConvertTo-Json -Depth 10)
            } else {
                $params["Body"] = $Body
            }
        }

        $response = Invoke-WebRequest @params -ErrorAction Stop

        if ($ExpectedStatusCodes -contains $response.StatusCode) {
            Write-Success "    PASSED - Status: $($response.StatusCode)"
            $script:PassedTests++
            $script:Results += [PSCustomObject]@{
                Method = $Method; Endpoint = $Endpoint
                Status = "PASSED"; StatusCode = $response.StatusCode; Message = "Success"
            }
        } else {
            Write-Failure "    FAILED - Unexpected status: $($response.StatusCode)"
            $script:FailedTests++
            $script:Results += [PSCustomObject]@{
                Method = $Method; Endpoint = $Endpoint
                Status = "FAILED"; StatusCode = $response.StatusCode; Message = "Unexpected status code"
            }
        }
    } catch {
        $statusCode   = $_.Exception.Response.StatusCode.value__
        $errorMessage = $_.Exception.Message

        if ($ExpectedStatusCodes -contains $statusCode) {
            Write-Success "    PASSED - Expected status: $statusCode"
            $script:PassedTests++
            $script:Results += [PSCustomObject]@{
                Method = $Method; Endpoint = $Endpoint
                Status = "PASSED"; StatusCode = $statusCode; Message = "Expected error response"
            }
        } elseif ($statusCode -eq 404) {
            Write-Failure "    FAILED - 404 Not Found"
            $script:FailedTests++
            $script:Results += [PSCustomObject]@{
                Method = $Method; Endpoint = $Endpoint
                Status = "FAILED"; StatusCode = 404; Message = "Endpoint not found"
            }
        } elseif ($statusCode -ge 500) {
            Write-Failure "    FAILED - $statusCode Server Error"
            $script:FailedTests++
            $script:Results += [PSCustomObject]@{
                Method = $Method; Endpoint = $Endpoint
                Status = "FAILED"; StatusCode = $statusCode; Message = "Server error"
            }
        } elseif ($statusCode -eq 400) {
            Write-Warn "    WARNING - 400 Bad Request (may be expected)"
            $script:PassedTests++
            $script:Results += [PSCustomObject]@{
                Method = $Method; Endpoint = $Endpoint
                Status = "WARNING"; StatusCode = 400; Message = "Bad request (may be expected)"
            }
        } elseif ($statusCode -eq 401) {
            Write-Warn "    WARNING - 401 Unauthorized (expected without valid token)"
            $script:PassedTests++
            $script:Results += [PSCustomObject]@{
                Method = $Method; Endpoint = $Endpoint
                Status = "WARNING"; StatusCode = 401; Message = "Unauthorized (expected)"
            }
        } elseif ($statusCode -eq 403) {
            Write-Warn "    WARNING - 403 Forbidden (expected without proper role)"
            $script:PassedTests++
            $script:Results += [PSCustomObject]@{
                Method = $Method; Endpoint = $Endpoint
                Status = "WARNING"; StatusCode = 403; Message = "Forbidden (expected)"
            }
        } else {
            Write-Failure "    FAILED - Error: $errorMessage"
            $script:FailedTests++
            $script:Results += [PSCustomObject]@{
                Method = $Method; Endpoint = $Endpoint
                Status = "FAILED"; StatusCode = $statusCode; Message = $errorMessage
            }
        }
    }
}

Write-Host "`n========================================" -ForegroundColor Magenta
Write-Host "API ENDPOINT TESTING" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta
Write-Host "Base URL: $BaseUrl"
Write-Host "Auth Token: $(if ($Token) { 'Provided' } else { 'Not provided' })"
Write-Host "========================================`n" -ForegroundColor Magenta

# Root / Health
Test-Endpoint -Method "GET" -Endpoint "/" -Description "Root endpoint" -RequiresAuth $false
Test-Endpoint -Method "GET" -Endpoint "/health" -Description "Health check" -RequiresAuth $false
Test-Endpoint -Method "GET" -Endpoint "/api/v1/health" -Description "Detailed health check" -RequiresAuth $false

# Auth
Write-Host "`n========== AUTH ENDPOINTS ==========" -ForegroundColor Yellow
Test-Endpoint -Method "POST" -Endpoint "/api/v1/auth/login" -Description "Login" -RequiresAuth $false -ExpectedStatusCodes @(200,400,401) -Body @{ identifier = "test@test.com"; password = "test" }
Test-Endpoint -Method "POST" -Endpoint "/api/v1/auth/login-mobile" -Description "Mobile login" -RequiresAuth $false -ExpectedStatusCodes @(200,400,401) -Body @{ identifier = "test@test.com"; password = "test" }
Test-Endpoint -Method "POST" -Endpoint "/api/v1/auth/register" -Description "Register user" -RequiresAuth $true -ExpectedStatusCodes @(201,400,401,403)
Test-Endpoint -Method "GET"  -Endpoint "/api/v1/auth/me" -Description "Get current user" -RequiresAuth $true -ExpectedStatusCodes @(200,401)
Test-Endpoint -Method "POST" -Endpoint "/api/v1/auth/logout" -Description "Logout" -RequiresAuth $true -ExpectedStatusCodes @(200,401)
Test-Endpoint -Method "POST" -Endpoint "/api/v1/auth/refresh-token" -Description "Refresh token" -RequiresAuth $false -ExpectedStatusCodes @(200,400,401)
Test-Endpoint -Method "POST" -Endpoint "/api/v1/auth/refresh-token-mobile" -Description "Mobile refresh token" -RequiresAuth $false -ExpectedStatusCodes @(200,400,401)

# Users
Write-Host "`n========== USER ENDPOINTS ==========" -ForegroundColor Yellow
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/users" -Description "Get all users" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403)
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/users/00000000-0000-0000-0000-000000000000" -Description "Get user by ID" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403,404)
Test-Endpoint -Method "PATCH"  -Endpoint "/api/v1/users/students/profile/00000000-0000-0000-0000-000000000000" -Description "Update student profile" -RequiresAuth $true -ExpectedStatusCodes @(200,400,401,403,404)
Test-Endpoint -Method "PATCH"  -Endpoint "/api/v1/users/teachers/profile/00000000-0000-0000-0000-000000000000" -Description "Update teacher profile" -RequiresAuth $true -ExpectedStatusCodes @(200,400,401,403,404)
Test-Endpoint -Method "PATCH"  -Endpoint "/api/v1/users/admin-or-staff/profile/00000000-0000-0000-0000-000000000000" -Description "Update admin/staff profile" -RequiresAuth $true -ExpectedStatusCodes @(200,204,400,401,403,404)
Test-Endpoint -Method "DELETE" -Endpoint "/api/v1/users/archive/00000000-0000-0000-0000-000000000000" -Description "Archive user" -RequiresAuth $true -ExpectedStatusCodes @(200,400,401,403,404)
Test-Endpoint -Method "POST"   -Endpoint "/api/v1/users/students/import" -Description "Import students" -RequiresAuth $true -ExpectedStatusCodes @(200,400,401,403)
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/users/students/by-id-number/TEST123" -Description "Get student by ID number" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403,404)
Test-Endpoint -Method "POST"   -Endpoint "/api/v1/users/students/00000000-0000-0000-0000-000000000000/register-rfid" -Description "Register student RFID" -RequiresAuth $false -ExpectedStatusCodes @(200,400,404,409)
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/users/students/rfid/TEST_RFID" -Description "Get student by RFID" -RequiresAuth $false -ExpectedStatusCodes @(200,404)

# Items
Write-Host "`n========== ITEM ENDPOINTS ==========" -ForegroundColor Yellow
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/items" -Description "Get all items" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403)
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/items/00000000-0000-0000-0000-000000000000" -Description "Get item by ID" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403,404)
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/items/by-serial/TEST123" -Description "Get item by serial" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403,404)
Test-Endpoint -Method "POST"   -Endpoint "/api/v1/items" -Description "Create item" -RequiresAuth $true -ExpectedStatusCodes @(201,400,401,403,409)
Test-Endpoint -Method "POST"   -Endpoint "/api/v1/items/import" -Description "Import items" -RequiresAuth $true -ExpectedStatusCodes @(200,400,401,403)
Test-Endpoint -Method "PATCH"  -Endpoint "/api/v1/items/00000000-0000-0000-0000-000000000000" -Description "Update item" -RequiresAuth $true -ExpectedStatusCodes @(200,400,401,403,404)
Test-Endpoint -Method "PATCH"  -Endpoint "/api/v1/items/rfid-scan/TEST_RFID" -Description "RFID scan" -RequiresAuth $false -ExpectedStatusCodes @(200,400,404)
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/items/rfid/TEST_RFID" -Description "Get item by RFID" -RequiresAuth $false -ExpectedStatusCodes @(200,404)
Test-Endpoint -Method "POST"   -Endpoint "/api/v1/items/00000000-0000-0000-0000-000000000000/register-rfid" -Description "Register item RFID" -RequiresAuth $false -ExpectedStatusCodes @(200,400,404,409)
Test-Endpoint -Method "POST"   -Endpoint "/api/v1/items/00000000-0000-0000-0000-000000000000/update-location" -Description "Update item location" -RequiresAuth $false -ExpectedStatusCodes @(200,400,404)
Test-Endpoint -Method "DELETE" -Endpoint "/api/v1/items/archive/00000000-0000-0000-0000-000000000000" -Description "Archive item" -RequiresAuth $true -ExpectedStatusCodes @(200,400,401,403,404)

# Lent Items
Write-Host "`n========== LENT ITEMS ENDPOINTS ==========" -ForegroundColor Yellow
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/lentItems" -Description "Get all lent items" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403)
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/lentItems/00000000-0000-0000-0000-000000000000" -Description "Get lent item by ID" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403,404)
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/lentItems/date/2025-01-01" -Description "Get lent items by date" -RequiresAuth $true -ExpectedStatusCodes @(200,400,401,403,404)
Test-Endpoint -Method "POST"   -Endpoint "/api/v1/lentItems" -Description "Create lent item" -RequiresAuth $true -ExpectedStatusCodes @(201,400,401)
Test-Endpoint -Method "POST"   -Endpoint "/api/v1/lentItems/guests" -Description "Create lent item for guest" -RequiresAuth $true -ExpectedStatusCodes @(201,400,401,403)
Test-Endpoint -Method "PATCH"  -Endpoint "/api/v1/lentItems/00000000-0000-0000-0000-000000000000" -Description "Update lent item" -RequiresAuth $true -ExpectedStatusCodes @(200,400,401,403,404)
Test-Endpoint -Method "PATCH"  -Endpoint "/api/v1/lentItems/hide/00000000-0000-0000-0000-000000000000" -Description "Hide from history" -RequiresAuth $true -ExpectedStatusCodes @(200,401,404)
Test-Endpoint -Method "DELETE" -Endpoint "/api/v1/lentItems/archive/00000000-0000-0000-0000-000000000000" -Description "Archive lent item" -RequiresAuth $true -ExpectedStatusCodes @(200,400,401,403,404)

# Summary
Write-Host "`n========== SUMMARY ENDPOINTS ==========" -ForegroundColor Yellow
Test-Endpoint -Method "GET" -Endpoint "/api/v1/summary" -Description "Get overall summary" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403)

# Archive Items
Write-Host "`n========== ARCHIVE ITEMS ENDPOINTS ==========" -ForegroundColor Yellow
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/archiveitems" -Description "Get all archived items" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403)
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/archiveitems/00000000-0000-0000-0000-000000000000" -Description "Get archived item by ID" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403,404)
Test-Endpoint -Method "DELETE" -Endpoint "/api/v1/archiveitems/restore/00000000-0000-0000-0000-000000000000" -Description "Restore archived item" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403,404)
Test-Endpoint -Method "DELETE" -Endpoint "/api/v1/archiveitems/00000000-0000-0000-0000-000000000000" -Description "Delete archived item" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403,404)

# Archive Lent Items
Write-Host "`n========== ARCHIVE LENT ITEMS ENDPOINTS ==========" -ForegroundColor Yellow
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/archivelentitems" -Description "Get all archived lent items" -RequiresAuth $true -ExpectedStatusCodes @(200,401)
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/archivelentitems/00000000-0000-0000-0000-000000000000" -Description "Get archived lent item by ID" -RequiresAuth $true -ExpectedStatusCodes @(200,401,404)
Test-Endpoint -Method "DELETE" -Endpoint "/api/v1/archivelentitems/restore/00000000-0000-0000-0000-000000000000" -Description "Restore archived lent item" -RequiresAuth $true -ExpectedStatusCodes @(200,401,404)
Test-Endpoint -Method "DELETE" -Endpoint "/api/v1/archivelentitems/00000000-0000-0000-0000-000000000000" -Description "Delete archived lent item" -RequiresAuth $true -ExpectedStatusCodes @(200,401,404)

# Archive Users
Write-Host "`n========== ARCHIVE USERS ENDPOINTS ==========" -ForegroundColor Yellow
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/archiveusers" -Description "Get all archived users" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403)
Test-Endpoint -Method "GET"    -Endpoint "/api/v1/archiveusers/00000000-0000-0000-0000-000000000000" -Description "Get archived user by ID" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403,404)
Test-Endpoint -Method "DELETE" -Endpoint "/api/v1/archiveusers/restore/00000000-0000-0000-0000-000000000000" -Description "Restore archived user" -RequiresAuth $true -ExpectedStatusCodes @(200,400,401,403)
Test-Endpoint -Method "DELETE" -Endpoint "/api/v1/archiveusers/00000000-0000-0000-0000-000000000000" -Description "Delete archived user" -RequiresAuth $true -ExpectedStatusCodes @(200,401,403,404)

# Summary
Write-Host "`n========================================" -ForegroundColor Magenta
Write-Host "TEST SUMMARY" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta
Write-Host "Total Tests:   $script:TotalTests"
Write-Success "Passed:        $script:PassedTests"
Write-Failure "Failed:        $script:FailedTests"
Write-Warn    "Skipped:       $script:SkippedTests"
Write-Host "========================================`n" -ForegroundColor Magenta

if ($script:FailedTests -gt 0) {
    Write-Host "`n========== FAILED TESTS ==========" -ForegroundColor Red
    $script:Results | Where-Object { $_.Status -eq "FAILED" } | Format-Table -AutoSize
}

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$csvPath = "api-test-results_$timestamp.csv"
$script:Results | Export-Csv -Path $csvPath -NoTypeInformation
Write-Info "`nResults exported to: $csvPath"

if ($script:FailedTests -gt 0) { exit 1 } else { exit 0 }
