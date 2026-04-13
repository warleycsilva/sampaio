using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.Results.SaleItem
{
    public class CreateSaleItemResult
    {
        public Guid SaleItemId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
