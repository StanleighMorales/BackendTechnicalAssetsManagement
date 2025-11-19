@echo off
echo Cleaning Build Artifacts...
dotnet clean BackendTechnicalAssetsManagement.csproj
cd ..
dotnet clean BackendTechnicalAssetsManagementTest/BackendTechnicalAssetsManagementTest.csproj
cd BackendTechnicalAssetsManagement
echo Clean complete!
