using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Sampaio.Domain.Results.SaleItem;

namespace Sampaio.Domain.Commands.SaleItem
{
    public class CreateSaleItemCommand : IRequest<CreateSaleItemResult>
    {
        [Required]
        public Guid SaleId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
