using System;
using System.Linq.Expressions;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Expression<Func<CartItem, bool>> Where(CartItemsFilter filter, Guid? id = null);
    }
}