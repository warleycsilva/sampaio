using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.Results.Debt
{
    public class DeleteDebtResult
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}
