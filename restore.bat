@echo off
echo Restoring NuGet Packages...
dotnet restore BackendTechnicalAssetsManagement.csproj
cd ..
dotnet restore BackendTechnicalAssetsManagementTest/BackendTechnicalAssetsManagementTest.csproj
cd BackendTechnicalAssetsManagement
echo Restore complete!
