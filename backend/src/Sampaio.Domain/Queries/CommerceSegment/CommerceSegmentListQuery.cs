using System.Collections.Generic;
using Sampaio.Shared.Paging;
using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.CommerceSegment
{
    public class CommerceSegmentListQuery : IRequest<IEnumerable<CommerceSegmentVm>>
    {
        public CommerceSegmentFilter Filter { get; set ;}
    }
}