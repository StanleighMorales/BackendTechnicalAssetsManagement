using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Repository
{
    public class LentItemsRepository : ILentItemsRepository
    {
        private readonly AppDbContext _context;
        public LentItemsRepository(AppDbContext context)
        {
            _context = context;
        }   

        public async Task<LentItems> AddAsync(LentItems lentItem)
        {
            await _context.LentItems.AddAsync(lentItem);
            return lentItem;
        }

        public async Task<IEnumerable<LentItems>> GetAllAsync()
        {
            return await _context.LentItems.ToListAsync();
        }

        public async Task<LentItems?> GetByDateTime(DateTime dateTime)
        {
            return await _context.LentItems.FirstOrDefaultAsync(li => li.LentAt == dateTime);
        }

        public async Task<LentItems?> GetByIdAsync(Guid id)
        {
            return await _context.LentItems.FirstOrDefaultAsync(li => li.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task SoftDeleteAsync(Guid id)
        {
            var itemToSoftDelete = await _context.LentItems.FindAsync(id);
            if (itemToSoftDelete != null)
            {
                itemToSoftDelete.IsDeleted = true;
            }
        }
        public Task UpdateAsync(LentItems lentItem)
        {
            _context.LentItems.Update(lentItem);
            return Task.CompletedTask;
        }
        //Admin-only methods
        public async Task<IEnumerable<LentItems>> GetDeletedAsync()
        {
            return await _context.LentItems
                .IgnoreQueryFilters()
                .Where(li => li.IsDeleted == true)
                .ToListAsync();
        }
        public async Task<IEnumerable<LentItems>> GetAllIncludingDeletedAsync()
        {
            return await _context.LentItems
                .IgnoreQueryFilters() // This will ignore the global query filter for IsDeleted
                .ToListAsync();
        }
        public async Task<LentItems?> GetDeletedByIdAsync(Guid id)
        {
            return await _context.LentItems
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(li => li.Id == id && li.IsDeleted == true);
        }

        public async Task PermaDeleteAsync(Guid id)
        {
            var itemToDelete = await _context.LentItems.FindAsync(id);
            if (itemToDelete != null)
            {
                _context.LentItems.Remove(itemToDelete);
            }
        }
        public async Task RestoreAsync(Guid id)
        {
            var item = await _context.LentItems
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(li => li.Id == id);

            if (item != null && item.IsDeleted == true)
            {
                item.IsDeleted = false;
            }
        }

    }
}
