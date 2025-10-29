# Item Import Guide

## Excel Import with Image and Status Support

Your import functionality now supports importing items with images and status information from Excel files.

### Supported Excel Columns

The import function supports the following columns (case-insensitive):

| Column Name | Required | Description | Example Values |
|-------------|----------|-------------|----------------|
| SerialNumber | ✅ Yes | Unique identifier for the item | "ABC123", "DEF456" |
| ItemName | ✅ Yes | Name of the item | "Dell Laptop", "Canon Camera" |
| ItemType | ✅ Yes | Type/category of item | "Laptop", "Camera", "Projector" |
| ItemMake | ✅ Yes | Manufacturer | "Dell", "Canon", "Epson" |
| ItemModel | ❌ No | Model number | "XPS 15", "EOS R6" |
| Description | ❌ No | Item description | "High-performance laptop for video editing" |
| Category | ❌ No | Item category | "Electronics", "MediaEquipment", "Tools", "Keys", "Miscellaneous" |
| Condition | ❌ No | Item condition | "New", "Good", "Defective", "Refurbished", "NeedRepair" |
| **Status** | ❌ No | Item availability | "Available", "Unavailable" (defaults to "Available") |
| **Image** | ❌ No | Image path or URL | "/images/laptop1.jpg", "https://example.com/image.png" |

### New Features

#### 1. Item Status Support
- **Column**: `Status`
- **Values**: `Available` or `Unavailable`
- **Default**: If not specified, items default to `Available`
- **Usage**: Allows you to import items that are immediately marked as unavailable

#### 2. Image Import Support
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
| SerialNumber | ItemName        | ItemType | ItemMake | ItemModel | Category      | Condition | Status      | Image                    |
|--------------|-----------------|----------|----------|-----------|---------------|-----------|-------------|--------------------------|
| LP001        | Dell XPS 15     | Laptop   | Dell     | XPS 15    | Electronics   | New       | Available   | images/dell-xps15.jpg    |
| CAM002       | Canon EOS R6    | Camera   | Canon    | EOS R6    | MediaEquipment| Good      | Unavailable | https://example.com/canon.jpg |
| PROJ003      | Epson Projector | Projector| Epson    | HC 2250   | MediaEquipment| Good      | Available   |                          |
```

### Import Process

1. **Serial Number Processing**: 
   - Automatically adds "SN-" prefix if missing
   - Checks for duplicates and skips duplicate entries

2. **Image Processing**:
   - Attempts to load images from provided paths/URLs
   - Continues import even if image loading fails
   - Stores image as binary data in database

3. **Status Processing**:
   - Defaults to "Available" if not specified
   - Case-insensitive matching

4. **Barcode Generation**:
   - Automatically generates barcode text and image for each item
   - Format: "ITEM-SN-{SerialNumber}"

### Error Handling

- **Missing Required Fields**: Rows with missing SerialNumber are skipped
- **Duplicate Serial Numbers**: Duplicate items are skipped with logging
- **Invalid Enum Values**: Invalid Category/Condition/Status values default to first enum value
- **Image Loading Failures**: Items are imported without images if loading fails
- **Invalid File Format**: Only .xlsx files are supported

### Usage Tips

1. **Prepare Images**: Ensure image files are accessible from the server
2. **Use Consistent Naming**: Keep column names consistent (case doesn't matter)
3. **Test Small Batches**: Start with a few items to verify the format works
4. **Check Logs**: Monitor application logs for any import warnings or errors