using System;
using System.Linq.Expressions;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface IPassageiroRepository : IRepository<Passageiro>
    {
        Expression<Func<Passageiro, bool>> Where(PassageiroFilter filter, Guid? id = null);
    }
}