using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.Results.Driver
{
    public class BaseDriverResult
    {
        public bool Success { get; set; } = false; //Alterar caso necessário
        public string Message { get; set; }
    }
}
