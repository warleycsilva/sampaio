using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.Plan;

namespace Sampaio.Domain.Commands.Plan
{
    public class UpdatePlanCommand : IRequest<UpdatePlanResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? MonthValue { get; set; }
    }
}
