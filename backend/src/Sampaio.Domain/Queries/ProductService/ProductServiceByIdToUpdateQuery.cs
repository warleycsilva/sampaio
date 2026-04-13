using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.ProductService
{
    public class PropertByIdToUpdateQuery : IRequest<ProductServiceVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}