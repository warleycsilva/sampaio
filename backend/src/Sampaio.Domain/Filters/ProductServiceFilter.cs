using Sampaio.Shared.Enums;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class ProductServiceFilter: Pagination
    {
        public string Name { get; set; }
        public EProductServiceType? Type { get; set; }
    }
}