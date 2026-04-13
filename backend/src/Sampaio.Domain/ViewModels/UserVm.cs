using System.Collections.Generic;
using Sampaio.Domain.Entities;

namespace Sampaio.Domain.ViewModels
{
    public class UserVm  : BaseVm
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public bool Active { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();

        public ICollection<UserPhone> Phones { get; set; } = new List<UserPhone>();
    }
}
