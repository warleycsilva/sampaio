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
    public class SaleItemRepository : Repository<SaleItem>, ISaleItemRepository
    {
        public SaleItemRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<SaleItem, bool>> Where(SaleItemFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<SaleItem>()
                .And(x => x.Deleted == false);

            predicate = (filter.SaleId == null)
                ? predicate
                : predicate.And(x => x.SaleId == filter.SaleId);

            predicate = (filter.ProductId == null)
                ? predicate
                : predicate.And(x => x.ProductId == filter.ProductId);

            return predicate;
        }
    }
}
