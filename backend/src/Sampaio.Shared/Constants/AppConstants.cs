using System.Collections.Generic;

namespace Sampaio.Shared.Constants
{
    public class AppConstants
    {
        public const string PreferedLanguageCookieKey = "lang";

        public static readonly IReadOnlyCollection<string> SuportedLanguages = new[]
        {
            "pt-BR", "en-US"
        };

        public const string AuthSchema = "Bearer";
        public const string AuthIdentities = "Bearer,idsrv";

        public const string UserMustChangePwdCookie = "usr.mst.cng.pwd";
        public const string RedirectUriBeforeChangePwdCookie = "rd.uri.bf.cng.pwd";
    }
}