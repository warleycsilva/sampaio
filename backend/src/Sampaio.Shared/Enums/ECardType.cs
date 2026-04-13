using System.ComponentModel;

namespace Sampaio.Shared.Enums
{
    public enum ECardType
    {
        [Description("Cartão de crédito")]
        Credit,
        [Description("Cartão de débito")]
        Debit
    }
}