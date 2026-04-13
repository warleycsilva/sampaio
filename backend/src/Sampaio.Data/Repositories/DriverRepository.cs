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
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        public DriverRepository(DataContext context)
             : base(context)
        {
        }

        public Expression<Func<Driver, bool>> Where(DriverFilter filter)
        {
            var predicate = PredicateBuilder.True<Driver>()
                .And(x => x.Deleted == false);

            predicate = (string.IsNullOrEmpty(filter.CnhNumber))
                ? predicate
                : predicate.And(x => EF.Functions.Like(x.CnhNumber, $"%{filter.CnhNumber}%"));

            predicate = (string.IsNullOrEmpty(filter.Name))
                ? predicate
                : predicate.And(driver => EF.Functions.Like(driver.User.FullName.ToLower(), $"%{filter.Name}%"));

            predicate = (filter.PlanId == null)
                ? predicate
                : predicate.And(x => x.PlanId == filter.PlanId);
            return predicate;




            
        }
    }
}
