using Sampaio.Shared.Enums;

namespace Sampaio.Domain.ViewModels
{
    public class DebtPaymentVm : BaseVm
    {
        public DebtVm Debt { get; set; }
        public EDebtPaymentStatus Status { get; set; }
        public string ExternalCode { get; set; }
    }
}