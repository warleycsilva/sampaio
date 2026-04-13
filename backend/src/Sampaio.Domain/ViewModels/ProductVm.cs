using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.ViewModels
{
    public class ProductVm : BaseVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? PriceFormatted { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string? DiscountPriceFormatted { get; set; }
        public EProductType Type { get; set; }
        public ProductCategoryVm ProductCategory { get; set; }
        
        public string ProductUrl { get; set; }
    }
}
