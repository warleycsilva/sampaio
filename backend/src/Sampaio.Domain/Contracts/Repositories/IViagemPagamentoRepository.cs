using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface IViagemPagamentoRepository : IRepository<ViagemPagamento>
    {
        Expression<Func<ViagemPagamento, bool>> Where(ViagemPagamentoFilter filter, Guid? id = null);
    }
}