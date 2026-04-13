using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sampaio.Domain.Entities;
using Sampaio.Data;

namespace Sampaio.Api.Config.Startup
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AppAddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
                options.EnableSensitiveDataLogging(true);
            });
            
            return services;
        }
        
        public static IApplicationBuilder AppUseMigrations(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();

                // try
                // {
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        if (!context.Database.EnsureCreated())
                        {
                            context.Database.Migrate();    
                        }
                    }
                // }
                // catch
                // {
                //     Console.WriteLine("Erro nas migrations");
                // }

                if (env.IsDevelopment())
                {
                    // var passwordHashService = serviceScope.ServiceProvider.GetService<IPasswordHasherService>();
                    // new SeedDevelopment(context, passwordHashService).Seed();
                }
                
                return app;
            }
        }

    }
}
