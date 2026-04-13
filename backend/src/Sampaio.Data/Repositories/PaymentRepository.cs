using System;
using System.Linq.Expressions;
using Sampaio.Shared.Persistence;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<Payment, bool>> Where(PaymentFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<Payment>()
                .And(x => x.Deleted == false);

            predicate = (filter.DebtId == null)
                ? predicate
                : predicate.And(x => x.DebtId == filter.DebtId);

            predicate = (filter.Status == null)
                ? predicate
                : predicate.And(x => x.Status == filter.Status);

            return predicate;
        }
    }
}