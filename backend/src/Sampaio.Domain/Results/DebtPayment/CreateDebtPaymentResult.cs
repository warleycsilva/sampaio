using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.Results.DebtPayment
{
    public class CreateDebtPaymentResult
    {
        public Guid DebtPaymentId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
