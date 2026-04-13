using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Sale
{
    public class SaleListQuery : IRequest<PagedList<SaleVm>>
    {
        public SaleFilter Filter { get; set; }
    }
}
