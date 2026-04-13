using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class ClientFilter : Pagination
    {
        public string ClientName { get; set; }

        public string DocumentNumber { get; set; }
    }
}