
namespace Sampaio.Shared.Paging
{
    public class Pagination : IPagination
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SortField { get; set; } = "Id";
        public string SortType { get; set; } = "asc";
      
    }
}

