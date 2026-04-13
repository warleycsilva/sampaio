using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Projections
{
    public static class UserProjections
    {
        public static UserVm ToVm(this User user) => new UserVm
        {
            Active = user.Active,
            CreatedAt = user.CreatedAt,
            Avatar = user.AvatarUrl,
            Email = user.Email,
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Deleted = user.Deleted,
            Phones = user.Phones == null ? null:user.Phones
        };

        public static IQueryable<UserVm> ToVm(this IQueryable<User> query) => query.Select(user => new UserVm
        {
            Active = user.Active,
            CreatedAt = user.CreatedAt,
            Avatar = user.AvatarUrl,
            Email = user.Email,
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Deleted = user.Deleted,
            Phones = user.Phones == null ? null : user.Phones
        });

        public static IEnumerable<UserVm> ToVm(this IEnumerable<User> query) => query.Select(user => new UserVm
        {
            Active = user.Active,
            CreatedAt = user.CreatedAt,
            Avatar = user.AvatarUrl,
            Email = user.Email,
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Deleted = user.Deleted,
            Phones = user.Phones == null ? null : user.Phones
        });

        public static SessionUser ToSessionUser(this User user)
        {
            var sessionUser = new SessionUser
            {
                AvatarUrl = user.AvatarUrl,
                Email = user.Email,
                Id = user.Id,
                Type = user.Driver != null ? EUserType.Driver : user.Commerce != null ? EUserType.Commerce : EUserType.Backoffice,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsSysAdmin = user.BackofficeUser != null ? true : false
            };
            return sessionUser;
        }
    }
}
