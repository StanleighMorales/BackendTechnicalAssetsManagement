namespace BackendTechnicalAssetsManagement.src.Interfaces.IValidations
{
    public interface IUserValidationService
    {
        Task ValidateUniqueUserAsync(string username, string email, string phoneNumber);
    }

}
