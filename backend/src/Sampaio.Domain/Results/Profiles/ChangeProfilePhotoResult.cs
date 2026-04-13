using System;

namespace Sampaio.Domain.Results.Profiles
{
    public class ChangeProfilePhotoResult
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public string NewAvatarUrl { get; set; }
    }
}