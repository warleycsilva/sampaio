using Sampaio.Shared.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Sampaio.Domain.Results.Sale;

namespace Sampaio.Domain.Commands.Sale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        [Required]
        public decimal Value { get; set; }
        public decimal DiscountValue { get; set; }

        [Required]
        public Guid CommerceId { get; set; }

        [Required]
        public Guid DriverId { get; set; }
    }
}
