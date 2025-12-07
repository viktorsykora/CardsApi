using System.Text.Json.Serialization;
using Application.Contracts;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Api.OpenApi
{
    internal sealed class CustomDateSchemaTransformer : IOpenApiSchemaTransformer
    {
        public Task TransformAsync(
            OpenApiSchema schema,
            OpenApiSchemaTransformerContext context,
            CancellationToken cancellationToken)
        {
            if (context.JsonPropertyInfo == null) { return Task.CompletedTask; }

            var converterAttribute = context.JsonPropertyInfo.AttributeProvider?
                .GetCustomAttributes(typeof(JsonConverterAttribute), inherit: true)
                .Cast<JsonConverterAttribute>()
                .FirstOrDefault(a => a.ConverterType == typeof(CustomDateConverter));

            if (converterAttribute != null)
            {
                schema.Type = "string";
                schema.Format = null;
                schema.Pattern = "dd.MM.yyyy";
                schema.Example = new OpenApiString(DateTime.Now.ToString(CustomDateConverter.Format));
                schema.Description = (schema.Description + $" (Format: {CustomDateConverter.Format})").Trim();
            }

            return Task.CompletedTask;
        }
    }
}