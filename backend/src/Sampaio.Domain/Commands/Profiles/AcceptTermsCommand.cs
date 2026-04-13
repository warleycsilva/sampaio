using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Profiles;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Commands.Profiles
{
    public class AcceptTermsCommand :  IRequest<AcceptTermsResult>
    {
        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}