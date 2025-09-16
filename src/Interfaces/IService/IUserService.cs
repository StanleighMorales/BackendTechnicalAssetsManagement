using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;

namespace BackendTechnicalAssetsManagement.src.Interfaces.IService
{
    public interface IUserService
    {
        Task<BaseProfileDto> GetUserProfileByIdAsync(Guid userId);

        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);

        Task<bool> UpdateUserAsync(int id, UserDto userDto);

        Task<bool> DeleteUserAsync(int id);


    }
}
