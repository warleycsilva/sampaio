using System;
using System.Linq.Expressions;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Expression<Func<Cart, bool>> Where(CartFilter filter, Guid? id = null);
    }
}