using System;
using System.ComponentModel;

namespace Sampaio.Shared.Enums
{
    [Serializable]
    public enum EContractType
    {
        [Description("Recorrente")]Recurring,
        [Description("Avulso")]Detached
    }
}