using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data; // Your DbContext namespace
using BackendTechnicalAssetsManagement.src.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Item> AddAsync(Item item)
        {
            await _context.Items.AddAsync(item);
            return item;
        }

        public async Task DeleteAsync(Guid id)
        {
            var itemToDelete = await _context.Items.FindAsync(id);
            if (itemToDelete != null)
            {
                _context.Items.Remove(itemToDelete);
            }
        }
        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item?> GetByIdAsync(Guid id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<Item?> GetBySerialNumberAsync(string serialNumber)
        {
            // Using a case-insensitive comparison is more robust for serial numbers
            return await _context.Items
                .FirstOrDefaultAsync(i => i.SerialNumber != null && i.SerialNumber.ToLower() == serialNumber.ToLower());
        }
        public async Task<Item?> GetByBarcodeAsync(string barcode)
        {
            return await _context.Items
                .FirstOrDefaultAsync(i => i.Barcode == barcode);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // SaveChangesAsync returns the number of state entries written to the database.
            // Returning > 0 is a reliable way to confirm changes were made.
            return await _context.SaveChangesAsync() > 0;
        }

        public Task UpdateAsync(Item item)
        {
            // This method simply tells EF Core's change tracker that the entity is modified.
            // The actual database UPDATE command is generated when SaveChangesAsync is called.
            _context.Items.Update(item);
            return Task.CompletedTask;
        }
        public async Task AddRangeAsync(IEnumerable<Item> items)
        {
            await _context.Items.AddRangeAsync(items);
        }
    }
}