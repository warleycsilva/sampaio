using System;
using System.Linq;
using System.Linq.Expressions;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class PassageiroRepository : Repository<Passageiro>, IPassageiroRepository
    {
        public PassageiroRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<Passageiro, bool>> Where(PassageiroFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<Passageiro>()
                .And(x => x.Deleted == false);
            
            predicate = filter.PassageiroId.HasValue
                ? predicate.And(x => x.Id == filter.PassageiroId)
                : predicate;
            
            predicate = filter.ViagemId.HasValue
                ? predicate.And(x => x.ViagemId == filter.ViagemId)
                : predicate;

            predicate = !string.IsNullOrEmpty(filter.Nome)
                ? predicate.And(x => x.Nome.ToLower().Contains(filter.Nome))
                : predicate;

            predicate = !string.IsNullOrEmpty(filter.Documento)
                ? predicate.And(x => x.Documento.ToLower().Contains(filter.Documento))
                : predicate;
            
            predicate = !string.IsNullOrEmpty(filter.Telefone)
                ? predicate.And(x => x.Telefone.ToLower().Contains(filter.Telefone))
                : predicate;
            
            predicate = filter.Comprador != null
                ? predicate.And(x => x.Comprador == filter.Comprador)
                : predicate;
            
            predicate = filter.Assento != null
                ? predicate.And(x => x.Assento == filter.Assento)
                : predicate;

            return predicate;
        }
    }
}