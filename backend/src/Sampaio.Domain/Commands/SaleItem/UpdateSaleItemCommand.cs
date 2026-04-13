using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.SaleItem;

namespace Sampaio.Domain.Commands.SaleItem
{
    public class UpdateSaleItemCommand : IRequest<UpdateSaleItemResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public Guid? SaleId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
