using System;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace Sampaio.Api.Config.Startup
{
    public static class HealthChecksConfig
    {
        public static IServiceCollection AppAddHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            services.AddHealthChecks()
                .AddSqlServer(
                    connectionString: config.GetConnectionString("Default"),
                    healthQuery: "SELECT 1;",
                    name: "Database",
                    failureStatus: HealthStatus.Degraded,
                    tags: new string[] { "db", "sql"});

            return services;
        }
        
        public static IApplicationBuilder AppUseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/api/status",
                new HealthCheckOptions()
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(
                            new
                            {
                                api = report.Status.ToString(),
                                resources = report.Entries.Select(e => new
                                {
                                    resource = e.Key,
                                    status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                                })
                            });
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
            
            return app;
        }
    }
}