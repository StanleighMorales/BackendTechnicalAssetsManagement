using BackendTechnicalAssetsManagement.src.DTOs.Archive.Users;

namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IArchiveUserService
    {
        Task<bool> ArchiveUserAsync(Guid targetUserId, Guid currentUserId);
        Task<IEnumerable<ArchiveUserDto>> GetAllArchivedUsersAsync();

        Task<ArchiveUserDto?> GetArchivedUserByIdAsync(Guid archiveUserId);

        Task<bool> PermanentDeleteArchivedUserAsync(Guid archiveUserId);
        Task<bool> RestoreUserAsync(Guid archiveUserId);

    }
}
