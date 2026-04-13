using System.Collections.Generic;

namespace Sampaio.Domain.Results.Profiles
{
    public class ResetPasswordResult
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public ICollection<string> Errors { get; set; } = new List<string>();
    }
}