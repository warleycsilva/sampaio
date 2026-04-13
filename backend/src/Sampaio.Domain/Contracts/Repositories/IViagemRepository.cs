using System;
using System.Linq.Expressions;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface IViagemRepository : IRepository<Viagem>
    {
        Expression<Func<Viagem, bool>> Where(ViagemFilter filter, Guid? id = null);
    }
}