namespace BackendTechnicalAssetsManagement.src.IValidations
{
    public interface ISaveImageWithValidationAsync
    {
        Task<string> SaveNewImageWithValidationAsync(IFormFile image);
    }
}
