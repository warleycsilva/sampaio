using MediatR;
using Sampaio.Domain.Results.Profiles;

namespace Sampaio.Domain.Commands.Profiles
{
    public class ActiveAccountCommand : IRequest<ActiveAccountResult>
    {
        public string Token { get; set; }
    }
}