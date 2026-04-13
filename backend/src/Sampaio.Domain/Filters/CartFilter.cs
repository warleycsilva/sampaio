using System;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class CartFilter : Pagination
    {
        public Guid? CommerceId { get; set; }
        
        public Guid? DriverId { get; set; }
        
        public decimal? Value { get; set; }
    }
}