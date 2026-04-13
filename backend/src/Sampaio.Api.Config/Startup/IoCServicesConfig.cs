using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sampaio.Data;
using Sampaio.Data.Repositories;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Contracts.Services;
using Sampaio.Domain.Services;
using Sampaio.Domain.Validators;
using Sampaio.Infra;
using Sampaio.Integrations.Pagarme;
using Sampaio.Logging;
using Sampaio.Shared.Config;
using Sampaio.Shared.Mail;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Utils;
using PasswordHasherService = Sampaio.Domain.Services.PasswordHasherService;

namespace Sampaio.Api.Config.Startup
{
    public static class IoCServicesConfig
    {
        public static IServiceCollection AppAddIoCServices(this IServiceCollection services,
            IConfiguration config)
        {
            Logging4NetFactory.InitializeLogFactory(
                new Log4NetAdapter(config.GetSection("AppConfig:Logger").Value), config.GetConnectionString("Default"));
            
            var appConfig = new AppConfig();

            config.GetSection(nameof(AppConfig)).Bind(appConfig);

            services.AddSingleton(appConfig);
            //infra

            services.AddSingleton<BaseHttpClient>();
            
            services.AddSingleton<HttpClient>();
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IPushNotificationService, PushNotificationService>();
            
            services.AddScoped<IViewRenderService, ViewRenderService>();
            
            services.AddScoped<ILoggedUser, LoggedUser>();

            services.AddScoped<IZipCodeService, ZipCodeService>();

            services.AddScoped<IMailService, MailService>();
            
            services.AddScoped<IJobsService, JobService>();
            
            services.AddScoped<IPasswordGeneratorService, PasswordGeneratorService>();

            services.AddScoped<IPasswordHasherService, Domain.Services.PasswordHasherService>();
            
             services.AddSingleton(Logging4NetFactory.Logger);

            //events
            services.AddScoped<IDomainNotification, DomainNotification>();

            // unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //validators
            services.AddValidatorsFromAssembly(typeof(SignupCommandValidator).Assembly);

            //services
            typeof(PasswordPolicyService).Assembly.GetTypes()
                .Where(x => !string.IsNullOrWhiteSpace(x.FullName) &&
                            x.FullName.Contains("Services") &&
                            x.GetInterfaces().Any() &&
                            x.IsClass)
                .ToList().ForEach(x =>
                {
                    var @interface = x.GetInterfaces().FirstOrDefault(s => s.Name.Contains(x.Name));

                    if (@interface == null) return;

                    services.AddScoped(@interface, x);
                });
            
            //pagarme
            services.AddIPagarmeApiClient(config);
            
            //repositories
            typeof(UserRepository).Assembly.GetTypes()
                .Where(x => !string.IsNullOrWhiteSpace(x.FullName) &&
                            x.FullName.Contains("Repositories") &&
                            x.GetInterfaces().Any() &&
                            x.IsClass &&
                            x != typeof(Repository<>))
                .ToList().ForEach(x =>
                {
                    var @interface = x.GetInterfaces().FirstOrDefault(s => s.Name.Contains(x.Name));

                    if (@interface == null) return;

                    services.AddScoped(@interface, x);
                });

            return services;
        }

        public static IConfiguration ConfigureEnvironmentVariables<T>(this IConfiguration config,
            IServiceCollection services)
            where T : class
        {
            var instance = (T) Activator.CreateInstance(typeof(T));
            config.Bind(instance.GetType().Name, instance);
            services.AddSingleton(instance);
            return config;
        }
    }
}
