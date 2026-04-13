using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class ProductCategoryProjections
    {
        public static ProductCategoryVm ToVm(this ProductCategory productCategory) => new ProductCategoryVm
        {
            Id = productCategory.Id,
            Name = productCategory.Name,
            Description = productCategory.Description,
        };

        public static IQueryable<ProductCategoryVm> ToVm(this IQueryable<ProductCategory> query) => query.Select(
            productCategory => new ProductCategoryVm
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
            });
        
        public static IEnumerable<ProductCategoryVm> ToVm(this IEnumerable<ProductCategory> query) => query.Select(
            productCategory => new ProductCategoryVm
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
            });
    }
}