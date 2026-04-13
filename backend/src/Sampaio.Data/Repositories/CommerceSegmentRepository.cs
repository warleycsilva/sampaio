using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class CommerceSegmentRepository : Repository<CommerceSegment>, ICommerceSegmentRepository
    {
        public CommerceSegmentRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<CommerceSegment, bool>> Where(CommerceSegmentFilter filter)
        {
            var predicate = PredicateBuilder.True<CommerceSegment>();
            
            predicate = predicate.And(x => !x.Deleted);

            predicate = string.IsNullOrEmpty(filter.Name)
                ? predicate
                : predicate.And(x => EF.Functions.Like(x.Name.ToLower(), $"%{filter.Name}%"));

            predicate = (filter.Description == null)
                ? predicate
                : predicate.And(x => EF.Functions.Like(x.Description.ToLower(), $"%{filter.Description}%"));
            
            return predicate;
        }
    }
}