using System;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class CartItemsFilter : Pagination
    {
        public Guid? CartId { get; set; }
        
        public Guid? ProductId { get; set; }
    }
}