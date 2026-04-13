using System;
using Hangfire;
using Hangfire.AspNetCore;
using Hangfire.Common;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sampaio.Api.Config.ActionFilters;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Shared.Config;
using TimeZoneConverter;

namespace Sampaio.Api.Config.Startup
{
    public static class HangfireConfig
    {

        public static IServiceCollection AppAddHangfire(
            this IServiceCollection services,
            IConfiguration config)
        {
            var storage = new SqlServerStorage(config.GetConnectionString("Default"), new SqlServerStorageOptions()
            )
            {
                JobExpirationTimeout = TimeSpan.FromHours(1)
            };

            services.AddHangfire(x => x.UseStorage(storage));

            services.AddHangfireServer(x =>
            {
                x.Activator = new AspNetCoreJobActivator(services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>());
                x.WorkerCount = 2;
            });

            JobStorage.Current = storage;

            services.AddSingleton<IBackgroundJobClient>(new BackgroundJobClient(storage));

            return services;
        }

        public static IApplicationBuilder AppUseHangfire(
            this IApplicationBuilder app,
            IServiceProvider serviceProvider,
            IConfiguration config,
            IWebHostEnvironment env)
        {
            using var scope = serviceProvider.CreateScope();

            var jwtTokenConfig = scope.ServiceProvider.GetService<JwtTokenConfig>();
            var appConfig = scope.ServiceProvider.GetService<AppConfig>();
            var hangfireUserConfig = scope.ServiceProvider.GetService<HangfireUserConfig>();

            var dashboardOptions = new DashboardOptions
            { };

            app.UseHangfireDashboard("/jobs", dashboardOptions);

            return app;
        }

        public static IApplicationBuilder AppUseRecurringJobs(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            var timezone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");

            var cronSyncPayments = "*/10 * * * *";
            var manager = new RecurringJobManager();
            const string checkPendingPaymentsJob = "check Pending Payments Job";
            manager.RemoveIfExists(checkPendingPaymentsJob);
            manager.AddOrUpdate(checkPendingPaymentsJob,
                Job.FromExpression<IJobsService>(i => i.CheckPendingPaymentStatus()), 
                cronSyncPayments,
                new RecurringJobOptions()
                {
                    TimeZone = timezone
                });
            return app;
        }
    }
}