using Sampaio.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class DriverFilter : Pagination
    {
        public string? CnhNumber { get; set; }
        public string? Name { get; set; }
        
        public Guid? PlanId { get; set; }
    }
}
