using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Primitives;
using Sampaio.Shared.Constants;

namespace Sampaio.Api.Config.Providers
{
    public class HeaderRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            string requestCulture = httpContext.Request.Headers["lang"];

            requestCulture = string.IsNullOrEmpty(requestCulture) ? "pt-BR" : requestCulture;

            var cultures = requestCulture.Split(',');

            var userCulture = cultures.FirstOrDefault();

            userCulture = string.IsNullOrEmpty(userCulture) ? "pt-BR" : userCulture;

            if (userCulture == "pt") userCulture = "pt-BR";
            if (userCulture == "en") userCulture = "en-US";
            
            
            var suportedCultures = AppConstants.SuportedLanguages.ToList().ConvertAll(x => x.ToLower());

            if (!suportedCultures.Contains(userCulture.ToLower()))
            {
                userCulture = "pt-BR";
            }

            return Task.FromResult(new ProviderCultureResult((StringSegment) userCulture));
        }
    }
}