using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using static BackendTechnicalAssetsManagement.src.DTOs.User.UserProfileDtos;

namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IUserService
    {
        Task<BaseProfileDto?> GetUserProfileByIdAsync(Guid userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);

        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<IEnumerable<TeacherDto>> GetAllTeachersAsync();

        Task<IEnumerable<StaffDto>> GetAllStaffAsync();

        // --- WRITE operations ---
        // The service will handle the "Fetch, Apply, Save" logic.
        // The controller will just pass the ID and the DTO.
        Task<bool> UpdateUserProfileAsync(Guid id, UpdateUserProfileDto dto);
        Task UpdateStaffOrAdminProfileAsync(Guid id, UpdateStaffProfileDto dto, Guid currentUserId);
        Task<bool> DeleteUserAsync(Guid id, Guid currentUserId);


    }
}
