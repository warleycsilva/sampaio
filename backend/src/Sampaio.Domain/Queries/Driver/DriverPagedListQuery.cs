using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Driver
{
    public class DriverPagedListQuery : IRequest<PagedList<DriverVm>>
    {
        public DriverFilter Filter { get; set; }
    }
}
