using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sampaio.Shared.Config;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Enums;

namespace Sampaio.Api.Config.Startup
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection AppAddAuthorization(this IServiceCollection services,
            IConfiguration config,
            IWebHostEnvironment env)
        {
            var jwtTokenConfig = new JwtTokenConfig();
            config.GetSection(nameof(JwtTokenConfig)).Bind(jwtTokenConfig);
           
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = jwtTokenConfig.TokenValidationParameters;
                x.RequireHttpsMetadata = !env.IsDevelopment();
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(SecurityPolices.Logged, p =>
                {
                    p.RequireAuthenticatedUser()
                        .Build();
                });
                
                opt.AddPolicy(SecurityPolices.Driver, p =>
                {
                    p.RequireAuthenticatedUser()
                        .RequireClaim(CustomClaims.Type, EUserType.Driver.ToString())
                        .Build();
                });
                
                opt.AddPolicy(SecurityPolices.Commerce, p =>
                {
                    p.RequireAuthenticatedUser()
                        .RequireClaim(CustomClaims.Type, EUserType.Commerce.ToString())
                        .Build();
                });
                
                opt.AddPolicy(SecurityPolices.Backoffice, p =>
                {
                    p.RequireAuthenticatedUser()
                        .RequireClaim(CustomClaims.Type, EUserType.Backoffice.ToString())
                        .Build();
                });
            });

            return services;
        }

        public static bool Validate(this AuthorizationHandlerContext ctx,
            params string[] claims)
        {
            Func<bool> predicate = null;

            foreach (var claim in claims)
            {
                if (predicate == null)
                {
                    predicate =
                        () => ctx.User.HasClaim(userClaim => userClaim.Type == claim && !string.IsNullOrWhiteSpace(userClaim.Value));
                    continue;
                }

                var aux = predicate();

                predicate =
                    () => aux || ctx.User.HasClaim(userClaim => userClaim.Type == claim && !string.IsNullOrWhiteSpace(userClaim.Value));
            }

            return predicate?.Invoke() ?? false;
        }

        public static bool Validate(this AuthorizationHandlerContext ctx,
            params Claim[] claims)
        {
            Func<bool> predicate = null;

            foreach (var claim in claims)
            {
                if (predicate == null)
                {
                    predicate =
                        () => ctx.User.HasClaim(userClaim => userClaim.Type == claim.Type && userClaim.Value == claim.Value);
                    continue;
                }

                var aux = predicate();

                predicate =
                    () => aux || ctx.User.HasClaim(userClaim => userClaim.Type == claim.Type && userClaim.Value == claim.Value);
            }

            return predicate?.Invoke() ?? false;
        }
    }
}