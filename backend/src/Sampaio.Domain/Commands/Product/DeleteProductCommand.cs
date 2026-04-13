using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.Product;

namespace Sampaio.Domain.Commands.Product
{
    public class DeleteProductCommand : IRequest<DeleteProductResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
