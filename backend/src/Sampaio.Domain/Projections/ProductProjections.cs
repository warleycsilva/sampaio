using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class ProductProjections
    {
        public static ProductVm ToVm(this Product product) => new ProductVm
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DiscountPrice = product?.DiscountPrice,
            Type = product.Type,
            ProductCategory = product?.ProductCategory?.ToVm(),
            ProductUrl = product?.ProductUrl,
        };

        public static IQueryable<ProductVm> ToVm(this IQueryable<Product> query) =>
            query.Select(product => new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                Type = product.Type,
                ProductCategory = product.ProductCategory != null ? product.ProductCategory.ToVm() : null,
                ProductUrl = product.ProductUrl,

            });

        public static IEnumerable<ProductVm> ToVm(this IEnumerable<Product> query)
        {
            return query.Select(product => new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPrice = product?.DiscountPrice,
                Type = product.Type,
                ProductCategory = product?.ProductCategory?.ToVm(),
                ProductUrl = product?.ProductUrl,
            });
        }
    }
}
