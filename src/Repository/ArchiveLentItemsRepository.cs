using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.LentItems;
using BackendTechnicalAssetsManagement.src.IRepository;
using Microsoft.EntityFrameworkCore;
namespace BackendTechnicalAssetsManagement.src.Repository
{
    public class ArchiveLentItemsRepository : IArchiveLentItemsRepository
    {
        private readonly AppDbContext _context;
        private readonly ILentItemsRepository _lentItemsRepository;
        public ArchiveLentItemsRepository(AppDbContext context, ILentItemsRepository lentItemsRepository)
        {
            _context = context;
            _lentItemsRepository = lentItemsRepository;
        }

        public async Task<ArchiveLentItems> CreateArchiveLentItemsAsync(ArchiveLentItems archiveLentItems)
        {
            await _context.ArchiveLentItems.AddAsync(archiveLentItems);
            return archiveLentItems;
        }

        public async Task<IEnumerable<ArchiveLentItems>> GetAllArchiveLentItemsAsync()
        {
            return await _context.ArchiveLentItems.ToListAsync();
        }

        public async Task<ArchiveLentItems?> GetArchiveLentItemsByIdAsync(Guid id)
        {
            return await _context.ArchiveLentItems.FindAsync(id);
        }

        public Task<ArchiveLentItems?> UpdateArchiveLentItemsAsync(Guid id, ArchiveLentItems archiveLentItems)
        {
            _context.ArchiveLentItems.Update(archiveLentItems);
            return Task.FromResult<ArchiveLentItems?>(archiveLentItems);
        }

        public async Task DeleteArchiveLentItemsAsync(Guid id)
        {
            var listToDelete = await _context.ArchiveLentItems.FindAsync(id);
            if(listToDelete != null)
            {
                _context.ArchiveLentItems.Remove(listToDelete);
            }


        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
