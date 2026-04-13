using System.ComponentModel;

namespace Sampaio.Shared.Enums
{
    public enum EContractRecurringModel
    {
        [Description("Diário")]
        Daily,
        [Description("Semanal")]
        Weekly,
        [Description("Mensal")]
        Monthly
    }
}