using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.IRepository;
using Microsoft.EntityFrameworkCore;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Repository
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly AppDbContext _context;

        public ActivityLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActivityLog> AddAsync(ActivityLog log)
        {
            await _context.ActivityLogs.AddAsync(log);
            return log;
        }

        public async Task<ActivityLog?> GetByIdAsync(Guid id)
        {
            return await _context.ActivityLogs
                .AsNoTracking()
                .Include(l => l.ActorUser)
                .Include(l => l.Item)
                .Include(l => l.LentItem)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<ActivityLog>> GetAllAsync()
        {
            return await _context.ActivityLogs
                .AsNoTracking()
                .Include(l => l.ActorUser)
                .Include(l => l.Item)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ActivityLog>> GetFilteredAsync(
            ActivityLogCategory? category,
            DateTime? from,
            DateTime? to,
            Guid? actorUserId,
            Guid? itemId,
            string? status)
        {
            var query = _context.ActivityLogs
                .AsNoTracking()
                .Include(l => l.ActorUser)
                .Include(l => l.Item)
                .AsQueryable();

            if (category.HasValue)
                query = query.Where(l => l.Category == category.Value);

            if (from.HasValue)
                query = query.Where(l => l.CreatedAt >= from.Value);

            if (to.HasValue)
                query = query.Where(l => l.CreatedAt <= to.Value);

            if (actorUserId.HasValue)
                query = query.Where(l => l.ActorUserId == actorUserId.Value);

            if (itemId.HasValue)
                query = query.Where(l => l.ItemId == itemId.Value);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(l => l.NewStatus == status);

            return await query.OrderByDescending(l => l.CreatedAt).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
