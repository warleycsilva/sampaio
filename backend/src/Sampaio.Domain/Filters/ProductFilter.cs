using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class ProductFilter : Pagination
    {
        public Guid? CommerceId { get; set; }
        public string Name { get; set; }
        public EProductType? Type { get; set; }
        public bool? GetAll { get; set; } = false;

    }
}
