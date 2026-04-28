using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
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
        private readonly ILentItemsRepository _lentItemsRepository;
        private readonly ISupabaseStorageService _storageService;
        private readonly AppDbContext _db;

        public ItemService(IItemRepository itemRepository, IMapper mapper, IWebHostEnvironment hostEnvironment, IArchiveItemsService archiveItemsService, ILentItemsRepository lentItemsRepository, ISupabaseStorageService storageService, AppDbContext db)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _archiveItemsService = archiveItemsService;
            _lentItemsRepository = lentItemsRepository;
            _storageService = storageService;
            _db = db;
        }

        public class DuplicateSerialNumberException : Exception
        {
            public DuplicateSerialNumberException(string message) : base(message) { }
        }

        public async Task<(ItemDto Item, RfidRegistrationSessionDto? Session)> CreateItemAsync(CreateItemsDto createItemDto)
        {
            // A. Standardize the SerialNumber with "SN-" prefix only if it's missing
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

            // Map the DTO to the new Item entity
            var newItem = _mapper.Map<Item>(createItemDto);

            // Set auto-generated values
            newItem.Status = ItemStatus.Available;

            if (createItemDto.Image != null)
            {
                try
                {
                    newItem.ImageUrl = await _storageService.UploadImageAsync(createItemDto.Image, "items");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to upload image: {ex.Message}", ex);
                }
            }

            await _itemRepository.AddAsync(newItem);
            await _itemRepository.SaveChangesAsync();

            var itemDto = _mapper.Map<ItemDto>(newItem);

            // C. If no RFID was provided at creation, auto-create a pending registration session
            //    so the ESP32 can pick it up immediately without a separate web action.
            RfidRegistrationSessionDto? sessionDto = null;
            if (string.IsNullOrEmpty(newItem.RfidUid))
            {
                var session = new RfidRegistrationSession
                {
                    ItemId = newItem.Id,
                    Status = "Pending",
                    ExpiresAt = DateTime.UtcNow.AddMinutes(5)
                };
                _db.RfidRegistrationSessions.Add(session);
                await _db.SaveChangesAsync();

                sessionDto = new RfidRegistrationSessionDto
                {
                    Id = session.Id,
                    ItemId = session.ItemId,
                    Status = session.Status,
                    ScannedRfidUid = session.ScannedRfidUid,
                    ErrorMessage = session.ErrorMessage,
                    CreatedAt = session.CreatedAt,
                    ExpiresAt = session.ExpiresAt
                };
            }

            return (itemDto, sessionDto);
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
                string newSerialNumber = updateItemDto.SerialNumber.ToUpperInvariant();

                if (!newSerialNumber.StartsWith("SN-", StringComparison.OrdinalIgnoreCase))
                {
                    newSerialNumber = $"SN-{newSerialNumber}";
                }

                var existingItemWithNewSN = await _itemRepository.GetBySerialNumberAsync(newSerialNumber);
                if (existingItemWithNewSN != null && existingItemWithNewSN.Id != id)
                {
                    throw new DuplicateSerialNumberException($"An item with serial number '{newSerialNumber}' already exists.");
                }

                existingItem.SerialNumber = newSerialNumber;
            }

            if (updateItemDto.Image != null)
            {
                // Delete old image from storage if it exists
                if (!string.IsNullOrEmpty(existingItem.ImageUrl))
                    await _storageService.DeleteImageAsync(existingItem.ImageUrl);

                existingItem.ImageUrl = await _storageService.UploadImageAsync(updateItemDto.Image, "items");
            }

            // 2. Use AutoMapper to apply all the *other* non-null properties from the DTO.
            _mapper.Map(updateItemDto, existingItem);

            // 3. Handle specific logic (image upload)
            // ... (Image logic is fine)

            // 4. Update the timestamp and save.
            existingItem.UpdatedAt = DateTime.UtcNow;

            await _itemRepository.UpdateAsync(existingItem);
            return await _itemRepository.SaveChangesAsync();
        }

        public async Task<(bool Success, string ErrorMessage, ItemStatus? NewStatus)> ScanRfidAsync(string rfidUid)
        {
            var item = await _itemRepository.GetByRfidUidAsync(rfidUid);
            if (item == null)
                return (false, "No item linked to this RFID tag.", null);

            var lentItem = await _lentItemsRepository.GetActiveByItemIdAsync(item.Id);
            if (lentItem == null)
                return (false, "No active lent record found for this item.", null);

            // Toggle: if currently Borrowed → Returned, otherwise → Borrowed
            bool isBorrowed = lentItem.Status?.Equals(LentItemsStatus.Borrowed.ToString(), StringComparison.OrdinalIgnoreCase) == true;
            lentItem.Status = isBorrowed
                ? LentItemsStatus.Returned.ToString()
                : LentItemsStatus.Borrowed.ToString();

            if (isBorrowed)
                lentItem.ReturnedAt = DateTime.UtcNow;
            else
                lentItem.LentAt = DateTime.UtcNow;

            await _lentItemsRepository.UpdateAsync(lentItem);
            var saved = await _lentItemsRepository.SaveChangesAsync();

            // Derive the corresponding ItemStatus for the response
            var newItemStatus = isBorrowed ? ItemStatus.Available : ItemStatus.Borrowed;
            return saved
                ? (true, string.Empty, newItemStatus)
                : (false, "Failed to save status change.", null);
        }

        public async Task<(bool Success, string ErrorMessage)> RegisterRfidToItemAsync(Guid itemId, string rfidUid)
        {
            // Check if this RFID UID is already assigned to another item
            var existing = await _itemRepository.GetByRfidUidAsync(rfidUid);
            if (existing != null && existing.Id != itemId)
                return (false, $"RFID UID '{rfidUid}' is already registered to another item.");

            var item = await _itemRepository.RegisterRfidAsync(itemId, rfidUid);
            if (item == null)
                return (false, "Item not found.");

            var saved = await _itemRepository.SaveChangesAsync();
            return saved ? (true, string.Empty) : (false, "Failed to save RFID registration.");
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateItemLocationAsync(Guid itemId, string location)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item == null)
                return (false, "Item not found.");

            item.Location = location;
            item.UpdatedAt = DateTime.UtcNow;
            await _itemRepository.UpdateAsync(item);
            var saved = await _itemRepository.SaveChangesAsync();
            return saved ? (true, string.Empty) : (false, "Failed to save location.");
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteItemAsync(Guid id) // Basically archive
        //TODO: Make sure that once deleted it will be pushed into the Item Archive
        {
            var itemToDelete = await _itemRepository.GetByIdAsync(id);
            if (itemToDelete == null) 
                return (false, "Item not found.");

            // Check if item has any active lent records (Borrowed, Pending, Approved, Reserved)
            var activeLentItems = await _lentItemsRepository.GetActiveByItemIdLightAsync(id);
            if (activeLentItems.Any())
            {
                var activeStatuses = string.Join(", ", activeLentItems.Select(li => li.Status).Distinct());
                return (false, $"Cannot archive item. It has active lent records with status: {activeStatuses}. Please return or cancel these records first.");
            }

            try
            {
                // Create a copy of the item data for archiving BEFORE modifying the entity
                var archiveDto = _mapper.Map<CreateArchiveItemsDto>(itemToDelete);
                
                // Set the archived status in the DTO (not the tracked entity)
                // UpdatedAt will be set automatically by ArchiveItemsService.CreateItemArchiveAsync
                archiveDto.Status = ItemStatus.Archived.ToString();
                
                // Create the archive record
                await _archiveItemsService.CreateItemArchiveAsync(archiveDto);

                // Before deleting the item, we need to handle the foreign key constraint
                // from ActivityLogs. We'll set ItemId to NULL in all related activity logs
                // to preserve the audit trail while allowing the item to be deleted.
                // This is done at the database level to avoid loading all activity logs into memory.
                await _itemRepository.NullifyActivityLogItemReferencesAsync(id);

                // Delete the original item from the main table
                await _itemRepository.DeleteAsync(id);

                // Save the deletion change
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
        /// Imports items from an Excel (.xlsx) file. Each item will be assigned a new GUID.
        /// Expected Excel columns: SerialNumber, ItemName, ItemType, ItemModel, ItemMake, Description, Category, Condition, Image
        /// Image column can contain file paths or URLs to load images from.
        /// All imported items are automatically assigned Status = Available.
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

                                // Handle image import (if image path/url is provided)
                                string? imageUrl = null;
                                var imageValue = GetColumnValue(row, columnMapping, "Image");
                                if (!string.IsNullOrWhiteSpace(imageValue))
                                {
                                    try
                                    {
                                        // If it's already a URL, store it directly; otherwise skip
                                        if (Uri.TryCreate(imageValue, UriKind.Absolute, out var uri) &&
                                            (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                                        {
                                            imageUrl = imageValue;
                                        }
                                    }
                                    catch
                                    {
                                        // If image loading fails, continue without image
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
                                    Id = newItemId,
                                    SerialNumber = serialNumber,
                                    ItemName = itemName,
                                    ItemType = itemType,
                                    ItemModel = itemModel,
                                    ItemMake = itemMake,
                                    Description = description,
                                    Category = Enum.TryParse<ItemCategory>(categoryValue, true, out var category) ? category : default,
                                    Condition = Enum.TryParse<ItemCondition>(conditionValue, true, out var condition) ? condition : default,
                                    Status = ItemStatus.Available,
                                    ImageUrl = imageUrl,
                                    CreatedAt = DateTime.UtcNow,
                                    UpdatedAt = DateTime.UtcNow
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

        public async Task<ItemDto?> GetItemByRfidUidAsync(string rfidUid)
        {
            var item = await _itemRepository.GetByRfidUidAsync(rfidUid);
            if (item == null) return null;
            return _mapper.Map<ItemDto>(item);
        }

    }
}
