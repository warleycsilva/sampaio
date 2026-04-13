using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<Product, bool>> Where(ProductFilter filter, Guid? id = null)
        {
            var predicate = PredicateBuilder.True<Product>()
                .And(x => x.Deleted == false);

            predicate = (!filter.CommerceId.HasValue && filter.GetAll == false)
                ? predicate
                : predicate.And(x => x.CommerceId == filter.CommerceId);

            predicate = (string.IsNullOrEmpty(filter.Name))
                ? predicate
                : predicate.And(x => EF.Functions.Like(x.Name.ToLower(), $"%{filter.Name.ToLower()}%"))
                    .Or(x => EF.Functions.Like(x.Description.ToLower(), $"%{filter.Name.ToLower()}%"));

            predicate = (filter.Type == null)
                ? predicate
                : predicate.And(x => x.Type == filter.Type);

            return predicate;
        }
    }
}
