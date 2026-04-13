using System.Collections.Generic;

namespace Sampaio.Domain.ViewModels
{
    public class DriverExtractVm : BaseVm
    {
        public List<SaleVm> Sales { get; set; } = new List<SaleVm>();

        public string MonthTotal { get; set; }

        public string Total { get; set; }
    }
}