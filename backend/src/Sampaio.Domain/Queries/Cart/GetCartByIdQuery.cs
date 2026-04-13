using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.Cart
{
    public class GetCartByIdQuery : IRequest<CartVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}