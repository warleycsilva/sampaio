using System;
using System.Collections.Generic;
using System.Text;

namespace Sampaio.Domain.Results.Driver
{
    public class DriverSignUpResult
    {
        public string Error { get; set; }

        public bool Success { get; set; } = false;

        public object User { get; set; }

        public string AccessToken { get; set; }

        public string Message { get; set; }
    }
}
