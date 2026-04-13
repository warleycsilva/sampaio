using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.Sale;

namespace Sampaio.Domain.Commands.Sale
{
    public class DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
