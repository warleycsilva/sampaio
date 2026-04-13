using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.Results.Plan
{
    public class DeletePlanResult
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}
