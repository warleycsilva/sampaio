using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class DebtPaymentProjections
    {
        public static DebtPaymentVm ToVm(this DebtPayment debtPayment) => new DebtPaymentVm
        {
            Id = debtPayment.Id,
            Debt = debtPayment.Debt.ToVm(),
            Status = debtPayment.Status,
        };

        public static IQueryable<DebtPaymentVm> ToVm(this IQueryable<DebtPayment> query) =>
            query.Select(debtPayment => new DebtPaymentVm
            {
                Id = debtPayment.Id,
                Debt = debtPayment.Debt.ToVm(),
                Status = debtPayment.Status,
            });

        public static IEnumerable<DebtPaymentVm> ToVm(this IEnumerable<DebtPayment> query)
        {
            return query.Select(debtPayment => new DebtPaymentVm
            {
                Id = debtPayment.Id,
                Debt = debtPayment.Debt.ToVm(),
                Status = debtPayment.Status,
            });
        }
    }
}