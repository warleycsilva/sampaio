using System.Collections.Generic;
using Sampaio.Domain.Entities;

namespace Sampaio.Domain.ViewModels
{
    public class CartVm : BaseVm
    {
        public decimal Value { get; set; }
        
        public decimal? DiscountValue { get; set; }
        
        public CommerceVm? Commerce { get; set; }
        
        public DriverVm? Driver { get; set; }
        public IEnumerable<CartItemVm> Items { get; set; }

    }
}