using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Expression<Func<User, bool>> Where(UserFilter filter);
        Task<T> AddUserAsync<T>(T user);
    }
}