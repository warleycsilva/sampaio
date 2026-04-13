using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class SaleItemFilter : Pagination
    {
        public Guid? SaleId { get; set; }
        public Guid? ProductId { get; set; }
        
    }
}
