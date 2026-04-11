@echo off
REM Quick API Test Runner for Windows
REM Double-click this file to run tests

echo ========================================
echo API Testing Suite
echo ========================================
echo.

REM Check if PowerShell is available
where powershell >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: PowerShell is not installed or not in PATH
    pause
    exit /b 1
)

REM Run the quick test script
powershell -ExecutionPolicy Bypass -File "%~dp0quick-test.ps1"

echo.
echo ========================================
echo Tests completed!
echo ========================================
echo.
pause
