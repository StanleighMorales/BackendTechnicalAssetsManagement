using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Archive;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Utils;
using TechnicalAssetManagementApi.Dtos.Item;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IArchiveItemsService _archiveItemsService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ImageFileManager _imageFileManager;

        public ItemService(IItemRepository itemRepository, IMapper mapper, IWebHostEnvironment hostEnvironment, ImageFileManager imageFileManager, IArchiveItemsService archiveItemsService)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _imageFileManager = imageFileManager;
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

            // Check if the SerialNumber has changed
            if (existingItem.SerialNumber != updateItemDto.SerialNumber)
            {
                // 1. Update the serial number
                // NOTE: If updateItemDto.SerialNumber might not have the "SN-" prefix, 
                // you should standardize it here too, similar to CreateItemAsync.

                // --- START Standardization check (Recommended) ---
                string newSerialNumber = updateItemDto.SerialNumber;
                if (!string.IsNullOrEmpty(newSerialNumber) &&
                    !newSerialNumber.StartsWith("SN-", StringComparison.OrdinalIgnoreCase))
                {
                    newSerialNumber = $"SN-{newSerialNumber}";
                }
                existingItem.SerialNumber = newSerialNumber;
                // --- END Standardization check ---


                // **The Critical Step: Re-generate the Barcode TEXT and IMAGE**
                if (!string.IsNullOrEmpty(existingItem.SerialNumber))
                {
                    // 2. Generate the Barcode TEXT value (e.g., "ITEM-SN-12345")
                    string barcodeText = BarcodeGenerator.GenerateItemBarcode(existingItem.SerialNumber);

                    // 3. Store the text for searching/debugging (Recommended, uses the old slot)
                    existingItem.Barcode = barcodeText;

                    // 4. Generate the Barcode IMAGE bytes and store in the new slot
                    existingItem.BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(barcodeText);
                }
                else
                {
                    // Handle scenario where SerialNumber is cleared
                    existingItem.Barcode = null;
                    existingItem.BarcodeImage = null; // Clear the image as well
                }
            }

            // 2. Use AutoMapper to apply all the non-null properties from the DTO.
            // NOTE: existingItem.BarcodeImage is NOT overwritten here, as it's not in the DTO.
            _mapper.Map(updateItemDto, existingItem);

            // 3. Handle specific logic (image upload)
            if (updateItemDto.Image != null)
            {
                _imageFileManager.ValidateImage(updateItemDto.Image);
                var newImageBytes = await _imageFileManager.GetImageBytesAsync(updateItemDto.Image);
                existingItem.Image = newImageBytes;
            }

            // 4. Update the timestamp and save.
            existingItem.UpdatedAt = DateTime.UtcNow;

            await _itemRepository.UpdateAsync(existingItem);
            return await _itemRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteItemAsync(Guid id)
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


    }
}