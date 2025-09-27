namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IUserValidationService
    {
        Task ValidateUniqueUserAsync(string username, string email, string phoneNumber);
    }

}
