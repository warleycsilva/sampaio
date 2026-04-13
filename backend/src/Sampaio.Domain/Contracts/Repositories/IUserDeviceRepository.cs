using System;
using System.Linq.Expressions;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface IUserDeviceRepository : IRepository<UserDevice>
    {
        Expression<Func<UserDevice, bool>> Where(UserDeviceFilter filter);
    }
}