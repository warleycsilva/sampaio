using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.SaleItem
{
    public class SaleItemListQuery : IRequest<PagedList<SaleItemVm>>
    {
        public SaleItemFilter Filter { get; set; }
    }
}
