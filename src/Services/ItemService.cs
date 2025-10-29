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

        public ItemService(IItemRepository itemRepository, IMapper mapper, IWebHostEnvironment hostEnvironment, IArchiveItemsService archiveItemsService)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _archiveItemsService = archiveItemsService;
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

            string barcodeText = BarcodeGenerator.GenerateItemBarcode(createItemDto.SerialNumber);

            // 2. Generate the Barcode IMAGE bytes
            byte[]? barcodeImageBytes = BarcodeImageUtil.GenerateBarcodeImageBytes(barcodeText);

            // 3. Map the DTO (Input) to the new Item (Entity)
            var newItem = _mapper.Map<Item>(createItemDto); 

            // 4. MANUALLY SET the auto-generated values on the ENTITY
            newItem.Barcode = barcodeText;
            newItem.BarcodeImage = barcodeImageBytes;

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
                string newSerialNumber = updateItemDto.SerialNumber;

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
                string barcodeText = BarcodeGenerator.GenerateItemBarcode(existingItem.SerialNumber);

                // 3. Update Barcode and BarcodeImage
                existingItem.Barcode = barcodeText;
                existingItem.BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(barcodeText);
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
            existingItem.UpdatedAt = DateTime.UtcNow;

            await _itemRepository.UpdateAsync(existingItem);
            return await _itemRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteItemAsync(Guid id) // Basically archive
        //TODO: Make sure that once deleted it will be pushed into the Item Archive
        {
            var itemToDelete = await _itemRepository.GetByIdAsync(id);
            if (itemToDelete == null) return false;

            //// We REMOVE the call to DeleteImage. It's not needed.
            //// The image bytes will be deleted from the database when the row is deleted.

            var archiveDto = _mapper.Map<CreateArchiveItemsDto>(itemToDelete);
            await _archiveItemsService.CreateItemArchiveAsync(archiveDto);

            // 4. Delete the original item from the main table
            await _itemRepository.DeleteAsync(id);

            // 5. Save the deletion change. This commits the removal of the item.
            return await _itemRepository.SaveChangesAsync();
            //await _itemRepository.DeleteAsync(id);
            //return await _itemRepository.SaveChangesAsync();
        }
        //Remove this after the Image validation is successfully working
        public async Task ImportItemsFromExcelAsync(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var itemsToCreate = new List<Item>();

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

                        foreach (DataRow row in dataTable.Rows)
                        {
                            try
                            {
                                // --- Data Reading and Validation ---
                                var serialNumber = row["SerialNumber"]?.ToString();

                                // Basic validation: Skip row if essential data like SerialNumber is missing
                                if (string.IsNullOrWhiteSpace(serialNumber))
                                {
                                    // Optional: Log a warning that a row was skipped
                                    continue;
                                }

                                // Add the "SN-" prefix, similar to your CreateItemAsync logic
                                if (!serialNumber.StartsWith("SN-"))
                                {
                                    serialNumber = $"SN-{serialNumber}";
                                }
                                var existingItem = await _itemRepository.GetBySerialNumberAsync(serialNumber);
                                if (existingItem != null)
                                {
                                    // TODO: Log that a duplicate was found and skipped.
                                    continue; 
                                }
                                var item = new Item
                                {
                                    Id = Guid.NewGuid(), // Generate new ID
                                    SerialNumber = serialNumber,
                                    ItemName = row["ItemName"]?.ToString() ?? string.Empty,
                                    ItemType = row["ItemType"]?.ToString() ?? string.Empty,
                                    ItemModel = row["ItemModel"]?.ToString(),
                                    ItemMake = row["ItemMake"]?.ToString() ?? string.Empty,
                                    Description = row["Description"]?.ToString(),

                                    // Enum Parsing with validation
                                    Category = Enum.TryParse<ItemCategory>(row["Category"]?.ToString(), true, out var category) ? category : default,
                                    Condition = Enum.TryParse<ItemCondition>(row["Condition"]?.ToString(), true, out var condition) ? condition : default,

                                    CreatedAt = DateTime.UtcNow,
                                    UpdatedAt = DateTime.UtcNow
                                };
                                itemsToCreate.Add(item);
                            }
                            catch (Exception ex)
                            {
                                // Optional: Log an error for the specific row that failed
                                // and continue processing the rest of the file.
                                // For example: Console.WriteLine($"Error processing row: {ex.Message}");
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
                // We need to add a method to our repository to handle bulk inserts.
                await _itemRepository.AddRangeAsync(itemsToCreate);
                await _itemRepository.SaveChangesAsync();
            }
        }

    }
}