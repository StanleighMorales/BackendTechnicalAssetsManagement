using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <summary>
    /// Fixes lent items barcodes by generating proper LENT-YYYYMMDD-XXX format barcodes
    /// for existing lent items that have null barcodes
    /// </summary>
    public partial class FixLentItemsBarcodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update existing lent items with null barcodes to have proper format
            migrationBuilder.Sql(@"
                DECLARE @Counter INT = 1;
                DECLARE @CurrentDate DATE = CAST(GETUTCDATE() AS DATE);
                DECLARE @DateString NVARCHAR(8) = FORMAT(@CurrentDate, 'yyyyMMdd');
                
                -- Update existing lent items with null barcodes to new date-based format
                UPDATE LentItems 
                SET Barcode = 'LENT-' + @DateString + '-' + FORMAT(@Counter, '000'),
                    @Counter = @Counter + 1
                WHERE Barcode IS NULL;
            ");

            // Update specific lent items to have barcodes based on today's date for testing
            migrationBuilder.Sql(@"
                -- Update specific lent items for testing purposes
                UPDATE LentItems 
                SET Barcode = 'LENT-20251101-001'
                WHERE Id = '16096275-55ce-4b63-9df7-b117234a53e2';
                
                UPDATE LentItems 
                SET Barcode = 'LENT-20251101-002'
                WHERE Id = '42554f4d-a6b9-4fb1-be12-4e37b7c54a5c';
                
                UPDATE LentItems 
                SET Barcode = 'LENT-20251101-003'
                WHERE Id = 'a604b8e2-26aa-45ee-b586-2e72172edee6';
                
                UPDATE LentItems 
                SET Barcode = 'LENT-20251101-004'
                WHERE Id = 'b184cf02-4ec0-4aba-9b14-184620f2044a';
                
                UPDATE LentItems 
                SET Barcode = 'LENT-20251101-005'
                WHERE Id = '233e8791-588f-4645-9e2d-22d0b5a7bfdc';
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert barcodes back to null
            migrationBuilder.Sql(@"
                UPDATE LentItems 
                SET Barcode = NULL
                WHERE Barcode LIKE 'LENT-________-___';
            ");
        }
    }
}