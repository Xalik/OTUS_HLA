using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace OtusSocNet.Swagger;

public class SnakeCaseSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null)
        {
            return;
        }

        var newProperties = new Dictionary<string, OpenApiSchema>();
        foreach (var property in schema.Properties)
        {
            var snakeCaseName = JsonNamingPolicy.SnakeCaseLower.ConvertName(property.Key);
            newProperties[snakeCaseName] = property.Value;
        }

        schema.Properties = newProperties;
    }
}