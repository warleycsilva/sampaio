using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        public PlanRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<Plan, bool>> Where(PlanFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<Plan>()
                .And(x => x.Deleted == false);

            if (filter == null)
                return predicate;

            predicate = (string.IsNullOrEmpty(filter.Name))
                ? predicate
                : predicate.And(x => EF.Functions.Like(x.Name.ToLower(), $"%{filter.Name.ToLower()}%"));

            predicate = (string.IsNullOrEmpty(filter.Description))
                ? predicate
                : predicate.And(x => EF.Functions.Like(x.Description.ToLower(), $"%{filter.Description.ToLower()}%"));

            return predicate;
        }
    }
}
