using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Sampaio.Domain.Results.Debt;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Commands.Debt
{
    public class CreateDebtCommand : IRequest<CreateDebtResult>
    {
        [Required]
        public decimal Value { get; set; }

        [Required]
        public Guid DriverId { get; set; }

        [Required]
        public EDebtStatus Status { get; set; }

        [Required]
        public EDebtType Type { get; set; }
        
        public Guid? PlanId { get; set; }
    }
}
