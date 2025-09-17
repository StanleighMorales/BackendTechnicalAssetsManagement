using AutoMapper;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.Interfaces.IRepository;
using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Models;
using BackendTechnicalAssetsManagement.src.Utils;
using TechnicalAssetManagementApi.Dtos.Item;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ItemService(IItemRepository itemRepository, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
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
            ValidateImageUtil.ValidateImage(createItemDto.Image);

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

        public async Task<ItemDto?> GetItemByIdAsync(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            return _mapper.Map<ItemDto>(item);
        }

        public async Task<bool> UpdateItemAsync(int id, UpdateItemDto updateItemDto)
        {
            //var existingItem = await _itemRepository.GetByIdAsync(id);
            //if (existingItem == null)
            //{
            //    return false;
            //}

            //if (updateItemDto.Image != null)
            //{
            //    if (!string.IsNullOrEmpty(existingItem.Image))
            //    {
            //        DeleteImage(existingItem.Image);
            //    }
            //    existingItem.Image = await SaveImageWithValidationAsync(updateItemDto.Image);
            //}

            //_mapper.Map(updateItemDto, existingItem);
            //existingItem.UpdatedAt = DateTime.UtcNow;

            //await _itemRepository.UpdateAsync(existingItem);
            //return await _itemRepository.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            //var itemToDelete = await _itemRepository.GetByIdAsync(id);
            //if (itemToDelete == null)
            //{
            //    return false;
            //}

            //if (!string.IsNullOrEmpty(itemToDelete.Image))
            //{
            //    DeleteImage(itemToDelete.Image);
            //}

            //await _itemRepository.DeleteAsync(id);
            //return await _itemRepository.SaveChangesAsync
            //
            throw new NotImplementedException();
        }
        //Remove this after the Image validation is successfully working
    }
}