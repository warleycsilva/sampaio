using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class DebtPaymentRepository : Repository<DebtPayment>, IDebtPaymentRepository
    {
        public DebtPaymentRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<DebtPayment, bool>> Where(DebtPaymentFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<DebtPayment>()
                .And(x => x.Deleted == false);

            predicate = (filter.DebtId == null)
                ? predicate
                : predicate.And(x => x.DebtId == filter.DebtId);

            predicate = (filter.Status == null)
                ? predicate
                : predicate.And(x => x.Status == filter.Status);

            predicate = (filter.ExternalCode == null)
                ? predicate
                : predicate.And(x => EF.Functions.Like(x.ExternalCode.ToLower(), $"%{filter.ExternalCode.ToLower()}%"));

            return predicate;
        }
    }
}
