using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.Users;

namespace BackendTechnicalAssetsManagement.src.IRepository
{
    public interface IArchiveUserRepository
    {
        Task<IEnumerable<ArchiveUser>> GetAllAsync();
        Task<ArchiveUser> AddAsync(ArchiveUser archiveUser);
        Task<ArchiveUser?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<ArchiveUserDto>> GetAllArchiveUserDtosAsync();
    }

}
