using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Deve.Api.Swagger
{
    /// <summary>
    /// Implements an OpenAPI operation filter to add the "Accept-Language" header parameter.
    /// </summary>
    public class AcceptLanguageHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// Adds the "Accept-Language" header parameter to API operations.
        /// </summary>
        /// <param name="operation">The OpenAPI operation being processed.</param>
        /// <param name="context">The context of the operation filter.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Style = ParameterStyle.Simple,
                Description = "Specifies the language preference for the response.",
                Required = false
            });
        }
    }
}