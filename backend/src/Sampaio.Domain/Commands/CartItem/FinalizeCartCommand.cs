using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Cart;

namespace Sampaio.Domain.Commands.Cart
{
    public class FinalizeCartCommand : IRequest<FinalizeCartResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}