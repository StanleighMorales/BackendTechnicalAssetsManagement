using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
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

            // 2. Use AutoMapper to apply all the non-null properties from the DTO.
            // This one line replaces all your UpdateChecker calls!
            _mapper.Map(updateItemDto, existingItem);

            // 3. Handle specific logic that AutoMapper can't do, like file uploads. (Same as before)
            if (updateItemDto.Image != null)
            {
                _imageFileManager.ValidateImage(updateItemDto.Image);
                var newImageBytes = await _imageFileManager.GetImageBytesAsync(updateItemDto.Image);
                existingItem.Image = newImageBytes;
            }

            // 4. Update the timestamp and save. (Same as before)
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