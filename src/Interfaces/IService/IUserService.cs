using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;

namespace BackendTechnicalAssetsManagement.src.Interfaces.IServices
{
    public interface IUserService
    {
        Task<GetUserProfileDto> GetUserProfileByIdAsync(int userId);
    }
}
