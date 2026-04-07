@echo off
echo Adding Location column to Items and LentItems tables...

sqlcmd -S "(localdb)\MSSQLLocalDB" -d ejDB -E -Q "IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Items') AND name = 'Location') ALTER TABLE Items ADD Location NVARCHAR(255) NULL;"
IF %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to add Location column to Items.
    exit /b 1
)
echo Items.Location column added (or already exists).

sqlcmd -S "(localdb)\MSSQLLocalDB" -d ejDB -E -Q "IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('LentItems') AND name = 'Location') ALTER TABLE LentItems ADD Location NVARCHAR(255) NULL;"
IF %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to add Location column to LentItems.
    exit /b 1
)
echo LentItems.Location column added (or already exists).

echo Done.
