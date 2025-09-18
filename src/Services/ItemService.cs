using AutoMapper;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.Interfaces.IRepository;
using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Models;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.IdentityModel.Tokens;
using TechnicalAssetManagementApi.Dtos.Item;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ImageFileManager _imageFileManager;

        public ItemService(IItemRepository itemRepository, IMapper mapper, IWebHostEnvironment hostEnvironment, ImageFileManager imageFileManager)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _imageFileManager = imageFileManager;
        }

        public class DuplicateSerialNumberException : Exception
        {
            public DuplicateSerialNumberException(string message) : base(message) { }
        }

        public async Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto)
        {
            // 1. Validate for duplicate serial number
            var existingItem = await _itemRepository.GetBySerialNumberAsync(createItemDto.SerialNumber);
            if (existingItem != null)
            {
                throw new DuplicateSerialNumberException($"An item with serial number '{createItemDto.SerialNumber}' already exists.");
            }

            var newItem = _mapper.Map<Item>(createItemDto);
            _imageFileManager.ValidateImage(createItemDto.Image);
            if (createItemDto.Image != null)
            {
                newItem.Image = await _imageFileManager.GetImageBytesAsync(createItemDto.Image);
            }
            // 4. Manually set properties not handled by AutoMapper

            newItem.CreatedAt = DateTime.UtcNow;
            newItem.UpdatedAt = DateTime.UtcNow;

            // 5. Save to database via repository
            await _itemRepository.AddAsync(newItem);
            await _itemRepository.SaveChangesAsync();

            // 6. Map the final result back to a DTO to return
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

        public async Task<bool> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await _itemRepository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return false;
            }

            var itemName = existingItem.ItemName;
            UpdateChecker.UpdatePropertyIfProvided(ref itemName, updateItemDto.ItemName);
            existingItem.ItemName = itemName;

            var serialNumber = existingItem.SerialNumber;
            UpdateChecker.UpdatePropertyIfProvided(ref serialNumber, updateItemDto.SerialNumber);
            existingItem.SerialNumber = serialNumber;

            var itemType = existingItem.ItemType;
            UpdateChecker.UpdatePropertyIfProvided(ref itemType, updateItemDto.ItemType);
            existingItem.ItemType = itemType;

            var itemModel = existingItem.ItemModel;
            UpdateChecker.UpdatePropertyIfProvided(ref itemModel, updateItemDto.ItemModel);
            existingItem.ItemModel = itemModel;

            var itemMake = existingItem.ItemMake;
            UpdateChecker.UpdatePropertyIfProvided(ref itemMake, updateItemDto.ItemMake);
            existingItem.ItemMake = itemMake;

            var description = existingItem.Description;
            UpdateChecker.UpdatePropertyIfProvided(ref description, updateItemDto.Description);
            existingItem.Description = description;

            var category = existingItem.Category;
            UpdateChecker.UpdatePropertyIfProvided(ref category, updateItemDto.Category);
            existingItem.Category = category;

            var condition = existingItem.Condition;
            UpdateChecker.UpdatePropertyIfProvided(ref condition, updateItemDto.Condition);
            existingItem.Condition = condition;

            if (updateItemDto.Image != null && updateItemDto.Image.Length > 0)
            {
                // No need to delete an old file, we will just overwrite the database field.

                // 1. Validate the new image
                _imageFileManager.ValidateImage(updateItemDto.Image);

                // 2. Convert the new image to byte[]
                var newImageBytes = await _imageFileManager.GetImageBytesAsync(updateItemDto.Image);

                // 3. Update the database record with the new byte[] data
                //    This fixes error CS0029
                existingItem.Image = newImageBytes;
            }


            existingItem.UpdatedAt = DateTime.UtcNow;

            await _itemRepository.UpdateAsync(existingItem);
            return await _itemRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var itemToDelete = await _itemRepository.GetByIdAsync(id);
            if (itemToDelete == null) return false;

            // We REMOVE the call to DeleteImage. It's not needed.
            // The image bytes will be deleted from the database when the row is deleted.

            await _itemRepository.DeleteAsync(id);
            return await _itemRepository.SaveChangesAsync();
        }
        //Remove this after the Image validation is successfully working
    }
}