using Sampaio.Shared.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class DriverProjection
    {
        public static DriverVm ToVm(this Driver driver) => new DriverVm
        {
            Id = driver.Id,
            BirthDate = driver.Birthdate,
            Identification = driver.Identification,
            User = driver.User.ToVm(),
            Plan = driver.Plan == null ? null : driver?.Plan?.ToVm(),
            CnhNumber = driver.CnhNumber,
            CreatedAt = driver.CreatedAt,
        };

        public static IQueryable<DriverVm> ToVm(this IQueryable<Driver> query)
            => query.Select(driver => new DriverVm
            {
                Id = driver.Id,
                BirthDate = driver.Birthdate,
                Identification = driver.Identification,
                User = driver.User.ToVm(),
                Plan = driver.Plan == null ? null : driver.Plan.ToVm(),
                CnhNumber = driver.CnhNumber,
                CreatedAt = driver.CreatedAt,
            });

        public static IEnumerable<DriverVm> ToVm(this IEnumerable<Driver> query)
            => query.Select(driver => new DriverVm
            {
                Id = driver.Id,
                BirthDate = driver.Birthdate,
                Identification = driver.Identification,
                User = driver.User.ToVm(),
                Plan = driver.Plan == null ? null : driver?.Plan?.ToVm(),
                CnhNumber = driver.CnhNumber,
                CreatedAt = driver.CreatedAt,
            });
    }
}
