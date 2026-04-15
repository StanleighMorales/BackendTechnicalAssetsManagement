using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BackendTechnicalAssetsManagement.src.Filters
{
    /// <summary>
    /// Swagger operation filter to properly handle file upload endpoints with IFormFile parameters.
    /// This filter modifies the OpenAPI schema to correctly represent multipart/form-data requests.
    /// </summary>
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check if any parameter is IFormFile
            var fileParameters = context.ApiDescription.ParameterDescriptions
                .Where(p => p.Type == typeof(IFormFile) || 
                           (p.Type != null && p.Type.Name == "IFormFile"))
                .ToList();

            if (!fileParameters.Any())
                return;

            // Clear existing parameters to avoid conflicts
            operation.Parameters?.Clear();

            // Set request body to multipart/form-data with file upload schema
            operation.RequestBody = new OpenApiRequestBody
            {
                Required = true,
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["file"] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary",
                                    Description = "Upload file"
                                }
                            },
                            Required = new HashSet<string> { "file" }
                        }
                    }
                }
            };
        }
    }
}
