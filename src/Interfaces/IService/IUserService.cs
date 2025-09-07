using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;

namespace BackendTechnicalAssetsManagement.src.Interfaces.IService
{
    public interface IUserService
    {
        Task<GetUserProfileDto> GetUserProfileByIdAsync(int userId);
    }
}
