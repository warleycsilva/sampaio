using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Sampaio.Domain.Results.DebtPayment;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Commands.DebtPayment
{
    public class CreateDebtPaymentCommand : IRequest<CreateDebtPaymentResult>
    {
        [Required]
        public EDebtPaymentStatus Status { get; set; }

        [Required]
        public Guid DebtId { get; set; }

        [Required]
        public string ExternalCode { get; set; }
    }
}
