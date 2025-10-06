using BackendTechnicalAssetsManagement.src.Classes;

namespace BackendTechnicalAssetsManagement.src.IRepository
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task SaveChangesAsync();
    }
}
