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

        // Full load with navigation properties — used when the caller needs User/Teacher/Item data.
        // AsNoTracking: these are read-only projections; no change tracking needed.
        public async Task<IEnumerable<LentItems>> GetAllAsync()
        {
            return await _context.LentItems
                .AsNoTracking()
                .Include(li => li.User)
                .Include(li => li.Teacher)
                .Include(li => li.Item)
                .ToListAsync();
        }

        // Lightweight: returns only the scalar fields of LentItems — no navigation properties.
        // Used by service-layer guards that only need Status, ItemId, UserId, etc.
        public async Task<IEnumerable<LentItems>> GetAllLightAsync()
        {
            return await _context.LentItems
                .AsNoTracking()
                .ToListAsync();
        }

        // Returns only active records (Pending/Approved/Borrowed) for a specific item.
        // Much cheaper than loading the whole table when checking availability.
        public async Task<IEnumerable<LentItems>> GetActiveByItemIdLightAsync(Guid itemId)
        {
            return await _context.LentItems
                .AsNoTracking()
                .Where(li => li.ItemId == itemId &&
                             (li.Status == "Pending" || li.Status == "Approved" || li.Status == "Borrowed"))
                .ToListAsync();
        }

        // Returns only active records for a specific user — used for borrowing-limit checks.
        public async Task<IEnumerable<LentItems>> GetActiveByUserIdLightAsync(Guid userId)
        {
            return await _context.LentItems
                .AsNoTracking()
                .Where(li => li.UserId == userId &&
                             (li.Status == "Pending" || li.Status == "Approved" || li.Status == "Borrowed"))
                .ToListAsync();
        }

        // Returns only Pending/Approved reservations that have passed their grace period.
        // Used by the expiry background service — avoids loading the entire table.
        public async Task<IEnumerable<LentItems>> GetExpiredReservationsAsync(DateTime cutoff)
        {
            return await _context.LentItems
                .AsNoTracking()
                .Where(li => li.ReservedFor.HasValue &&
                             li.ReservedFor.Value < cutoff &&
                             (li.Status == "Pending" || li.Status == "Approved") &&
                             !li.LentAt.HasValue)
                .ToListAsync();
        }

        public async Task<IEnumerable<LentItems>> GetByDateTime(DateTime dateTime)
        {
            var utcDateTime = dateTime.Kind == DateTimeKind.Utc
                ? dateTime
                : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            if (utcDateTime.TimeOfDay == TimeSpan.Zero)
            {
                var startOfDay = utcDateTime.Date;
                var endOfDay = startOfDay.AddDays(1);
                return await _context.LentItems
                    .AsNoTracking()
                    .Include(li => li.User)
                    .Include(li => li.Teacher)
                    .Include(li => li.Item)
                    .Where(li => li.LentAt.HasValue &&
                                 li.LentAt.Value >= startOfDay &&
                                 li.LentAt.Value < endOfDay)
                    .ToListAsync();
            }

            var startOfMinute = new DateTime(utcDateTime.Year, utcDateTime.Month, utcDateTime.Day,
                                             utcDateTime.Hour, utcDateTime.Minute, 0, DateTimeKind.Utc);
            var endOfMinute = startOfMinute.AddMinutes(1);

            return await _context.LentItems
                .AsNoTracking()
                .Include(li => li.User)
                .Include(li => li.Teacher)
                .Include(li => li.Item)
                .Where(li => li.LentAt.HasValue &&
                             li.LentAt.Value >= startOfMinute &&
                             li.LentAt.Value < endOfMinute)
                .ToListAsync();
        }

        public async Task<IEnumerable<LentItems>> GetAllBorrowedItemsAsync()
        {
            var borrowStatuses = new[] { "Pending", "Approved", "Borrowed", "Returned", "Canceled", "Denied", "Expired" };
            return await _context.LentItems
                .AsNoTracking()
                .Include(li => li.User)
                .Include(li => li.Teacher)
                .Include(li => li.Item)
                .Where(li => borrowStatuses.Contains(li.Status))
                .OrderByDescending(li => li.CreatedAt)
                .ToListAsync();
        }

        public async Task<LentItems?> GetByIdAsync(Guid id)
        {
            // Tracked — callers may update this entity
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
                // placeholder — forward to archive table later
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

        public async Task<LentItems?> GetActiveByItemIdAsync(Guid itemId)
        {
            return await _context.LentItems
                .AsNoTracking()
                .Include(li => li.User)
                .Include(li => li.Teacher)
                .Include(li => li.Item)
                .FirstOrDefaultAsync(li =>
                    li.ItemId == itemId &&
                    li.Status != "Returned" &&
                    li.Status != "Canceled" &&
                    li.Status != "Expired" &&
                    li.Status != "Denied");
        }
    }
}
