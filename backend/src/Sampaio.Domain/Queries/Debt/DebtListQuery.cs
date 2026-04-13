using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Debt
{
    public class DebtListQuery : IRequest<PagedList<DebtVm>>
    {
        public DebtFilter Filter { get; set; }
    }
}
