using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class SaleItemProjections
    {
        public static SaleItemVm ToVm(this SaleItem saleItem) => new SaleItemVm
        {
            Id = saleItem.Id,
            Sale = saleItem.Sale.ToVm(),
            Product = saleItem.Product.ToVm(),
            Quantity = saleItem.Quantity,
        };

        public static IQueryable<SaleItemVm> ToVm(this IQueryable<SaleItem> query) =>
            query.Select(saleItem => new SaleItemVm
            {
                Id = saleItem.Id,
                Sale = saleItem.Sale.ToVm(),
                Product = saleItem.Product.ToVm(),
                Quantity = saleItem.Quantity,
            });

        public static IEnumerable<SaleItemVm> ToVm(this IEnumerable<SaleItem> query)
        {
            return query.Select(saleItem => new SaleItemVm
            {
                Id = saleItem.Id,
                Sale = saleItem.Sale.ToVm(),
                Product = saleItem.Product.ToVm(),
                Quantity = saleItem.Quantity,
            });
        }
    }
}
