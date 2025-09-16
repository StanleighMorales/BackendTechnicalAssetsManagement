using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Models;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;

namespace BackendTechnicalAssetsManagement.src.Interfaces.IService
{
    public interface IAuthService
    {
        Task<User> Register(RegisterUserDto request);
        Task<string> Login(LoginUserDto request);
        Task<string> RefreshToken();

        Task<UserDto> RegisterStaffAsync(RegisterStaffDto registerStaffDto);

        Task<UserDto> RegisterTeacherAsync(RegisterTeacherDto registerTeacherDto);

        Task<UserDto> RegisterStudentAsync(RegisterStudentDto registerStudentDto);


        Task Logout();
    }
}
