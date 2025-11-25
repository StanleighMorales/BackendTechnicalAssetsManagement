using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.Items;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Utils;
using ExcelDataReader;
using System.Data;
using System.Text;
using TechnicalAssetManagementApi.Dtos.Item;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IArchiveItemsService _archiveItemsService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IBarcodeGeneratorService _barcodeGenerator;

        public ItemService(IItemRepository itemRepository, IMapper mapper, IWebHostEnvironment hostEnvironment, IArchiveItemsService archiveItemsService, IBarcodeGeneratorService barcodeGenerator)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _archiveItemsService = archiveItemsService;
            _barcodeGenerator = barcodeGenerator;
        }

        public class DuplicateSerialNumberException : Exception
        {
            public DuplicateSerialNumberException(string message) : base(message) { }
        }

        public async Task<ItemDto> CreateItemAsync(CreateItemsDto createItemDto)
        {
            // A. **Standardize the SerialNumber with "SN-" prefix only if it's missing**
            if (string.IsNullOrEmpty(createItemDto.SerialNumber))
            {
                throw new ArgumentException("SerialNumber cannot be empty.");
            }

            // Convert serial number to uppercase
            createItemDto.SerialNumber = createItemDto.SerialNumber.ToUpperInvariant();

            // Check if it already has the prefix (assuming case-insensitivity might be safer)
            if (!createItemDto.SerialNumber.StartsWith("SN-", StringComparison.OrdinalIgnoreCase))
            {
                createItemDto.SerialNumber = $"SN-{createItemDto.SerialNumber}";
            }

            ImageConverterUtils.ValidateImage(createItemDto.Image);

            // B. Validate for duplicate serial number (using the standardized number)
            var existingItem = await _itemRepository.GetBySerialNumberAsync(createItemDto.SerialNumber);
            if (existingItem != null)
            {
                throw new DuplicateSerialNumberException($"An item with serial number '{createItemDto.SerialNumber}' already exists.");
            }

            string barcodeText = _barcodeGenerator.GenerateItemBarcode(createItemDto.SerialNumber);

            // 2. Generate the Barcode IMAGE bytes
            byte[]? barcodeImageBytes = _barcodeGenerator.GenerateBarcodeImage(barcodeText);

            // 3. Map the DTO (Input) to the new Item (Entity)
            var newItem = _mapper.Map<Item>(createItemDto); 

            // 4. MANUALLY SET the auto-generated values on the ENTITY
            newItem.Barcode = barcodeText;
            newItem.BarcodeImage = barcodeImageBytes;
            newItem.Status = ItemStatus.Available; // Always set to Available for new items

            if (createItemDto.Image != null)
            {
                newItem.ImageMimeType = createItemDto.Image.ContentType;
            }

            // ... rest of the saving code ...
            await _itemRepository.AddAsync(newItem);
            await _itemRepository.SaveChangesAsync();

            // 5. Map the final ENTITY (which now has Barcode and BarcodeImage) to the ItemDto (Output)
            return _mapper.Map<ItemDto>(newItem);
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }

        public async Task<ItemDto?> GetItemByIdAsync(Guid id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            return _mapper.Map<ItemDto>(item);
        }
        public async Task<ItemDto?> GetItemByBarcodeAsync(string barcode)
        {
            var item = await _itemRepository.GetByBarcodeAsync(barcode);

            // Use the existing mapper instance to convert Item to ItemDto
            return _mapper.Map<ItemDto>(item);
        }

        public async Task<ItemDto?> GetItemBySerialNumberAsync(string serialNumber)
        {
            var item = await _itemRepository.GetBySerialNumberAsync(serialNumber);
            return _mapper.Map<ItemDto>(item);
        }

        public async Task<bool> UpdateItemAsync(Guid id, UpdateItemsDto updateItemDto)
        {
            var existingItem = await _itemRepository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return false;
            }
            ImageConverterUtils.ValidateImage(updateItemDto.Image);

            // Check if the SerialNumber has changed
            if (updateItemDto.SerialNumber != null && existingItem.SerialNumber != updateItemDto.SerialNumber)
            {
                // 1. Update the serial number
                string newSerialNumber = updateItemDto.SerialNumber.ToUpperInvariant();

                // Standardization check
                if (!newSerialNumber.StartsWith("SN-", StringComparison.OrdinalIgnoreCase))
                {
                    newSerialNumber = $"SN-{newSerialNumber}";
                }

                // Check for duplication on the *new* serial number
                var existingItemWithNewSN = await _itemRepository.GetBySerialNumberAsync(newSerialNumber);
                // Ensure the duplicate isn't the item we are currently updating
                if (existingItemWithNewSN != null && existingItemWithNewSN.Id != id)
                {
                    throw new DuplicateSerialNumberException($"An item with serial number '{newSerialNumber}' already exists.");
                }

                existingItem.SerialNumber = newSerialNumber;

                // **The Critical Step: Re-generate the Barcode TEXT and IMAGE**
                // Since we checked for null above, we know existingItem.SerialNumber will be non-null here.

                // 2. Generate the Barcode TEXT value
                string barcodeText = _barcodeGenerator.GenerateItemBarcode(existingItem.SerialNumber);

                // 3. Update Barcode and BarcodeImage
                existingItem.Barcode = barcodeText;
                existingItem.BarcodeImage = _barcodeGenerator.GenerateBarcodeImage(barcodeText);
            }
            // ELSE: If updateItemDto.SerialNumber is null, the existing SerialNumber, Barcode, and BarcodeImage are preserved.

            if (updateItemDto.Image != null)
            {
                // ImageConverterUtils is used by the Create flow and converts IFormFile to byte[].
                existingItem.Image = ImageConverterUtils.ConvertIFormFileToByteArray(updateItemDto.Image);
                existingItem.ImageMimeType = updateItemDto.Image.ContentType;
            }

            // 2. Use AutoMapper to apply all the *other* non-null properties from the DTO.
            // The AutoMapper ignore rules for SerialNumber, Barcode, and BarcodeImage are still necessary
            // to prevent any accidental mapping that might be happening for other reasons, 
            // but the manual logic above was the primary cause.
            _mapper.Map(updateItemDto, existingItem);

            // 3. Handle specific logic (image upload)
            // ... (Image logic is fine)

            // 4. Update the timestamp and save.
            existingItem.UpdatedAt = DateTime.Now;

            await _itemRepository.UpdateAsync(existingItem);
            return await _itemRepository.SaveChangesAsync();
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteItemAsync(Guid id) // Basically archive
        //TODO: Make sure that once deleted it will be pushed into the Item Archive
        {
            var itemToDelete = await _itemRepository.GetByIdAsync(id);
            if (itemToDelete == null) 
                return (false, "Item not found.");

            try
            {
                //// We REMOVE the call to DeleteImage. It's not needed.
                //// The image bytes will be deleted from the database when the row is deleted.

                // Set item status to Archived before archiving
                itemToDelete.Status = ItemStatus.Archived;
                itemToDelete.UpdatedAt = DateTime.Now;

                var archiveDto = _mapper.Map<CreateArchiveItemsDto>(itemToDelete);
                await _archiveItemsService.CreateItemArchiveAsync(archiveDto);

                // 4. Delete the original item from the main table
                await _itemRepository.DeleteAsync(id);

                // 5. Save the deletion change. This commits the removal of the item.
                var success = await _itemRepository.SaveChangesAsync();
                
                return success 
                    ? (true, string.Empty) 
                    : (false, "Failed to save changes during archiving process.");
            }
            catch (Exception ex)
            {
                return (false, $"Archive operation failed: {ex.Message}");
            }
        }
        /// <summary>
        /// Imports items from an Excel (.xlsx) file. Each item will be assigned a new GUID and barcode.
        /// Expected Excel columns: SerialNumber, ItemName, ItemType, ItemModel, ItemMake, Description, Category, Condition, Image
        /// The barcode will be generated with format: "ITEM-SN-{SerialNumber}"
        /// Image column can contain file paths or URLs to load images from
        /// All imported items are automatically assigned Status = Available
        /// </summary>
        /// <param name="file">Excel file (.xlsx format only)</param>
        public async Task<ImportItemsResponseDto> ImportItemsFromExcelAsync(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var response = new ImportItemsResponseDto();
            var itemsToCreate = new List<Item>();
            int rowNumber = 1; // Start at 1 for header row

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true // Assumes first row is the header
                        }
                    });

                    if (result.Tables.Count > 0)
                    {
                        var dataTable = result.Tables[0];
                        response.TotalProcessed = dataTable.Rows.Count;

                        // Create a flexible column mapping to handle different header formats
                        var columnMapping = new Dictionary<string, string>();
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            var columnName = column.ColumnName.Trim();
                            
                            // Map columns with flexible matching (case-insensitive and handles extra text)
                            if (columnName.StartsWith("SerialNumber", StringComparison.OrdinalIgnoreCase))
                                columnMapping["SerialNumber"] = column.ColumnName;
                            else if (columnName.StartsWith("ItemName", StringComparison.OrdinalIgnoreCase))
                                columnMapping["ItemName"] = column.ColumnName;
                            else if (columnName.StartsWith("ItemType", StringComparison.OrdinalIgnoreCase))
                                columnMapping["ItemType"] = column.ColumnName;
                            else if (columnName.StartsWith("ItemModel", StringComparison.OrdinalIgnoreCase))
                                columnMapping["ItemModel"] = column.ColumnName;
                            else if (columnName.StartsWith("ItemMake", StringComparison.OrdinalIgnoreCase))
                                columnMapping["ItemMake"] = column.ColumnName;
                            else if (columnName.StartsWith("Description", StringComparison.OrdinalIgnoreCase))
                                columnMapping["Description"] = column.ColumnName;
                            else if (columnName.StartsWith("Category", StringComparison.OrdinalIgnoreCase))
                                columnMapping["Category"] = column.ColumnName;
                            else if (columnName.StartsWith("Condition", StringComparison.OrdinalIgnoreCase))
                                columnMapping["Condition"] = column.ColumnName;
                            // Status column is ignored - all imported items default to Available
                            else if (columnName.StartsWith("Image", StringComparison.OrdinalIgnoreCase) || 
                                     columnName.StartsWith("ImagePath", StringComparison.OrdinalIgnoreCase) ||
                                     columnName.StartsWith("ImageUrl", StringComparison.OrdinalIgnoreCase))
                                columnMapping["Image"] = column.ColumnName;
                        }

                        foreach (DataRow row in dataTable.Rows)
                        {
                            rowNumber++;
                            try
                            {
                                // --- Data Reading and Validation ---
                                var serialNumber = GetColumnValue(row, columnMapping, "SerialNumber")?.Trim();

                                // Basic validation: Skip row if essential data like SerialNumber is missing
                                if (string.IsNullOrWhiteSpace(serialNumber))
                                {
                                    response.FailureCount++;
                                    response.Errors.Add($"Row {rowNumber}: Missing SerialNumber");
                                    continue;
                                }

                                // Convert serial number to uppercase
                                serialNumber = serialNumber.ToUpperInvariant();

                                // Add the "SN-" prefix, similar to your CreateItemAsync logic
                                if (!serialNumber.StartsWith("SN-", StringComparison.OrdinalIgnoreCase))
                                {
                                    serialNumber = $"SN-{serialNumber}";
                                }

                                // Check for duplicate serial number
                                var existingItem = await _itemRepository.GetBySerialNumberAsync(serialNumber);
                                if (existingItem != null)
                                {
                                    response.FailureCount++;
                                    response.SkippedDuplicates.Add($"Row {rowNumber}: Item with SerialNumber '{serialNumber}' already exists");
                                    continue; 
                                }

                                // Generate GUID for the new item
                                var newItemId = Guid.NewGuid();

                                // Generate barcode text using the same structure as normal item creation
                                string barcodeText = _barcodeGenerator.GenerateItemBarcode(serialNumber);

                                // Generate barcode image bytes
                                byte[]? barcodeImageBytes = _barcodeGenerator.GenerateBarcodeImage(barcodeText);

                                // Handle image import (if image path/url is provided)
                                byte[]? imageBytes = null;
                                string? imageMimeType = null;
                                var imageValue = GetColumnValue(row, columnMapping, "Image");
                                if (!string.IsNullOrWhiteSpace(imageValue))
                                {
                                    try
                                    {
                                        // Try to load image from file path or URL
                                        imageBytes = await LoadImageFromPathOrUrlAsync(imageValue);
                                        imageMimeType = GetMimeTypeFromPath(imageValue);
                                    }
                                    catch
                                    {
                                        // If image loading fails, continue without image
                                        // Optional: Log the error
                                    }
                                }

                                // Read and normalize all field values (trim whitespace, handle case-insensitivity)
                                var itemName = GetColumnValue(row, columnMapping, "ItemName")?.Trim() ?? string.Empty;
                                var itemType = GetColumnValue(row, columnMapping, "ItemType")?.Trim() ?? string.Empty;
                                var itemModel = GetColumnValue(row, columnMapping, "ItemModel")?.Trim();
                                var itemMake = GetColumnValue(row, columnMapping, "ItemMake")?.Trim() ?? string.Empty;
                                var description = GetColumnValue(row, columnMapping, "Description")?.Trim();
                                var categoryValue = GetColumnValue(row, columnMapping, "Category")?.Trim();
                                var conditionValue = GetColumnValue(row, columnMapping, "Condition")?.Trim();

                                var item = new Item
                                {
                                    Id = newItemId, // Use generated GUID
                                    SerialNumber = serialNumber,
                                    ItemName = itemName,
                                    ItemType = itemType,
                                    ItemModel = itemModel,
                                    ItemMake = itemMake,
                                    Description = description,

                                    // Enum Parsing with case-insensitive validation
                                    Category = Enum.TryParse<ItemCategory>(categoryValue, true, out var category) ? category : default,
                                    Condition = Enum.TryParse<ItemCondition>(conditionValue, true, out var condition) ? condition : default,
                                    Status = ItemStatus.Available, // All imported items default to Available

                                    // Set image information
                                    Image = imageBytes,
                                    ImageMimeType = imageMimeType,

                                    // Set barcode information
                                    Barcode = barcodeText,
                                    BarcodeImage = barcodeImageBytes,

                                    CreatedAt = DateTime.Now,
                                    UpdatedAt = DateTime.Now
                                };
                                itemsToCreate.Add(item);
                                response.SuccessCount++;
                            }
                            catch (Exception ex)
                            {
                                response.FailureCount++;
                                response.Errors.Add($"Row {rowNumber}: {ex.Message}");
                                continue;
                            }
                        }
                    }
                }
            }

            // --- Database Operation ---
            // Now that we have the list, we save it to the database.
            if (itemsToCreate.Any())
            {
                await _itemRepository.AddRangeAsync(itemsToCreate);
                await _itemRepository.SaveChangesAsync();
            }

            return response;
        }

        /// <summary>
        /// Helper method to safely get column values from DataRow using flexible column mapping
        /// Returns trimmed values with null/whitespace handling
        /// </summary>
        /// <param name="row">The DataRow to read from</param>
        /// <param name="columnMapping">Dictionary mapping logical column names to actual column names</param>
        /// <param name="logicalColumnName">The logical column name to retrieve</param>
        /// <returns>The column value as string (trimmed), or null if not found or empty</returns>
        private static string? GetColumnValue(DataRow row, Dictionary<string, string> columnMapping, string logicalColumnName)
        {
            if (columnMapping.TryGetValue(logicalColumnName, out var actualColumnName))
            {
                var value = row[actualColumnName]?.ToString()?.Trim();
                return string.IsNullOrWhiteSpace(value) ? null : value;
            }
            return null;
        }

        /// <summary>
        /// Loads image bytes from a file path or URL
        /// </summary>
        /// <param name="imagePathOrUrl">File path or URL to the image</param>
        /// <returns>Image bytes</returns>
        private async Task<byte[]?> LoadImageFromPathOrUrlAsync(string imagePathOrUrl)
        {
            if (string.IsNullOrWhiteSpace(imagePathOrUrl))
                return null;

            try
            {
                // Check if it's a URL
                if (Uri.TryCreate(imagePathOrUrl, UriKind.Absolute, out var uri) && 
                    (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                {
                    // Load from URL
                    using var httpClient = new HttpClient();
                    return await httpClient.GetByteArrayAsync(uri);
                }
                else
                {
                    // Treat as file path
                    var fullPath = Path.IsPathRooted(imagePathOrUrl) 
                        ? imagePathOrUrl 
                        : Path.Combine(_hostEnvironment.ContentRootPath, imagePathOrUrl);
                    
                    if (File.Exists(fullPath))
                    {
                        return await File.ReadAllBytesAsync(fullPath);
                    }
                }
            }
            catch
            {
                // Return null if loading fails
            }

            return null;
        }

        /// <summary>
        /// Gets MIME type from file path or URL
        /// </summary>
        /// <param name="pathOrUrl">File path or URL</param>
        /// <returns>MIME type string</returns>
        private static string? GetMimeTypeFromPath(string pathOrUrl)
        {
            var extension = Path.GetExtension(pathOrUrl)?.ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                ".svg" => "image/svg+xml",
                _ => "image/jpeg" // Default fallback
            };
        }

    }
}
