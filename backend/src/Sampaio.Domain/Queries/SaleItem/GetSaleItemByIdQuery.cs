using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.SaleItem
{
    public class GetSaleItemByIdQuery : IRequest<SaleItemVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
