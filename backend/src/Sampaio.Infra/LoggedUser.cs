using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Shared.Security;

namespace Sampaio.Infra
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _accessor;

        public LoggedUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public bool IsAuthenticated() =>
            _accessor.HttpContext.User.Identity.IsAuthenticated;

        public IEnumerable<Claim> GetClaims() =>
            _accessor.HttpContext.User.Claims;

        public SessionUser User
        {
            get
            {
                var claims = GetClaims().ToList();

                if (!claims.Any())
                {
                    return null;
                }

                try
                {
                    return SessionUser.User(claims);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
