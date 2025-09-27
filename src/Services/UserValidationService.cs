using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class UserValidationService : IUserValidationService
    {
        private readonly IUserRepository _userRepository;
        public UserValidationService (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task ValidateUniqueUserAsync(string username, string email, string phoneNumber)
        {
            if (await _userRepository.GetByUsernameAsync(username) != null)
            {
                throw new Exception($"Username '{username}' is already taken.");
            }
            if (await _userRepository.GetByEmailAsync(email) != null)
            {
                throw new Exception($"Email '{email}' already exist.");
            }
            if (await _userRepository.GetByPhoneNumberAsync(phoneNumber) != null)
            {
                throw new Exception($"Phone Number already used.");

            }
        }
    }
}
