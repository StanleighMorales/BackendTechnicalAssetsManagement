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

        public async Task<IEnumerable<LentItems>> GetByDateTime(DateTime dateTime)
        {
            // Ensure we're working with UTC time for database comparison
            var utcDateTime = dateTime.Kind == DateTimeKind.Utc ? dateTime : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            
            // If only date is provided (time is 00:00:00), search for all items on that date
            if (utcDateTime.TimeOfDay == TimeSpan.Zero)
            {
                var startOfDay = utcDateTime.Date;
                var endOfDay = startOfDay.AddDays(1);
                
                // Debug: Log the search range
                Console.WriteLine($"Searching for date range: {startOfDay:yyyy-MM-dd HH:mm:ss.fff} to {endOfDay:yyyy-MM-dd HH:mm:ss.fff}");
                
                return await _context.LentItems
                    .Include(li => li.User)
                    .Include(li => li.Teacher)
                    .Include(li => li.Item)
                    .Where(li => li.LentAt.HasValue && 
                                li.LentAt.Value >= startOfDay && 
                                li.LentAt.Value < endOfDay)
                    .ToListAsync();
            }
            
            // If specific time is provided, search for items within that minute (ignoring seconds)
            var startOfMinute = new DateTime(utcDateTime.Year, utcDateTime.Month, utcDateTime.Day, 
                                           utcDateTime.Hour, utcDateTime.Minute, 0, DateTimeKind.Utc);
            var endOfMinute = startOfMinute.AddMinutes(1);
            
            // Debug: Log the search range
            Console.WriteLine($"Searching for time range: {startOfMinute:yyyy-MM-dd HH:mm:ss.fff} to {endOfMinute:yyyy-MM-dd HH:mm:ss.fff}");
            
            return await _context.LentItems
                .Include(li => li.User)
                .Include(li => li.Teacher)
                .Include(li => li.Item)
                .Where(li => li.LentAt.HasValue && 
                            li.LentAt.Value >= startOfMinute && 
                            li.LentAt.Value < endOfMinute)
                .ToListAsync();
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
