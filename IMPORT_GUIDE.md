# Item Import Guide

## Overview
This guide explains how to bulk import items using an Excel file with automatic barcode generation and image support.

## Excel File Requirements

### File Format
- **Supported formats**: `.xlsx` only (no .xls or .csv)
- **MIME type validation**: `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`
- **First row**: Must contain column headers

### Supported Excel Columns

The import function supports the following columns (case-insensitive with flexible prefix matching):

| Column Name | Required | Description | Example Values | Accepted Variations |
|-------------|----------|-------------|----------------|---------------------|
| SerialNumber | ✅ Yes | Unique identifier (auto-converted to uppercase with "SN-" prefix) | "abc123" → "SN-ABC123" | SerialNumber, Serial Number, SN |
| ItemName | ✅ Yes | Name of the item | "Dell Laptop", "Canon Camera" | ItemName, Item Name, Name |
| ItemType | ✅ Yes | Type/category of item | "Laptop", "Camera", "Projector" | ItemType, Item Type, Type |
| ItemMake | ✅ Yes | Manufacturer or brand | "Dell", "Canon", "Epson" | ItemMake, Item Make, Make, Brand |
| Category | ✅ Yes | Item category (must match enum) | "Electronics", "MediaEquipment", "Tools", "Keys", "Miscellaneous" | Category |
| Condition | ✅ Yes | Item condition (must match enum) | "New", "Good", "Defective", "Refurbished", "NeedRepair" | Condition |
| ItemModel | ❌ No | Model number or name | "XPS 15", "EOS R6", "ThinkPad X1" | ItemModel, Item Model, Model |
| Description | ❌ No | Additional details about the item | "High-performance laptop for video editing" | Description, Desc |
| Image | ❌ No | File path or URL to item image | "/images/laptop1.jpg", "https://example.com/image.png" | Image, ImagePath, ImageUrl |

**Important Notes**: 
- Status is NOT included in imports - all imported items are automatically assigned `Status = Available`
- ID, Barcode, and BarcodeImage are auto-generated and should NOT be included in the Excel file

### Valid Enum Values

#### Category (Required)
Must be one of these exact values (case-insensitive):
- `Electronics` - Electronic devices and equipment
- `Keys` - Physical keys and access cards
- `MediaEquipment` - Audio/visual equipment
- `Tools` - Hand tools and equipment
- `Miscellaneous` - Other items

#### Condition (Required)
Must be one of these exact values (case-insensitive):
- `New` - Brand new item (never used)
- `Good` - Used but in good working condition
- `Defective` - Not working properly or broken
- `Refurbished` - Restored to working condition
- `NeedRepair` - Requires maintenance or repair

## Sample Excel Structure

| SerialNumber | ItemName | ItemType | ItemModel | ItemMake | Description | Category | Condition | Image |
|--------------|----------|----------|-----------|----------|-------------|----------|-----------|-------|
| 12345 | Laptop | Computer | ThinkPad X1 | Lenovo | 16GB RAM, 512GB SSD | Electronics | Good | |
| 67890 | Projector | Display | EB-X41 | Epson | 3600 lumens | MediaEquipment | New | |
| ABC123 | Screwdriver Set | Hand Tool | | Stanley | 10-piece set | Tools | Good | |
| KEY001 | Room Key | Access Key | | Master Lock | Room 101 | Keys | New | |
| lp001 | Dell XPS 15 | Laptop | XPS 15 | Dell | | Electronics | New | images/dell-xps15.jpg |
| cam002 | Canon Camera | Camera | EOS R6 | Canon | | MediaEquipment | Good | https://example.com/canon.jpg |

**Note**: The example shows mixed case to demonstrate case-insensitivity. Serial numbers are auto-converted to uppercase with "SN-" prefix (e.g., "lp001" → "SN-LP001"). All items are imported with `Status = Available`.

## What Gets Auto-Generated

For each item in the Excel file, the system automatically generates:

1. **Item ID**: Unique GUID (e.g., `3fa85f64-5717-4562-b3fc-2c963f66afa6`)
2. **Serial Number Formatting**: 
   - Converts to uppercase: `abc123` → `ABC123`
   - Adds "SN-" prefix if missing: `ABC123` → `SN-ABC123`
   - Already prefixed numbers unchanged: `SN-12345` → `SN-12345`
