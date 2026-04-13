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
    public class DebtRepository : Repository<Debt>, IDebtRepository
    {
        public DebtRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<Debt, bool>> Where(DebtFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<Debt>()
                .And(x => x.Deleted == false);

            predicate = (filter.DriverId == null)
                ? predicate
                : predicate.And(x => x.DriverId == filter.DriverId);

            predicate = (filter.Status == null)
                ? predicate
                : predicate.And(x => x.Status == filter.Status);

            predicate = (filter.Type == null)
                ? predicate
                : predicate.And(x => x.Type == filter.Type);
            
            predicate = (filter.SessionUser == null) // Importante!
                ? predicate
                : predicate.And(x => x.DriverId == filter.SessionUser.Id);

            return predicate;
        }
    }
}
