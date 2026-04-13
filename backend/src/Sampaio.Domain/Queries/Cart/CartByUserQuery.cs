using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Queries.Cart
{
    public class CartByUserQuery : IRequest<CartVm>
    {
        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}