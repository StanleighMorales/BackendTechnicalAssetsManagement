namespace BackendTechnicalAssetsManagement.src.Interfaces.IValidations
{
    public interface ISaveImageWithValidationAsync
    {
        Task<string> SaveNewImageWithValidationAsync(IFormFile image);
    }
}
