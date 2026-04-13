using System.ComponentModel;

namespace Sampaio.Shared.Enums
{
    public enum EContractStatus
    {
        [Description("Em dia")]
        UpToDate,
        [Description("Em Débito")]
        Debt,
        [Description("Pendente")]
        Pending,
        [Description("Rejeitado")]
        Rejected,
        [Description("Encerrado")]
        Terminated
    }
}