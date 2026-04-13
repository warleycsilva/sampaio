using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class EstablishmentFilter : Pagination
    {
        public string Name { get; set; }

        public string DocumentNumber { get; set; }
    }
}
