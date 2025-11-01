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
            return await _context.LentItems
                .Include(li => li.User)
                .Include(li => li.Teacher)
                .Include(li => li.Item)
                .ToListAsync();

        }

        public async Task<LentItems?> GetByDateTime(DateTime dateTime)
        {
            return await _context.LentItems
                .Include(li => li.User)
                .Include(li => li.Teacher)
                .Include(li => li.Item)
                .FirstOrDefaultAsync(li => li.LentAt.HasValue && li.LentAt.Value == dateTime);
        }

        public async Task<LentItems?> GetByIdAsync(Guid id)
        {
            return await _context.LentItems
                .Include(li => li.User)
                .Include(li => li.Teacher)
                .Include(li => li.Item)
                .FirstOrDefaultAsync(li => li.Id == id);
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
            // this should forward the list to archive table later

            }
        }
        public Task UpdateAsync(LentItems lentItem)
        {
            _context.LentItems.Update(lentItem);
            return Task.CompletedTask;
        }

        public async Task PermaDeleteAsync(Guid id)
        {
            var itemToDelete = await _context.LentItems.FindAsync(id);
            if (itemToDelete != null)
            {
                _context.LentItems.Remove(itemToDelete);
            }
        }

        public AppDbContext GetDbContext()
        {
            return _context;
        }

        public async Task<LentItems?> GetByBarcodeAsync(string barcode)
        {
            return await _context.LentItems
                .Include(li => li.User)
                .Include(li => li.Teacher)
                .Include(li => li.Item)
                .FirstOrDefaultAsync(li => li.Barcode == barcode);
        }
    }
}
