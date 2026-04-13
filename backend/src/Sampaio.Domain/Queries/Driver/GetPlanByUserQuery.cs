using MediatR;
using System.Text.Json.Serialization;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Queries.Driver
{
    public class GetPlanByUserQuery : IRequest<DriverVm>
    {
        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}