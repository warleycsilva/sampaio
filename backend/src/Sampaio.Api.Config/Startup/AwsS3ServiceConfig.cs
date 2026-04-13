using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Infra;
using Sampaio.Shared.Config;

namespace Sampaio.Api.Config.Startup
{
    public static class AwsS3ServiceConfig
    {
        public static IServiceCollection AppAddAwsS3Service(this IServiceCollection services, IConfiguration config)
        {
            var awsConfig = new AwsS3Config();
            
            config.GetSection(nameof(AwsS3Config)).Bind(awsConfig);
            
            services.AddSingleton(awsConfig);

            services.AddSingleton<IAwsS3StorageService, AwsS3StorageService>();
            
            return services;
        }
    }
}