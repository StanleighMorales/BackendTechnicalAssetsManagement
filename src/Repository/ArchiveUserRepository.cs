using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.Users;
using BackendTechnicalAssetsManagement.src.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Repository
{
    public class ArchiveUserRepository : IArchiveUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ArchiveUserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ArchiveUser> AddAsync(ArchiveUser archiveUser)
        {
            await _context.ArchiveUsers.AddAsync(archiveUser);
            return archiveUser;
        }

        public async Task DeleteAsync(Guid id)
        {
            var userToDelete = await _context.ArchiveUsers.FindAsync(id);
            if (userToDelete != null)
            {
                _context.ArchiveUsers.Remove(userToDelete);
            }
        }

        // CORRECTED IMPLEMENTATION
        public async Task<IEnumerable<ArchiveUserDto>> GetAllArchiveUserDtosAsync()
        {
            return await _context.ArchiveUsers
                .ProjectTo<ArchiveUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ArchiveUser>> GetAllAsync()
        {
            return await _context.ArchiveUsers.ToListAsync();
        }

        public async Task<ArchiveUser?> GetByIdAsync(Guid id)
        {
            return await _context.ArchiveUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}