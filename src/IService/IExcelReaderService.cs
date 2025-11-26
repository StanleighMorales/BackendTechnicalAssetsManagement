using Microsoft.AspNetCore.Http;

namespace BackendTechnicalAssetsManagement.src.IService
{
    public interface IExcelReaderService
    {
        /// <summary>
        /// Reads student data from an Excel file
        /// Returns list of tuples: (FirstName, LastName, MiddleName, RowNumber)
        /// </summary>
        Task<(List<(string FirstName, string LastName, string? MiddleName, int RowNumber)> Students, string? ErrorMessage)> ReadStudentsFromExcelAsync(IFormFile file);
    }
}
