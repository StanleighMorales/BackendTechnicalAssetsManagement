namespace BackendTechnicalAssetsManagement.src.Interfaces.IService
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);

        bool VerifyPassword(string password, string hash);
    }
}
