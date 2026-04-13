using Sampaio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Extensions;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.ViewModels
{
    public class SaleVm : BaseVm
    {
        public decimal Value { get; set; }
        
        public decimal? DiscountValue { get; set; }
        public CommerceVm Commerce { get; set; }
        public DriverVm Driver { get; set; }
        public ESalesStatus Status { get; set; }
        public string StatusDescription { get; set; } 
        
        public string CreatedAtShort { get; set; }
    }
}
