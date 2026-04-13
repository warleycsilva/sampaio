// using System.Collections.Generic;
// using System.Linq;
// using Sampaio.Domain.Entities;
//
// namespace Sampaio.Domain.Projections
// {
//     public static class CartToSaleProjections
//     {
//         public static Sale ToSale(this Cart cart) => new Sale
//         {
//             Id = cart.Id,
//         };
//
//         public static IQueryable<Sale> ToVm(this IQueryable<Cart> query) =>
//             query.Select(cart => new Sale
//             {
//                 Id = cart.Id,
//             });
//
//         public static IEnumerable<Sale> ToVm(this IEnumerable<Cart> query)
//         {
//             return query.Select(cart => new Sale
//             {
//                 Id = cart.Id,
//             });
//         }
//
//
//     }
// }