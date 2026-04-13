using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.Plan;

namespace Sampaio.Domain.Commands.Plan
{
    public class DeletePlanCommand : IRequest<DeletePlanResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
