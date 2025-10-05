using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;

namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IUserService
    {
        Task<BaseProfileDto?> GetUserProfileByIdAsync(Guid userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);

        // --- WRITE operations ---
        // The service will handle the "Fetch, Apply, Save" logic.
        // The controller will just pass the ID and the DTO.
        Task<bool> UpdateUserProfileAsync(Guid id, UpdateUserProfileDto dto);
        Task<bool> DeleteUserAsync(Guid id);


    }
}
