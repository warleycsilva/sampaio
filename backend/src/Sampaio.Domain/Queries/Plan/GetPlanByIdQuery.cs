using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.Plan
{
    public class GetPlanByIdQuery : IRequest<PlanVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
