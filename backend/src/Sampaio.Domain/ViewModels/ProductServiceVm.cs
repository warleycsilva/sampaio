using System;
using Sampaio.Domain.Entities;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.ViewModels
{
    public class ProductServiceVm : BaseVm
    {
        public EProductServiceType Type { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal OriginalPrice { get; set; }
        
        public decimal PromotionalPrice { get; set; }
        
        public EstablishmentVm Establishment { get; set; }
        
        public Guid EstablishmentId { get; set; }
        
        public bool IsActive { get; set; }
    }
}