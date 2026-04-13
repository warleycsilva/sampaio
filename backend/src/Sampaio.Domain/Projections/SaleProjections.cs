using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sampaio.Shared.Extensions;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class SaleProjections
    {
        public static SaleVm ToVm(this Sale sale) => new SaleVm
        {
            Id = sale.Id,
            Value = sale.Value,
            DiscountValue = sale?.DiscountValue,
            Commerce = sale?.Commerce?.ToVm(),
            Driver = sale?.Driver?.ToVm(),
            Status = sale.Status,
            StatusDescription = sale.Status.Description(),
            CreatedAt = sale.CreatedAt.AddHours(-3),
            CreatedAtShort = sale?.CreatedAt.AddHours(-3).ToString("dd/MM/yyyy HH:mm")
        };

        public static IQueryable<SaleVm> ToVm(this IQueryable<Sale> query) =>
            query.Select(sale => new SaleVm
            {
                Id = sale.Id,
                Value = sale.Value,
                DiscountValue = sale.DiscountValue,
                Commerce = sale.Commerce != null ? sale.Commerce.ToVm() : null,
                Driver = sale.Driver != null ? sale.Driver.ToVm() : null,
                Status = sale.Status,
                StatusDescription = sale.Status.Description(),
                CreatedAt = sale.CreatedAt.AddHours(-3),
                CreatedAtShort = sale.CreatedAt.AddHours(-3).ToString("dd/MM/yyyy HH:mm")

            });

        public static IEnumerable<SaleVm> ToVm(this IEnumerable<Sale> query)
        {
            return query.Select(sale => new SaleVm
            {
                Id = sale.Id,
                Value = sale.Value,
                DiscountValue = sale?.DiscountValue,
                Commerce = sale?.Commerce?.ToVm(),
                Driver = sale?.Driver?.ToVm(),
                Status = sale.Status,
                StatusDescription = sale.Status.Description(),
                CreatedAt = sale.CreatedAt.AddHours(-3),
                CreatedAtShort = sale?.CreatedAt.AddHours(-3).ToString("dd/MM/yyyy HH:mm")
            });
        }


    }
}
