using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.Results.SaleItem
{
    public class DeleteSaleItemResult
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}
