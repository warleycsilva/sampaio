using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.Sale;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Commands.Sale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public decimal? Value { get; set; }
        public decimal? DiscountValue { get; set; }
        public Guid? CommerceId { get; set; }
        public Guid? DriverId { get; set; }
        public ESalesStatus? Status { get; set; }
    }
}
