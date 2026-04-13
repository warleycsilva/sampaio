using System;
using System.Linq.Expressions;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Expression<Func<Payment, bool>> Where(PaymentFilter filter, Guid? id = null);
    }
}