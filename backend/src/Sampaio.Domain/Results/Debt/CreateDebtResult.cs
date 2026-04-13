using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.Results.Debt
{
    public class CreateDebtResult
    {
        public Guid DebtId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
