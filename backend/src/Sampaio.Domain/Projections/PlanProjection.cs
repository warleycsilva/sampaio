using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sampaio.Domain.Entities;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class PlanProjection
    {
        public static PlanVm ToVm(this Plan plan) => new PlanVm
        {
            Id = plan.Id,
            Name = plan.Name,
            Description = plan.Description,
            MonthValue = plan.MonthValue,
            CreatedAt = plan.CreatedAt
        };

        public static IQueryable<PlanVm> ToVm(this IQueryable<Plan> query) =>
            query.Select(plan => new PlanVm
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                MonthValue = plan.MonthValue,
                CreatedAt = plan.CreatedAt
            });

        public static IEnumerable<PlanVm> ToVm(this IEnumerable<Plan> query) =>
            query.Select(plan => new PlanVm
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                MonthValue = plan.MonthValue,
                CreatedAt = plan.CreatedAt
            });

    }

}
