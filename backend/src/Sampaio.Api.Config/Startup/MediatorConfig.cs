using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sampaio.Api.Config.Pipelines;
using Sampaio.Domain.CommandHandlers;

namespace Sampaio.Api.Config.Startup
{
    public static class MediatorConfig
    {
        public static IServiceCollection AppAddMediator(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            services.AddMediatR(
                typeof(AuthorizationCommandHandler).GetTypeInfo().Assembly);

            return services;
        }
    }
}