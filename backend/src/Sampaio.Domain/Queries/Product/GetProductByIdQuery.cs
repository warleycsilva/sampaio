using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.Product
{
    public class GetProductByIdQuery : IRequest<ProductVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
