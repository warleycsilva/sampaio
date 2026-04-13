using System.ComponentModel;

namespace Sampaio.Shared.Enums
{
    public enum EMovementType
    {
        [Description("Locação")]
        Rental,
        [Description("Reparação")]
        Repair,
        [Description("Multa")]
        Penalty,
        [Description("Outros")]
        Others
    }
}