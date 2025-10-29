using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.Items;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class ArchiveItemsService : IArchiveItemsService
    {
        private readonly IArchiveItemRepository _archiveItemRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ArchiveItemsService(IArchiveItemRepository archiveItemRepository, IItemRepository itemRepository, IMapper mapper)
        {
            _archiveItemRepository = archiveItemRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }
        public async Task<ArchiveItemsDto> CreateItemArchiveAsync(CreateArchiveItemsDto createItemArchive)
        {
            var itemArchive = _mapper.Map<ArchiveItems>(createItemArchive);

            itemArchive.CreatedAt = DateTime.UtcNow;
            itemArchive.UpdatedAt = DateTime.UtcNow;

            await _archiveItemRepository.CreateItemArchiveAsync(itemArchive);

            return _mapper.Map<ArchiveItemsDto>(itemArchive);
        }

        public async Task<bool> DeleteItemArchiveAsync(Guid id)
        {
            // 1. Await the repository call to get the result (ArchiveItems or null)
            var itemToDelete = await _archiveItemRepository.GetItemArchiveByIdAsync(id);

            // 2. Check if the actual item is null
            if (itemToDelete == null)
            {
                return false; // Item not found
            }

            // 3. Proceed with deletion
            await _archiveItemRepository.DeleteItemArchiveAsync(id);

            // 4. Save changes and return true/false based on success (i.e., if rows were affected)
            return await _archiveItemRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ArchiveItemsDto>> GetAllItemArchivesAsync()
        {
            var items = await _archiveItemRepository.GetAllItemArchivesAsync();
            return _mapper.Map<IEnumerable<ArchiveItemsDto>>(items);
        }

        public async Task<ArchiveItemsDto?> GetItemArchiveByIdAsync(Guid id)
        {
            var item = await _archiveItemRepository.GetItemArchiveByIdAsync(id);
            return _mapper.Map<ArchiveItemsDto?>(item);
        }

        public async Task<ItemDto?> RestoreItemAsync(Guid archiveId)
        {
            var archivedItem = await _archiveItemRepository.GetItemArchiveByIdAsync(archiveId);
            if (archivedItem == null) return null;

            // Because of our mapping change, restoredItem now has the correct, original Id
            var restoredItem = _mapper.Map<Item>(archivedItem);

            // Set new timestamps and ensure status is Available when restored
            restoredItem.CreatedAt = DateTime.UtcNow;
            restoredItem.UpdatedAt = DateTime.UtcNow;
            restoredItem.Status = ItemStatus.Available; // Restored items should be available

            // EF Core will now happily add this item with its specified Id
            await _itemRepository.AddAsync(restoredItem);

            // Delete from archive
            await _archiveItemRepository.DeleteItemArchiveAsync(archiveId);

            // Save all changes in one transaction
            await _archiveItemRepository.SaveChangesAsync();

            return _mapper.Map<ItemDto>(restoredItem);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _archiveItemRepository.SaveChangesAsync();
        }
         
        public async Task<bool> UpdateItemArchiveAsync(Guid id, UpdateArchiveItemsDto UpdateItemArchive)
        {
            var itemToUpdate = await _archiveItemRepository.GetItemArchiveByIdAsync(id);

            if (itemToUpdate == null)
            {
                return false; // Not found
            }
            _mapper.Map(UpdateItemArchive, itemToUpdate);

            await _archiveItemRepository.UpdateItemArchiveAsync(itemToUpdate);
            return await _archiveItemRepository.SaveChangesAsync();
        }
    }
}
