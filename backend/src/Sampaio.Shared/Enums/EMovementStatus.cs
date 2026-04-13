using System.ComponentModel;

namespace Sampaio.Shared.Enums
{
    public enum EMovementStatus
    {
        [Description("A receber")]
        ToReceive,
        [Description("Parcialmente Recebido")]
        PartiallyReceived,
        [Description("Recebido")]
        Received
    }
}