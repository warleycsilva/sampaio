using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class CommerceRepository: Repository<Commerce>, ICommerceRepository
    {
        public CommerceRepository(DataContext context)
            : base(context)
        {
        }

        public Expression<Func<Commerce, bool>> Where(CommerceFilter filter)
        {
            var predicate = PredicateBuilder.True<Commerce>();
            
            predicate = string.IsNullOrWhiteSpace(filter.Name)
                ? predicate
                : predicate.And(x => EF.Functions.Like((x.User.FirstName).ToLower(), $"%{filter.Name.ToLower()}%"))
                    .Or(x => EF.Functions.Like((x.AddressInformation.Address.ToLower()), $"%{filter.Name.ToLower()}%"))
                    .Or(x => EF.Functions.Like((x.AddressInformation.Neighborhood.ToLower()), $"%{filter.Name.ToLower()}%"))
                    .Or(x => EF.Functions.Like((x.AddressInformation.City.Name.ToLower()), $"%{filter.Name.ToLower()}%"));

            predicate = filter.UserType != EUserType.Driver
                ? predicate
                : predicate.And(x => x.Products.Any());

            predicate = predicate.And(x => !x.Deleted);
            return predicate;
        }   
    }
}