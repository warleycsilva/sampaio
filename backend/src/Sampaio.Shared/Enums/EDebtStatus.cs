using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sampaio.Shared.Enums
{
    public enum EDebtStatus
    {
        [Description("Novo")]
        New,
        [Description("Pendente")]
        Pending,
        [Description("Pago")]
        Paid,
        [Description("Cancelado")]
        Cancelled,
    }
}
