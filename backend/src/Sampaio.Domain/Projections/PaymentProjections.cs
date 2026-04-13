using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class PaymentProjections
    {
        public static PaymentVm ToVm(this Payment payment) => new PaymentVm
        {
            Id = payment.Id,
            Debt = payment.Debt.ToVm(),
            Status = payment.Status,
        };

        public static IQueryable<PaymentVm> ToVm(this IQueryable<Payment> query) =>
            query.Select(payment => new PaymentVm
            {
                Id = payment.Id,
                Debt = payment.Debt.ToVm(),
                Status = payment.Status,
            });

        public static IEnumerable<PaymentVm> ToVm(this IEnumerable<Payment> query)
        {
            return query.Select(payment => new PaymentVm
            {
                Id = payment.Id,
                Debt = payment.Debt.ToVm(),
                Status = payment.Status,
            });
        }
    }
}