using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sampaio.Shared.Enums
{
    public enum EDebtType
    {
        [Description("Novo")]
        New,
        [Description("Pagamento de plano")]
        Plan,
    }
}
