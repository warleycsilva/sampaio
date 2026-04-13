using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface IDebtPaymentRepository : IRepository<DebtPayment>
    {
        Expression<Func<DebtPayment, bool>> Where(DebtPaymentFilter filter, Guid? id = null);
    }
}
