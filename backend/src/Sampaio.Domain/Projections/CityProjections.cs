using System.Collections.Generic;
using System.Linq;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class CityProjections
    {
        public static CityVm ToVm(this City city) => new CityVm
        {
            Id = city.Id,
            Name = city.Name,
            State = city?.State?.ToVm() ?? null
        };

        public static IQueryable<CityVm> ToVm(this IQueryable<City> query)
            => query.Select(city
                => new CityVm
                {
                    Id = city.Id,
                    Name = city.Name,
                    State = city.State != null ? city.State.ToVm():null
                });
        public static IEnumerable<CityVm> ToVm(this IEnumerable<City> query)
            => query.Select(city
                => new CityVm
                {
                    Id = city.Id,
                    Name = city.Name,
                    State = city.State?.ToVm()
                });
    }
}