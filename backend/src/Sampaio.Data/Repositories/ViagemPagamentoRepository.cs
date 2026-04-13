using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class ViagemPagamentoRepository : Repository<ViagemPagamento>, IViagemPagamentoRepository
    {
        public ViagemPagamentoRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<ViagemPagamento, bool>> Where(ViagemPagamentoFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<ViagemPagamento>()
                .And(x => x.Deleted == false);
            
            
            predicate = !string.IsNullOrEmpty(filter.Cpf.ToString())
                ? predicate.And(x => x.Cpf.ToString().Contains(filter.Cpf.ToString()))
                : predicate;
            
            predicate = !string.IsNullOrEmpty(filter.Email)
                ? predicate.And(x => x.Email.ToLower().Contains(filter.Email))
                : predicate;
            
            predicate = filter.Id.HasValue
                ? predicate.And(x => x.Id == filter.Id)
                : predicate;
            
            predicate = filter.ViagemId.HasValue
                ? predicate.And(x => x.ViagemId == filter.ViagemId)
                : predicate;
            
            predicate = filter.DataDe != null
                ? predicate.And(x => x.CreatedAt.Date >= filter.DataDe.Value.Date)
                : predicate;
            
            predicate = filter.DataAte != null
                ? predicate.And(x => x.CreatedAt.Date <= filter.DataAte.Value.Date)
                : predicate;

            predicate = filter.Status != null
                ? predicate.And(x => x.PagamentoStatus == filter.Status)
                : predicate;

            predicate = !string.IsNullOrEmpty(filter.IdTransacao)
                ? predicate.And(x => x.IdTransacao.ToLower().Contains(filter.IdTransacao))
                : predicate;

            return predicate;
        }
    }
}