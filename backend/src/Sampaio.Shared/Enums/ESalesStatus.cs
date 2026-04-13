using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sampaio.Shared.Enums
{
    public enum ESalesStatus
    {
        [Description("Novo")]
        New,
        [Description("Completa")]
        Complete,
        [Description("Finalizada")]
        Finalized,
    }
}
