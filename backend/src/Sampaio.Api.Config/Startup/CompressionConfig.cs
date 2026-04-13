using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace Sampaio.Api.Config.Startup
{
    public static class CompressionConfig
    {
        public static IServiceCollection AppAddCompression(this IServiceCollection services)
        {
//            services.Configure<GzipCompressionProviderOptions>(x =>
//            {
//                x.Level = CompressionLevel.Fastest;
//            });
//
//            services.AddResponseCompression(x =>
//            {
//                x.Providers.Add<GzipCompressionProvider>();
//            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
            });
            
            return services;
        }
    }
}