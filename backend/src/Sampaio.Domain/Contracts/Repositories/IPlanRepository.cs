using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Expression<Func<Plan, bool>> Where(PlanFilter filter, Guid? id = null);
    }
}
