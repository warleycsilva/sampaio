using System;
using System.Linq.Expressions;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<Cart, bool>> Where(CartFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<Cart>()
                .And(x => x.Deleted == false);
            
            predicate = (filter.CommerceId == null)
                ? predicate
                : predicate.And(x => x.CommerceId == filter.CommerceId);
            
            predicate = (filter.DriverId == null)
                ? predicate
                : predicate.And(x => x.DriverId == filter.DriverId);

            return predicate;
        }
    }
}