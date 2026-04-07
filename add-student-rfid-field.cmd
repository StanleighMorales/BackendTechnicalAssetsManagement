@echo off
echo Adding RfidUid column to Students table...

sqlcmd -S "(localdb)\MSSQLLocalDB" -d ejDB -E -Q "IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Students') AND name = 'RfidUid') ALTER TABLE Students ADD RfidUid NVARCHAR(MAX) NULL;"
IF %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to add RfidUid column.
    exit /b 1
)
echo RfidUid column added (or already exists).

echo Done.
