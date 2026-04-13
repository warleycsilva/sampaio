using System;
using System.Linq.Expressions;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class UserDeviceRepository: Repository<UserDevice>, IUserDeviceRepository
    {
        public UserDeviceRepository(DataContext context)
            : base(context)
        {
        }

        public Expression<Func<UserDevice, bool>> Where(UserDeviceFilter filter)
        {
            var predicate = PredicateBuilder.True<UserDevice>();
            
            predicate = predicate.And(x => !x.Deleted);
            
            predicate = string.IsNullOrWhiteSpace(filter.DeviceId)
                ? predicate
                : predicate.And(x => x.DeviceId == filter.DeviceId);
            
            predicate = (filter.UserId == null)
                ? predicate
                : predicate.And(x => x.UserId == filter.UserId);
            
            return predicate;
        }
    }
}