3. **Barcode Text**: Generated with format `ITEM-SN-{SerialNumber}`
   - Example: `ITEM-SN-SN-12345`
4. **Barcode Image**: Auto-generated barcode image stored as byte array
5. **Status**: Automatically set to `Available` for all imported items
6. **Timestamps**: 
   - `CreatedAt`: Set to current UTC time
   - `UpdatedAt`: Set to current UTC time

## Image Import Support

### Supported Image Sources
The `Image` column (also accepts `ImagePath` or `ImageUrl`) supports two formats:

1. **File Paths**: Relative or absolute paths to image files
   - Relative: `images/laptop1.jpg`
   - Absolute: `C:\Images\camera.png`
   - Server-relative: `/var/www/images/projector.jpg`

2. **URLs**: Direct HTTP/HTTPS links to images
   - Example: `https://example.com/images/item.jpg`
   - Example: `http://cdn.example.com/products/laptop.png`

### Supported Image Formats
JPG, JPEG, PNG, GIF, BMP, WEBP, SVG

### Image Loading Behavior
- If image loading fails, the item is **still imported** without the image
- No error is thrown for failed image loads
- Images are stored as binary data (byte array) in the database
- MIME type is automatically detected from file extension

## API Endpoint

**POST** `/api/v1/items/import`

**Authorization**: Admin or Staff role required

**Content-Type**: `multipart/form-data`

**Request Body**:
```
file: items.xlsx
```

## Response Format

### Success Response
```json
{
  "success": true,
  "message": "Import completed. Success: 4, Failed: 0",
  "data": {
    "totalProcessed": 4,
    "successCount": 4,
    "failureCount": 0,
    "errors": [],
    "skippedDuplicates": []
  }
}
```

### Partial Success Response
```json
{
  "success": true,
  "message": "Import completed. Success: 3, Failed: 1",
  "data": {
    "totalProcessed": 4,
    "successCount": 3,
    "failureCount": 1,
    "errors": [
      "Row 3: Missing SerialNumber"
    ],
    "skippedDuplicates": []
  }
}
```

### Duplicate Detection Response
```json
{
  "success": true,
  "message": "Import completed. Success: 2, Failed: 2",
  "data": {
    "totalProcessed": 4,
    "successCount": 2,
    "failureCount": 2,
    "errors": [],
    "skippedDuplicates": [
      "Row 2: Item with SerialNumber 'SN-12345' already exists",
      "Row 4: Item with SerialNumber 'SN-67890' already exists"
    ]
  }
}
```

### Complete Failure Response
```json
{
  "success": false,
  "message": "Import failed. No items were imported. 4 row(s) had errors.",
  "data": {
    "totalProcessed": 4,
    "successCount": 0,
    "failureCount": 4,
    "errors": [
      "Row 2: Missing SerialNumber",
      "Row 3: Missing ItemName",
      "Row 4: Invalid Category value",
      "Row 5: Invalid Condition value"
    ],
    "skippedDuplicates": []
  },
  "errors": [
    "Row 2: Missing SerialNumber",
    "Row 3: Missing ItemName",
    "Row 4: Invalid Category value",
    "Row 5: Invalid Condition value"
  ]
}
```

## Import Process Details

### 1. Serial Number Processing
- Trims whitespace
- Converts to uppercase: `abc123` → `ABC123`
- Adds "SN-" prefix if missing: `ABC123` → `SN-ABC123`
- Checks for duplicates against existing items in database
- Skips duplicate entries and logs them in `skippedDuplicates`

### 2. Field Value Processing
- All text fields are trimmed (leading/trailing whitespace removed)
- Empty or whitespace-only values are treated as `null`
- Case-insensitive enum matching for Category and Condition
- Flexible column name matching (prefix-based)

### 3. Image Processing
- Attempts to load images from provided paths or URLs
- Continues import even if image loading fails (no error thrown)
- Stores image as binary data (byte array) in database
- Auto-detects and stores MIME type based on file extension

