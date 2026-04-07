@echo off
echo Adding TagUid and StudentRfid columns to LentItems table...

sqlcmd -S "(localdb)\MSSQLLocalDB" -d ejDB -E -Q "IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('LentItems') AND name = 'TagUid') ALTER TABLE LentItems ADD TagUid NVARCHAR(MAX) NULL;"
IF %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to add TagUid column.
    exit /b 1
)
echo TagUid column added (or already exists).

sqlcmd -S "(localdb)\MSSQLLocalDB" -d ejDB -E -Q "IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('LentItems') AND name = 'StudentRfid') ALTER TABLE LentItems ADD StudentRfid NVARCHAR(MAX) NULL;"
IF %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to add StudentRfid column.
    exit /b 1
)
echo StudentRfid column added (or already exists).

echo Done.
