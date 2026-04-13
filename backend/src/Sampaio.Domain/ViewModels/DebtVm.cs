using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.Extensions;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.ViewModels
{
    public class DebtVm : BaseVm
    {
        public decimal Value { get; set; }
        public DriverVm Driver { get; set; }
        public EDebtStatus Status { get; set; }
        public EDebtType Type { get; set; }

        public string StatusDescription { get; set; } 
        public string TypeDescription { get; set; }
        public PlanVm? Plan { get; set; }
        public Guid? PlanId { get; set; }
        public string? QrCode { get; set; }
        public string? QrCodeImage { get; set; }
    }
}
