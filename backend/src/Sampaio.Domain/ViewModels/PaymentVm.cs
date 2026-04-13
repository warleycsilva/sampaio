using Sampaio.Shared.Enums;

namespace Sampaio.Domain.ViewModels
{
    public class PaymentVm : BaseVm
    {
        public DebtVm Debt { get; set; }
        
        public EPaymentStatus Status { get; set; }
    }
}