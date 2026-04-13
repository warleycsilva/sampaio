using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.ViewModels
{
    public class SaleItemVm : BaseVm
    {
        public SaleVm Sale { get; set; }
        public ProductVm Product { get; set; }
        public int Quantity { get; set; }
    }
}
