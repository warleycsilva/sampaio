using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Infra;
using Sampaio.Shared.Config;

namespace Sampaio.Api.Config.Startup
{
    public static class AzureStorageServiceConfig
    {
        public static IServiceCollection AppAddAzureStorageService(this IServiceCollection services, IConfiguration config)
        {
            var azureBlobConfig = new AzureBlobConfig();
            
            config.GetSection(nameof(AzureBlobConfig)).Bind(azureBlobConfig);
            
            services.AddSingleton(azureBlobConfig);

            services.AddSingleton<IStorageService, StorageService>();
            
            return services;
        }
    }
}