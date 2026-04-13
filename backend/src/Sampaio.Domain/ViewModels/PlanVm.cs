using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.ViewModels
{
    public class PlanVm : BaseVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MonthValue { get; set; }
    }
}