### 4. Status Assignment
- All imported items are automatically assigned `Status = Available`
- Status cannot be specified in the import file
- Item status can be changed later through PATCH `/api/v1/items/{id}` endpoint

### 5. Barcode Generation
- Automatically generates barcode text for each item
- Format: `ITEM-SN-{SerialNumber}` (using the formatted serial number)
- Example: `ITEM-SN-SN-12345`
- Generates barcode image as byte array for printing/display

## Validation Rules

### Required Field Validation
- **SerialNumber**: Cannot be empty or whitespace
- **ItemName**: Cannot be empty or whitespace
- **ItemType**: Cannot be empty or whitespace
- **ItemMake**: Cannot be empty or whitespace
- **Category**: Must match valid enum value (case-insensitive)
- **Condition**: Must match valid enum value (case-insensitive)

### Uniqueness Validation
- **SerialNumber**: Must be unique across all items in the database
- Duplicate serial numbers are automatically skipped and logged

### Enum Validation
- **Category**: Must be one of: Electronics, Keys, MediaEquipment, Tools, Miscellaneous
- **Condition**: Must be one of: New, Good, Defective, Refurbished, NeedRepair
- Matching is case-insensitive
- Invalid values will cause row to fail with error message

### File Validation
- **File Extension**: Must be `.xlsx`
- **MIME Type**: Must be `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`
- **File Size**: No explicit limit (handled by server configuration)

## Error Handling

The import process is designed to be resilient and continue processing even when individual rows fail:

### Row-Level Errors
- If a single row fails validation, it's skipped
- Error is logged with row number and reason
- Other rows continue to process normally
- Final response includes all errors

### Duplicate Detection
- Items with duplicate serial numbers are automatically skipped
- Listed separately in `skippedDuplicates` array
- Does not count as a failure error

### Image Loading Failures
- If an image fails to load from path/URL, the item is still imported
- Item is saved without the image
- No error is logged for image failures

### Missing Required Fields
- Row is skipped with error: "Row X: Missing [FieldName]"
- Most common: "Missing SerialNumber"

### Invalid Enum Values
- Row fails with error indicating invalid value
- Example: "Row X: Invalid Category value"

## Common Issues and Solutions

### Issue: "Only .xlsx files are allowed"
**Solution**: Convert your file to .xlsx format. The system does not accept .xls or .csv files.

### Issue: "Missing SerialNumber"
**Solution**: Ensure every row has a value in the SerialNumber column. Empty cells will cause the row to be skipped.

### Issue: "Item with SerialNumber 'SN-XXX' already exists"
**Solution**: This serial number is already in the database. Either:
- Remove the duplicate row from your Excel file
- Update the serial number to a unique value
- The system will automatically skip duplicates

### Issue: Invalid Category or Condition
**Solution**: Check that your Category and Condition values exactly match one of the valid enum values. Case doesn't matter, but spelling must be exact.

### Issue: Images not loading
**Solution**: 
- Verify file paths are correct and accessible from the server
- For URLs, ensure they are publicly accessible
- Check file permissions for local paths
- Note: Items will still import without images if loading fails

## Best Practices

1. **Prepare Your Data**: Clean and validate your Excel file before importing
2. **Use Standard Column Names**: Stick to the documented column names for clarity
3. **Test with Small Batches**: Import a few items first to verify your format
4. **Check Serial Numbers**: Ensure all serial numbers are unique before importing
5. **Verify Enum Values**: Double-check that Category and Condition values match valid options
6. **Review Response**: Always check the response for errors or skipped items
7. **Image Paths**: Use absolute paths or URLs for images to avoid loading issues
8. **Backup First**: Consider backing up your database before large imports
9. **Monitor Logs**: Check application logs for any warnings during import
10. **Incremental Imports**: For large datasets, consider importing in batches

## Next Steps After Import

1. Review the import response for any errors or skipped items
2. Verify imported items appear correctly in the system
3. Check that barcodes were generated properly
4. Verify images loaded correctly (if provided)
5. All imported items will have `Status = Available`
6. Items can be updated individually using PATCH `/api/v1/items/{id}`
7. Barcodes can be printed for physical labeling
8. Item status can be changed as items are borrowed/reserved