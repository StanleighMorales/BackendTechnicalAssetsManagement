 using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Repository
{
    public class ArchiveItemsRepository : IArchiveItemRepository
    {
        private readonly AppDbContext _context;
        public ArchiveItemsRepository(AppDbContext context) 
        { 
            _context = context;
        }
        public async Task<ArchiveItems> CreateItemArchiveAsync(ArchiveItems itemArchive)
        {
            await _context.ArchiveItems.AddAsync(itemArchive);
            return itemArchive;

        }

        public async Task DeleteItemArchiveAsync(Guid id)
        {
            var itemToDelete = await _context.ArchiveItems.FindAsync(id);

            if (itemToDelete != null) {
                _context.ArchiveItems.Remove(itemToDelete);
            }
        }

        public async Task<IEnumerable<ArchiveItems>> GetAllItemArchivesAsync()
        {
            return await _context.ArchiveItems.ToListAsync();
        }

        public async Task<ArchiveItems?> GetItemArchiveByIdAsync(Guid id)
        {
            return await _context.ArchiveItems.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        //Currently not used
        public Task<ArchiveItems?> UpdateItemArchiveAsync(ArchiveItems itemArchive)
        {
            _context.ArchiveItems.Update(itemArchive);
            return Task.FromResult<ArchiveItems?>(itemArchive);
        }
    }
}
