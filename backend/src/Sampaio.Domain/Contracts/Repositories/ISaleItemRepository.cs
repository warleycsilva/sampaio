using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface ISaleItemRepository : IRepository<SaleItem>
    {
        Expression<Func<SaleItem, bool>> Where(SaleItemFilter filter, Guid? id = null);
    }
}
