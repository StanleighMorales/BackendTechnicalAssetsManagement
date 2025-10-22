using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.LentItems;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class ArchiveLentItemsService : IArchiveLentItemsService
    {
        private readonly IArchiveLentItemsRepository _repository;
        private readonly ILentItemsRepository _lentItemsRepository;
        private readonly IMapper _mapper;

        public ArchiveLentItemsService(IArchiveLentItemsRepository archiveLentItemsRepository, ILentItemsRepository lentItemsRepository, IMapper mapper)
        { 
            _repository = archiveLentItemsRepository;
            _lentItemsRepository = lentItemsRepository;
            _mapper = mapper;
        }

        public async Task<ArchiveLentItemsDto> CreateLentItemsArchiveAsync(CreateArchiveLentItemsDto createLentItemsArchive)
        {
            var lentItemsArchive = _mapper.Map<ArchiveLentItems>(createLentItemsArchive);

            lentItemsArchive.CreatedAt = DateTime.UtcNow;
            lentItemsArchive.UpdatedAt = DateTime.UtcNow;

            await _repository.CreateArchiveLentItemsAsync(lentItemsArchive);

            return _mapper.Map<ArchiveLentItemsDto>(lentItemsArchive);


        }
        public async Task<bool> DeleteLentItemsArchiveAsync(Guid id)
        {
            var lentItemsToDelete = await _repository.GetArchiveLentItemsByIdAsync(id);

            if (lentItemsToDelete == null)
            {
                return false;
            }
            await _repository.DeleteArchiveLentItemsAsync(id);
            return await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ArchiveLentItemsDto>> GetAllLentItemsArchivesAsync()
        {
            var lentItems = await _repository.GetAllArchiveLentItemsAsync();
            return _mapper.Map<IEnumerable<ArchiveLentItemsDto>>(lentItems);
        }

        public async Task<ArchiveLentItemsDto?> GetLentItemsArchiveByIdAsync(Guid id)
        {
            var lentItems = await _repository.GetArchiveLentItemsByIdAsync(id);
            return _mapper.Map<ArchiveLentItemsDto?>(lentItems);
        }

        public async Task<ArchiveLentItemsDto?> RestoreLentItemsAsync(Guid archiveId)
        {
            var archivedLentItems = await _repository.GetArchiveLentItemsByIdAsync(archiveId);

            if (archivedLentItems == null) return null;
            var restoredLentItems = _mapper.Map<LentItems>(archivedLentItems);

            await _lentItemsRepository.AddAsync(restoredLentItems);
            await _repository.DeleteArchiveLentItemsAsync(archiveId);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ArchiveLentItemsDto>(restoredLentItems);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

    }
}
