using System;
using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class CartItemsProjections
    {
        public static CartItemVm ToVm(this CartItem cartItem) => new CartItemVm
        {
            Id = cartItem.Id,
            Product = cartItem?.Product?.ToVm(),
            ProductId = cartItem?.ProductId,
            ProductName = cartItem.Product?.Name,
            Quantity = cartItem.Quantity
        };

        public static IQueryable<CartItemVm> ToVm(this IQueryable<CartItem> query) =>
            query.Select(cartItem => new CartItemVm
            {
                Id = cartItem.Id,
                Product = cartItem.Product != null ? cartItem.Product.ToVm() : null,
                ProductId = cartItem != null ? cartItem.ProductId : (Guid?) null,
                ProductName = cartItem.Product != null ? cartItem.Product.Name : null,
                Quantity = cartItem.Quantity
            });

        public static IEnumerable<CartItemVm> ToVm(this IEnumerable<CartItem> query)
        {
            return query.Select(cartItem => new CartItemVm
            {
                Id = cartItem.Id,
                Product = cartItem?.Product?.ToVm(),
                ProductId = cartItem?.ProductId,
                ProductName = cartItem.Product?.Name,
                Quantity = cartItem.Quantity
            });
        }
    }
}