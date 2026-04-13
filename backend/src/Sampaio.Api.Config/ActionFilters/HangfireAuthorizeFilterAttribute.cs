using System.Linq;
using Hangfire.Dashboard;
using Sampaio.Shared.Config;

namespace Sampaio.Api.Config.ActionFilters
{
    public class HangfireAuthorizeFilterAttribute : IDashboardAuthorizationFilter
    {
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly AppConfig _appConfig;
        private readonly HangfireUserConfig _hangfireUserConfig;
        
        public HangfireAuthorizeFilterAttribute(JwtTokenConfig jwtTokenConfig, AppConfig appConfig, HangfireUserConfig hangfireUserConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _appConfig = appConfig;
            _hangfireUserConfig = hangfireUserConfig;
        }
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            var token = httpContext.Request.Cookies["hangire.cookie"];
            
            var valid = !string.IsNullOrEmpty(token);

            if (valid)
            {
                var principal = _jwtTokenConfig.ValidateToken(token);

                valid = principal.Claims.Any(x => x.Type == "username" && x.Value == _hangfireUserConfig.Username);
            }

            return valid;
        }
    }
}