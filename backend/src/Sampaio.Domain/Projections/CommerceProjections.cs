using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class CommerceProjections
    {
        public static CommerceVm ToVm(this Commerce commerce) => new CommerceVm
        {
            Id = commerce.Id,
            Name = commerce.Name,
            Homologated = commerce.Homologated,
            Identification = commerce.Identification,
            User = commerce?.User?.ToVm(),
            Latitude = commerce.Latitude,
            Longitude = commerce.Longitude,
            AddressInformation = commerce.AddressInformation == null ? null : commerce?.AddressInformation?.ToVm(),
            CommerceSegment = commerce.Segment == null ? null : commerce?.Segment?.ToVm(),
            HasParking = commerce.HasParking,
            CreatedAt = commerce.CreatedAt,
            Products = commerce.Products != null ? commerce.Products.ToVm() : null
        };

        public static IQueryable<CommerceVm> ToVm(this IQueryable<Commerce> query)
            => query.Select(commerce
                => new CommerceVm
                {
                    Id = commerce.Id,
                    Name = commerce.Name,
                    Homologated = commerce.Homologated,
                    Identification = commerce.Identification,
                    User = commerce.User == null ? null : commerce.User.ToVm(),
                    Latitude = commerce.Latitude,
                    Longitude = commerce.Longitude,
                    AddressInformation = commerce.AddressInformation == null ? null : commerce.AddressInformation.ToVm(),
                    CommerceSegment = commerce.Segment == null ? null : commerce.Segment.ToVm(),
                    HasParking = commerce.HasParking,
                    CreatedAt = commerce.CreatedAt,
                    Products = commerce.Products != null ? commerce.Products.ToVm() : null
                });
        public static IEnumerable<CommerceVm> ToVm(this IEnumerable<Commerce> query)
            => query.Select(commerce
                => new CommerceVm
                {
                    Id = commerce.Id,
                    Name = commerce.Name,
                    Homologated = commerce.Homologated,
                    Identification = commerce.Identification,
                    User = commerce?.User?.ToVm(),
                    Latitude = commerce.Latitude,
                    Longitude = commerce.Longitude,
                    AddressInformation = commerce.AddressInformation == null ? null : commerce?.AddressInformation?.ToVm(),
                    CommerceSegment = commerce.Segment == null ? null : commerce?.Segment?.ToVm(),
                    HasParking = commerce.HasParking,
                    CreatedAt = commerce.CreatedAt,
                    Products = commerce.Products != null ? commerce.Products.ToVm() : null
                });
    }
}