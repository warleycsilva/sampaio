using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class SaleFilter : Pagination
    {
        public decimal? Value { get; set; }
        public Guid? CommerceId { get; set; }
        public Guid? DriverId { get; set; }
        public ESalesStatus? Status { get; set; }
    }
}
