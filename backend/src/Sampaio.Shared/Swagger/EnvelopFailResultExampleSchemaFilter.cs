using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Sampaio.Shared.Results;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sampaio.Shared.Swagger
{
    public class EnvelopFailResultExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if(context.Type != typeof(EnvelopFailResult))return;
            
            schema.Example = new OpenApiObject
            {
                [ "errors" ] = new OpenApiArray
                {
                    new OpenApiString("Some error")
                },
                [ "success" ] = new OpenApiBoolean(false)
            };
        }
    }
}
