using System;

namespace Sampaio.Domain.ViewModels
{
    public class CartItemVm : BaseVm
    {
        public ProductVm Product { get; set; }
        
        public Guid? ProductId { get; set; }
        
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}