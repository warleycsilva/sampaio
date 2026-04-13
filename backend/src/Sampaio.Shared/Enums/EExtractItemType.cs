using System.ComponentModel;

namespace Sampaio.Shared.Enums
{
    public enum EExtractItemType
    {
        [Description("Debito")]Debit,
        [Description("Credito")]Credit,
        [Description("Pendente")]Pending
    }
}