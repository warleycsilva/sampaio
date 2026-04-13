using System.Collections.Generic;
using System.Security.Claims;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Contracts.Infra
{
    public interface ILoggedUser
    {
        SessionUser User { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaims();
    }
}