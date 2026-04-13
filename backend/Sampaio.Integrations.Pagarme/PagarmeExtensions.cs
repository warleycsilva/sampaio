using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Polly;
using Polly.Contrib.WaitAndRetry;
using RestEase.HttpClientFactory;
using Sampaio.Integrations.Pagarme.Contracts;

namespace Sampaio.Integrations.Pagarme
{
    public static class PagarmeExtensions
    {
        public static IServiceCollection AddIPagarmeApiClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            var policy = Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrResult(x =>
                    x.StatusCode is HttpStatusCode.InternalServerError || x.StatusCode is HttpStatusCode.RequestTimeout)
                .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3));

            services.Configure<PagarmeIntegrationOptions>(configuration.GetSection(nameof(PagarmeIntegrationOptions)));


            services.AddRestEaseClient<IPagarmePaymentClient>(
                    configuration[
                        $"{nameof(PagarmeIntegrationOptions)}:{nameof(PagarmeIntegrationOptions.BaseUrl)}"],
                    configurer =>
                    {
                        configurer.JsonSerializerSettings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        };
                    })
                .AddPolicyHandler(policy);

            services.AddScoped<PagarmeClient>();

            return services;
        }
    }
}