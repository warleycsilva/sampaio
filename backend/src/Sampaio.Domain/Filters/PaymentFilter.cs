using System;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class PaymentFilter : Pagination
    {
        public Guid? DebtId { get; set; }
        public EPaymentStatus? Status { get; set; }
    }
}