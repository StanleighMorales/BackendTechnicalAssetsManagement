using AutoMapper;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.Interfaces.IRepository;
using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Models;
using Microsoft.AspNetCore.Hosting;
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

        // --- FULLY IMPLEMENTED CREATE METHOD ---
        public async Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto) // Consistent naming
        {
            // 1. Validate for duplicate serial number
            var existingItem = await _itemRepository.GetBySerialNumberAsync(createItemDto.SerialNumber);
            if (existingItem != null)
            {
                throw new DuplicateSerialNumberException($"An item with serial number '{createItemDto.SerialNumber}' already exists.");
            }

            // 2. Handle the image file
            string? imagePath = await SaveImageWithValidationAsync(createItemDto.Image);

            // 3. Map DTO to Model
            var newItem = _mapper.Map<Item>(createItemDto);

            // 4. Manually set properties not handled by AutoMapper
            newItem.Image = imagePath;
            newItem.CreatedAt = DateTime.UtcNow;
            newItem.UpdatedAt = DateTime.UtcNow;

            // 5. Save to database via repository
            await _itemRepository.AddAsync(newItem);
            await _itemRepository.SaveChangesAsync();

            // 6. Map the final result back to a DTO to return
            return _mapper.Map<ItemDto>(newItem);
        }

        // --- All other CRUD methods, fully implemented ---
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
            var existingItem = await _itemRepository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return false;
            }

            if (updateItemDto.Image != null)
            {
                if (!string.IsNullOrEmpty(existingItem.Image))
                {
                    DeleteImage(existingItem.Image);
                }
                existingItem.Image = await SaveImageWithValidationAsync(updateItemDto.Image);
            }

            _mapper.Map(updateItemDto, existingItem);
            existingItem.UpdatedAt = DateTime.UtcNow;

            await _itemRepository.UpdateAsync(existingItem);
            return await _itemRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var itemToDelete = await _itemRepository.GetByIdAsync(id);
            if (itemToDelete == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(itemToDelete.Image))
            {
                DeleteImage(itemToDelete.Image);
            }

            await _itemRepository.DeleteAsync(id);
            return await _itemRepository.SaveChangesAsync();
        }

        // --- Helper Methods ---
        private async Task<string?> SaveImageWithValidationAsync(IFormFile? image)
        {
            if (image == null || image.Length == 0) return null;
            if (image.Length > 2 * 1024 * 1024) throw new ArgumentException("Image file size cannot exceed 2MB");

            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif", ".webp" };
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Invalid image file type.");
            }

            if (string.IsNullOrEmpty(_hostEnvironment.WebRootPath))
            {
                throw new InvalidOperationException("wwwroot folder is not configured.");
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "items");
            Directory.CreateDirectory(uploadsFolder);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return Path.Combine("images", "items", uniqueFileName).Replace('\\', '/');
        }

        private void DeleteImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || string.IsNullOrEmpty(_hostEnvironment.WebRootPath)) return;

            var physicalPath = Path.Combine(_hostEnvironment.WebRootPath, imagePath.TrimStart('/'));
            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }
        }
    }
}