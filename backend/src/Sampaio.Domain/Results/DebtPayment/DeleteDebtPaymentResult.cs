using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.Results.DebtPayment
{
    public class DeleteDebtPaymentResult
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}
