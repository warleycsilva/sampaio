using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Sampaio.Domain.Results.Payment;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Commands.Payment
{
    public class CreatePaymentCommand : IRequest<CreatePaymentResult>
    {
        [Required]
        public Guid DebtId { get; set; }
        
        [Required]
        public EPaymentStatus Status { get; set; }
    }
}