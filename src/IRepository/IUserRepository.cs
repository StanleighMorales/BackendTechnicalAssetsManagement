using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.User;

namespace BackendTechnicalAssetsManagement.src.IRepository
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdentifierAsync(string identifyer);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByPhoneNumberAsync(string phoneNumber);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<UserDto>> GetAllUserDtosAsync();

        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<IEnumerable<TeacherDto>> GetAllTeachersAsync();
        Task<IEnumerable<StaffDto>> GetAllStaffAsync();
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);

        Task DeleteAsync(Guid id);

        Task<bool> SaveChangesAsync();
    }
}
