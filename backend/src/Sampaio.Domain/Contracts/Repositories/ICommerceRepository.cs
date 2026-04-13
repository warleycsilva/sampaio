using System;
using System.Linq.Expressions;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface ICommerceRepository : IRepository<Commerce>
    {
        Expression<Func<Commerce, bool>> Where(CommerceFilter filter);
    }
}