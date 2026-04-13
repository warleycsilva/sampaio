using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context)
            : base(context)
        {
        }

        public Expression<Func<User, bool>> Where(UserFilter filter)
        {
            var predicate = PredicateBuilder.True<User>();
            
            predicate = predicate.And(x => !x.Deleted);
            
            predicate = string.IsNullOrWhiteSpace(filter.Name)
                ? predicate
                : predicate.And(x => EF.Functions.Like((x.FirstName + " " + x.LastName).ToLower(), $"%{filter.Name.ToLower()}%"));

            predicate = string.IsNullOrWhiteSpace(filter.Email)
                ? predicate
                : predicate.And(x => x.Email.ToLower() == filter.Email.ToLower());

            predicate = !filter.Active.HasValue
                ? predicate
                : predicate.And(x => x.Active == filter.Active);
            
            return predicate;
        }
        
        public async Task<User> FindByEmailAsync(string email) => 
            await _context.Set<User>().FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

        public async Task<T> AddUserAsync<T>(T user)
        {
            await _context.AddAsync(user);
            return user;
        }
    }
}