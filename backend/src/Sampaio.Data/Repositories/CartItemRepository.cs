using System;
using System.Linq.Expressions;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<CartItem, bool>> Where(CartItemsFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<CartItem>()
                .And(x => x.Deleted == false);
            
            predicate = (filter.CartId == null)
                ? predicate
                : predicate.And(x => x.CartId == filter.CartId);

            predicate = (filter.ProductId == null)
                ? predicate
                : predicate.And(x => x.ProductId == filter.ProductId);

            return predicate;
        }
    }
}