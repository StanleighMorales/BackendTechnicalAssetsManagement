using BackendTechnicalAssetsManagement.src.Data;
using Microsoft.EntityFrameworkCore;
using BackendTechnicalAssetsManagement.src.Interfaces.IServices;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserProfileDto> GetUserProfileByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if(user == null)
            {
                throw new Exception("User not found");
            }

            var userProfile = new GetUserProfileDto
            {
                Id = userId,
                Username = user.Username
            };
            return userProfile;
        }
    }
}
