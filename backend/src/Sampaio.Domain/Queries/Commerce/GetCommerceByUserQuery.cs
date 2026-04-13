using System.Text.Json.Serialization;
using MediatR;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Queries.Commerce
{
    public class GetCommerceByUserQuery : IRequest<CommerceVm>
    {
        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}