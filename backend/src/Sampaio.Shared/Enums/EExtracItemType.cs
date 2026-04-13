using System.ComponentModel;

namespace Sampaio.Shared.Enums
{
    public enum EExtracItemType
    {
        [Description("Débito")]Debit,
        [Description("Crédito")]Credit,
        [Description("Em Análise")]Future,
        [Description("Recusado")]Declined
    }
}