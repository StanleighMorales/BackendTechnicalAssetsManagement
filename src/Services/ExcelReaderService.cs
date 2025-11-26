using BackendTechnicalAssetsManagement.src.IService;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Text;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class ExcelReaderService : IExcelReaderService
    {
        public async Task<(List<(string FirstName, string LastName, string? MiddleName, int RowNumber)> Students, string? ErrorMessage)> ReadStudentsFromExcelAsync(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var students = new List<(string FirstName, string LastName, string? MiddleName, int RowNumber)>();
            int rowNumber = 1; // Start at 1 for header row

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    if (result.Tables.Count > 0)
                    {
                        var dataTable = result.Tables[0];

                        // Create flexible column mapping
                        var columnMapping = new Dictionary<string, int>();
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            var columnName = dataTable.Columns[i].ColumnName.Trim();

                            if (columnName.Equals("LastName", StringComparison.OrdinalIgnoreCase) ||
                                columnName.Equals("Last Name", StringComparison.OrdinalIgnoreCase) ||
                                columnName.Equals("Surname", StringComparison.OrdinalIgnoreCase))
                                columnMapping["LastName"] = i;
                            else if (columnName.Equals("FirstName", StringComparison.OrdinalIgnoreCase) ||
                                     columnName.Equals("First Name", StringComparison.OrdinalIgnoreCase) ||
                                     columnName.Equals("Given Name", StringComparison.OrdinalIgnoreCase))
                                columnMapping["FirstName"] = i;
                            else if (columnName.Equals("MiddleName", StringComparison.OrdinalIgnoreCase) ||
                                     columnName.Equals("Middle Name", StringComparison.OrdinalIgnoreCase))
                                columnMapping["MiddleName"] = i;
                        }

                        // Validate required columns exist
                        if (!columnMapping.ContainsKey("LastName") || !columnMapping.ContainsKey("FirstName"))
                        {
                            return (students, "Excel file must contain 'LastName' and 'FirstName' columns.");
                        }

                        // Process each row
                        foreach (DataRow row in dataTable.Rows)
                        {
                            rowNumber++;

                            var lastName = row[columnMapping["LastName"]]?.ToString()?.Trim();
                            var firstName = row[columnMapping["FirstName"]]?.ToString()?.Trim();
                            var middleName = columnMapping.ContainsKey("MiddleName")
                                ? row[columnMapping["MiddleName"]]?.ToString()?.Trim()
                                : null;

                            // Skip rows with missing required fields (they'll be handled by the service)
                            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                            {
                                students.Add((firstName, lastName, middleName, rowNumber));
                            }
                            else
                            {
                                // Add empty entry to maintain row numbering
                                students.Add((firstName ?? "", lastName ?? "", middleName, rowNumber));
                            }
                        }
                    }
                }
            }

            return (students, null);
        }
    }
}
