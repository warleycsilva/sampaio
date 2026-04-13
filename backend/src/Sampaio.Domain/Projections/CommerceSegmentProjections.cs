using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class CommerceSegmentProjections
    {
        public static CommerceSegmentVm ToVm(this CommerceSegment commerce) => new CommerceSegmentVm
        {
            Id = commerce.Id,
            Name = commerce.Name,
            Description = commerce.Description,
        };

        public static IQueryable<CommerceSegmentVm> ToVm(this IQueryable<CommerceSegment> query)
            => query.Select(commerce
                => new CommerceSegmentVm
                {
                    Id = commerce.Id,
                    Name = commerce.Name,
                });
        public static IEnumerable<CommerceSegmentVm> ToVm(this IEnumerable<CommerceSegment> query)
            => query.Select(commerce
                => new CommerceSegmentVm
                {
                    Id = commerce.Id,
                    Name = commerce.Name,
                });
    }
}