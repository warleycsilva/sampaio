using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Sampaio.Shared.Config;
using Sampaio.Shared.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Sampaio.Api.Config.Startup
{
    public static class ApiDocsConfig
    {
        public static IServiceCollection AppAddApiDocs(this IServiceCollection services,
            IConfiguration config)
        {
            var appConfig = new AppConfig();

            config.GetSection(nameof(AppConfig)).Bind(appConfig);

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

               c.SwaggerDoc("v1",
                   new OpenApiInfo {Title = "Sampaio API", Version = "v1", Description = "Sampaio API",});
               
               c.SwaggerDoc("common:v1",
                   new OpenApiInfo {Title = "Sampaio - Common Endpoints", Version = "v1", Description = "Sampaio - Common Endpoints",});
               
               c.SwaggerDoc("backoffice:v1",
                    new OpenApiInfo {Title = "Sampaio - Backoffice Endpoints", Version = "v1", Description = "Sampaio - Backoffice Endpoints",});
               
               c.SwaggerDoc("signup:v1",
                   new OpenApiInfo {Title = "Sampaio - SignUp Endpoints", Version = "v1", Description = "Sampaio - SignUp Endpoints",});
               
               c.SwaggerDoc("public:v1",
                   new OpenApiInfo {Title = "Sampaio - Public Endpoints", Version = "v1", Description = "Sampaio - Public Endpoints",});
                
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Sampaio.Api.xml"));

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Sampaio.Domain.xml"));

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Sampaio.Shared.xml"));

                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "",
                        In = ParameterLocation.Header,
                        BearerFormat = "Bearer {access_token}",
                        Scheme = "Bearer"
                    });

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.SchemaFilter<EnvelopFailResultExampleSchemaFilter>();
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        public static IApplicationBuilder AppUseApiDocs(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = false;
                c.RouteTemplate = "api/docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/api/docs/v1/swagger.json", "Documentação dos Endpoints - SampaioTur (V1)");
                c.SwaggerEndpoint($"/api/docs/signup:v1/swagger.json", "SignUp Endpoints - SampaioTur (V1)");
                c.SwaggerEndpoint($"/api/docs/public:v1/swagger.json", "Public Endpoints - SampaioTur (V1)");
                c.SwaggerEndpoint($"/api/docs/common:v1/swagger.json", "Common Endpoints - SampaioTur (V1)");
                c.SwaggerEndpoint($"/api/docs/backoffice:v1/swagger.json", "Backoffice Endpoints - SampaioTur (V1)");
                c.RoutePrefix = "api/docs";

                c.DocExpansion(DocExpansion.List);
                c.EnableDeepLinking();
                c.EnableFilter();
                //options.MaxDisplayedTags(5);
                c.ShowExtensions();
                c.DefaultModelRendering(ModelRendering.Example);

                c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put,
                    SubmitMethod.Patch, SubmitMethod.Delete);
            });
            return app;
        }
    }
}
