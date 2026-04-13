using System;
using System.Collections.Generic;

namespace Sampaio.Shared.Paging
{
    public class PagedList<T>
    {
        public PagedList()
        {

        }

        public PagedList(IEnumerable<T> itens, long totalRecords, long pageSize)
        {
            Itens = itens;
            TotalRecords = totalRecords;
            PageSize = pageSize;
            SetTotalPages(pageSize);
        }

        public IEnumerable<T> Itens { get; set; }

        public long TotalPages { get; set; }

        public long TotalRecords { get; set; }
       
        public long PageSize { get; set; }

        private void SetTotalPages(long pageSize)
        {
            if (TotalRecords > 0)
            {
                TotalPages = (long)Math.Ceiling(TotalRecords / Convert.ToDouble(pageSize));
            }
        }
    }
}
