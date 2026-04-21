using BackendTechnicalAssetsManagement.src.IService;
using Microsoft.Extensions.Configuration;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class PasswordHashingService : IPasswordHashingService
    {
        private readonly int _workFactor;

        public PasswordHashingService(IConfiguration configuration)
        {
            _workFactor = configuration.GetValue<int>("Security:BcryptWorkFactor", 12);
        }

        public PasswordHashingService(int workFactor = 12)
        {
            _workFactor = workFactor;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, _workFactor);
        }

        public bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
