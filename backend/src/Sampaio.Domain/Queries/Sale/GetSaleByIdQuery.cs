using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.Sale
{
    public class GetSaleByIdQuery : IRequest<SaleVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
