namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);

        bool VerifyPassword(string password, string hash);
    }
}
