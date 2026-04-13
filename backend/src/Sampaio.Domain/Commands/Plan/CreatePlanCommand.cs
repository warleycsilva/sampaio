using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Sampaio.Domain.Results.Plan;

namespace Sampaio.Domain.Commands.Plan
{
    public class CreatePlanCommand : IRequest<CreatePlanResult>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal MonthValue { get; set; }
    }
}
