# Item Import Guide

## Excel Import with Image Support

Your import functionality supports importing items with images from Excel files. All imported items are automatically assigned an "Available" status.

### Supported Excel Columns

The import function supports the following columns (case-insensitive):

| Column Name | Required | Description | Example Values |
|-------------|----------|-------------|----------------|
| SerialNumber | ✅ Yes | Unique identifier (auto-converted to uppercase) | "abc123" → "SN-ABC123" |
| ItemName | ✅ Yes | Name of the item | "Dell Laptop", "Canon Camera" |
| ItemType | ✅ Yes | Type/category of item | "Laptop", "Camera", "Projector" |
| ItemMake | ✅ Yes | Manufacturer | "Dell", "Canon", "Epson" |
| ItemModel | ❌ No | Model number | "XPS 15", "EOS R6" |
| Description | ❌ No | Item description | "High-performance laptop for video editing" |
| Category | ❌ No | Item category | "Electronics", "MediaEquipment", "Tools", "Keys", "Miscellaneous" |
| Condition | ❌ No | Item condition | "New", "Good", "Defective", "Refurbished", "NeedRepair" |
| **Image** | ❌ No | Image path or URL | "/images/laptop1.jpg", "https://example.com/image.png" |

**Note**: Status is not included in imports. All imported items are automatically assigned `Status = Available`.

### Features

#### Image Import Support
- **Column**: `Image`, `ImagePath`, or `ImageUrl`
- **Supported Sources**:
  - **File Paths**: Relative or absolute paths to image files
    - Example: `images/laptop1.jpg`
    - Example: `C:\Images\camera.png`
  - **URLs**: HTTP/HTTPS URLs to images
    - Example: `https://example.com/images/projector.jpg`
- **Supported Formats**: JPG, JPEG, PNG, GIF, BMP, WEBP, SVG
- **Behavior**: If image loading fails, the item is still imported without the image

### Example Excel Structure

```
| SerialNumber | ItemName        | ItemType | ItemMake | ItemModel | Category      | Condition | Image                    |
|--------------|-----------------|----------|----------|-----------|---------------|-----------|--------------------------|
| lp001        | Dell XPS 15     | Laptop   | Dell     | XPS 15    | electronics   | new       | images/dell-xps15.jpg    |
| cam002       | Canon EOS R6    | Camera   | Canon    | EOS R6    | MediaEquipment| good      | https://example.com/canon.jpg |
| PROJ003      | Epson Projector | Projector| Epson    | HC 2250   | mediaequipment| Good      |                          |

**Note**: The example above shows mixed case to demonstrate case-insensitivity. Serial numbers will be converted to uppercase (LP001, CAM002, PROJ003), and enum values (Category, Condition) will be matched case-insensitively. All items will be imported with `Status = Available`.
```

### Import Process

1. **Serial Number Processing**: 
   - Automatically converts to uppercase (e.g., "abc123" becomes "ABC123")
   - Automatically adds "SN-" prefix if missing (e.g., "ABC123" becomes "SN-ABC123")
   - Checks for duplicates and skips duplicate entries

2. **Field Value Processing**:
   - All text fields are trimmed (leading/trailing whitespace removed)
   - Empty values are treated as null
   - Case-insensitive enum matching for Category and Condition

3. **Image Processing**:
   - Attempts to load images from provided paths/URLs
   - Continues import even if image loading fails
   - Stores image as binary data in database

4. **Status Assignment**:
   - All imported items are automatically assigned `Status = Available`
   - Status cannot be specified in the import file
   - Item status can be changed later through the update API

5. **Barcode Generation**:
   - Automatically generates barcode text and image for each item
   - Format: "ITEM-SN-{SerialNumber}" (using uppercase serial number)

### Error Handling

- **Missing Required Fields**: Rows with missing SerialNumber are skipped
- **Duplicate Serial Numbers**: Duplicate items are skipped with logging
- **Invalid Enum Values**: Invalid Category/Condition values default to first enum value
- **Image Loading Failures**: Items are imported without images if loading fails
- **Invalid File Format**: Only .xlsx files are supported

### Usage Tips

1. **Prepare Images**: Ensure image files are accessible from the server
2. **Use Consistent Naming**: Keep column names consistent (case doesn't matter)
3. **Serial Numbers**: Can use any case (e.g., "abc123", "ABC123") - will be automatically converted to uppercase
4. **Enum Values**: Category and Condition values are case-insensitive
5. **Whitespace**: Leading/trailing spaces in all fields are automatically removed
6. **Status Field**: Do not include a Status column - all items are automatically set to Available
7. **Test Small Batches**: Start with a few items to verify the format works
8. **Check Logs**: Monitor application logs for any import warnings or errors