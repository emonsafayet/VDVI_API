using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Framework.Core.Swagger.Filters
{
    /// <summary>
    /// Removes Schemas from Swagger API Documentation
    /// </summary>
    public class RemoveSchemasFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            IDictionary<string, OpenApiSchema> remove = swaggerDoc.Components.Schemas;

            foreach (KeyValuePair<string, OpenApiSchema> item in remove)
            {
                swaggerDoc.Components.Schemas.Remove(item.Key);
            }
        }
    }
}
