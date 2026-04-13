using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class DebtPaymentFilter : Pagination
    {
        public Guid? DebtId { get; set; }
        public EDebtPaymentStatus? Status { get; set; }
        public string? ExternalCode { get; set; }
    }
}
