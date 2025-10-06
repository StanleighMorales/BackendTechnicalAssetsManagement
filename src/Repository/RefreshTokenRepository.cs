using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.IRepository;
using Microsoft.EntityFrameworkCore;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;
    public RefreshTokenRepository(AppDbContext context) { _context = context; }

    public async Task AddAsync(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User) // Include user data when needed
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}