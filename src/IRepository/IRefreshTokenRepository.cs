using BackendTechnicalAssetsManagement.src.Classes;

namespace BackendTechnicalAssetsManagement.src.IRepository
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<RefreshToken?> GetLatestActiveTokenForUserAsync(Guid userId);
        Task RevokeAllForUserAsync(Guid userId);
        Task SaveChangesAsync();
    }
}
