using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.CartItem;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Commands.CartItem
{
    public class RemoveCartItemCommand : IRequest<RemoveCartItemResult>
    {
        [JsonIgnore]
        public Guid CartItemId { get; set; }
        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}