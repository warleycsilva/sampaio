using System.Collections.Generic;
using System.Security.Claims;
using Newtonsoft.Json;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Results.Signup
{
    public class SignupResult
    {
        public string Message { get; set; }

        public string Error { get; set; }

        public bool Success { get; set; } = false;
    }
}
