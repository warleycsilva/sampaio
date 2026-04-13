using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.Results.Plan
{
    public class CreatePlanResult
    {
        public Guid PlanId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
