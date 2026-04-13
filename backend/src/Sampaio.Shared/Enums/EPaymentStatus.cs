using System.ComponentModel;

namespace Sampaio.Shared.Enums
{
    public enum EPaymentStatus
    {
        [Description("Aprovado")]Approved,
        [Description("Reprovado")]Canceled,
        [Description("Reprovado")]Failed,
        [Description("Aguardando")]Pending,
        [Description("Reembolsado")]Refunded
    }
}