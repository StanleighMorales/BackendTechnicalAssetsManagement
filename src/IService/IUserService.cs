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
        Task<bool> UpdateStudentProfileAsync(Guid id, UpdateStudentProfileDto dto);
        Task<bool> UpdateUserProfileAsync(Guid id, UpdateUserProfileDto dto);
        Task UpdateStaffOrAdminProfileAsync(Guid id, UpdateStaffProfileDto dto, Guid currentUserId);
        Task<(bool Success, string ErrorMessage)> DeleteUserAsync(Guid id, Guid currentUserId);
        Task<ImportStudentsResponseDto> ImportStudentsFromExcelAsync(IFormFile file);
        Task<bool> CompleteStudentRegistrationAsync(Guid userId, CompleteStudentRegistrationDto dto);
        Task<(bool IsComplete, string ErrorMessage)> ValidateStudentProfileComplete(Guid userId);
        Task<object?> GetStudentByIdNumberAsync(string studentIdNumber);
    }
}
