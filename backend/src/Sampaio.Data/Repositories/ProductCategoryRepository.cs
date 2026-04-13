using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Filters;
using Sampaio.Shared.Utils;

namespace Sampaio.Data.Repositories
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<ProductCategory, bool>> Where(ProductCategoryFilter filter)
        {
            var predicate = PredicateBuilder.True<ProductCategory>();

            predicate = predicate.And(x => !x.Deleted);
            
            predicate = string.IsNullOrEmpty(filter.Name)
                ? predicate
                : predicate.And(x => EF.Functions.Like(x.Name.ToLower(), $"%{filter.Name}%"));

            predicate = (filter.Description == null)
                ? predicate
                : predicate.And(x => EF.Functions.Like(x.Description.ToLower(), $"%{filter.Description}%"));
            
            return predicate;
        }
    }
}