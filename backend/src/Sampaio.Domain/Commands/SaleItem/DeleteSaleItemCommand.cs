using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.SaleItem;

namespace Sampaio.Domain.Commands.SaleItem
{
    public class DeleteSaleItemCommand : IRequest<DeleteSaleItemResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
