using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.Plan
{
    public class PlanListQuery : IRequest<IEnumerable<PlanVm>>
    {
        public PlanFilter Filter { get; set; }
    }
}
