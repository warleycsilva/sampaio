using System;
using System.Linq;
using System.Linq.Expressions;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class ViagemRepository : Repository<Viagem>, IViagemRepository
    {
        public ViagemRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<Viagem, bool>> Where(ViagemFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<Viagem>()
                .And(x => x.Deleted == false);
            
            predicate = filter.ViagemId.HasValue
                ? predicate.And(x => x.Id == filter.ViagemId)
                : predicate;
            
            predicate = id.HasValue
                ? predicate.And(x => x.Id == id)
                : predicate;

            predicate = filter.DataPartida != null
                ? predicate.And(x => x.DataPartida.Date >= filter.DataPartida.Value.Date)
                : predicate;

            var origem = filter.Origem?.ToLower().Trim();
            predicate = !string.IsNullOrEmpty(filter.Origem)
                ? predicate.And(x => x.Origem.ToLower().Contains(origem))
                : predicate;

            var destino = filter.Destino?.ToLower().Trim();
            predicate = !string.IsNullOrEmpty(filter.Destino)
                ? predicate.And(x => x.Destino.ToLower().Contains(destino))
                : predicate;

            var email = filter.Email?.ToLower().Trim();
            predicate = !string.IsNullOrEmpty(filter.Email)
                ? predicate.And(x => x.ViagemPagamentos.Any(vp => vp.Email.ToLower().Contains(email)))
                : predicate;
            var cpf = filter.Cpf?.Replace(".", "").Replace("-", "")?.Trim();
            predicate = cpf != null
                ? predicate.And(x => x.ViagemPagamentos.Any(vp => vp.Cpf == cpf))
                : predicate;
            
            predicate = filter.IsActive != null
                ? predicate.And(x => x.IsActive == filter.IsActive)
                : predicate;
            
            return predicate;
        }
    }
}