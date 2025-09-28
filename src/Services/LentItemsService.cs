using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class LentItemsService : ILentItemsService
    {
        private readonly ILentItemsRepository _repository;

        public LentItemsService(ILentItemsRepository repository)
        {
            _repository = repository;
        }

        public async Task<LentItems> AddAsync(LentItems lentItem)
        {
            await _repository.AddAsync(lentItem);
            await _repository.SaveChangesAsync();
            return lentItem;
        }

        public async Task<IEnumerable<LentItems>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<LentItems?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<LentItems?> GetByDateTimeAsync(DateTime dateTime)
        {
            return await _repository.GetByDateTime(dateTime);
        }

        public async Task UpdateAsync(LentItems lentItem)
        {
            await _repository.UpdateAsync(lentItem);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> SoftDeleteAsync(Guid id)
        {
            await _repository.SoftDeleteAsync(id);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> PermaDeleteAsync(Guid id)
        {
            await _repository.PermaDeleteAsync(id);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

        // 🔹 Admin-only methods

        public async Task<IEnumerable<LentItems>> GetAllIncludingDeletedAsync()
        {
            return await _repository.GetAllIncludingDeletedAsync();
        }

        public async Task<IEnumerable<LentItems>> GetDeletedAsync()
        {
            return await _repository.GetDeletedAsync();
        }

        public async Task<LentItems?> GetDeletedByIdAsync(Guid id)
        {
            return await _repository.GetDeletedByIdAsync(id);
        }

        public async Task<bool> RestoreAsync(Guid id)
        {
            await _repository.RestoreAsync(id);
            return await _repository.SaveChangesAsync();
        }
    }
}
