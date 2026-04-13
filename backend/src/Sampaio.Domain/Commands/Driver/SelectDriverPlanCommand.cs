using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Driver;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Commands.Driver
{
    public class SelectDriverPlanCommand : IRequest<SelectDriverPlanResult>
    {
        [Required]
        public Guid PlanId { get; set; }
        
        [JsonIgnore] 
        public SessionUser SessionUser { get; set; }
    }
}