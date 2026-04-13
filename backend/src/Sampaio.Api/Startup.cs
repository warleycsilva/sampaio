using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sampaio.Api.Config.Startup;
using Sampaio.Shared.Config;
using Sampaio.Shared.Mail;

namespace Sampaio.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        private const string CorsPolicy = "CorsPolicy";

        public Startup(IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine(Configuration.GetConnectionString("Default"));
            Configuration.ConfigureEnvironmentVariables<AppConfig>(services)
                .ConfigureEnvironmentVariables<EmailConfig>(services)
                .ConfigureEnvironmentVariables<AwsS3Config>(services)
                .ConfigureEnvironmentVariables<JwtTokenConfig>(services)
                .ConfigureEnvironmentVariables<AzureBlobConfig>(services)
                .ConfigureEnvironmentVariables<HangfireUserConfig>(services)
                .ConfigureEnvironmentVariables<PagarmeConfig>(services)
                .ConfigureEnvironmentVariables<GerenciaNetConfig>(services);

            services
                .AddCors(options =>
                {
                    options.AddPolicy(CorsPolicy,
                        builder =>
                            builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .WithOrigins(Configuration.GetSection("AppConfig:Domain").Value, Configuration.GetSection("AppConfig:AppUrl").Value)
                        .AllowCredentials()
                    );
                })
                .AppAddAwsS3Service(Configuration)
                .AppAddAzureStorageService(Configuration)
                .AppAddDatabase(Configuration)
                .AppAddMvc()
                .AppAddAuthorization(Configuration, Environment)
                .AppAddCompression()
                .AppAddMediator()
                .AppAddApiDocs(Configuration)
                .AppAddIoCServices(Configuration)
                .AppAddHangfire(Configuration)
                .AppAddHealthChecks(Configuration);

            if (Environment.IsProduction())
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseCors(CorsPolicy);
            app.AppUseApiDocs();
            app.AppUseHealthChecks();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.AppUseMigrations(env);
            app.AppUseHangfire(serviceProvider, Configuration, env);
            app.AppUseRecurringJobs(env);
        }
    }
}
