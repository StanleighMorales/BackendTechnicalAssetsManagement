using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Models;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;

namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IAuthService
    {
        Task<UserDto> Register(RegisterUserDto request);
        Task<UserDto> Login(LoginUserDto request);
        Task<string> RefreshToken();
        Task Logout();
    }
}
