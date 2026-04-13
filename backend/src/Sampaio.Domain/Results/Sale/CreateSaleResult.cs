using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.Results.Sale
{
    public class CreateSaleResult
    {
        public Guid SaleId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
