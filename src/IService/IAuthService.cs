using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Models;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;

namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IAuthService
    {
        Task<UserDto> Register(RegisterUserDto request);
        Task<UserDto> Login(LoginUserDto request);
        Task<MobileLoginResponseDto> LoginMobile(LoginUserDto loginDto);
        Task<MobileLoginResponseDto> RefreshTokenMobile(string refreshToken);
        Task ChangePassword(Guid userId, ChangePasswordDto request);
        Task<string> RefreshToken();
        Task Logout();
    }
}
