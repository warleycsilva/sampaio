using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class CartProjections
    {
        public static CartVm ToVm(this Cart cart) => new CartVm
        {
            Id = cart.Id,
            Value = cart.Value,
            DiscountValue = cart?.DiscountValue,
            Items = cart.CartItems?.ToVm(),
        };

        public static IQueryable<CartVm> ToVm(this IQueryable<Cart> query) =>
            query.Select(cart => new CartVm
            {
                Id = cart.Id,
               Value = cart.Value,
               DiscountValue = cart.DiscountValue,
               Items = cart.CartItems == null ? null : cart.CartItems.ToVm(),
            });

        public static IEnumerable<CartVm> ToVm(this IEnumerable<Cart> query)
        {
            return query.Select(cart => new CartVm
            {
                Id = cart.Id,
                Value = cart.Value,
                DiscountValue = cart?.DiscountValue,
                Items = cart.CartItems?.ToVm(),
            });
        }
    }
}