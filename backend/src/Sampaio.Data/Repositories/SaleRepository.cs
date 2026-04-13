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
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<Sale, bool>> Where(SaleFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<Sale>()
                .And(x => x.Deleted == false);

            predicate = (filter.CommerceId == null)
                ? predicate
                : predicate.And(x => x.CommerceId == filter.CommerceId);
            
            predicate = (filter.DriverId == null)
                ? predicate
                : predicate.And(x => x.DriverId == filter.DriverId);

            predicate = (filter.Status == null)
                ? predicate
                : predicate.And(x => x.Status == filter.Status); 
            
            predicate =
                 predicate.And(x => x.DiscountValue >= 0);

            return predicate;
        }
    }
}
