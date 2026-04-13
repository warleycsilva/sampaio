using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Payment;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Commands.Payment
{
    public class UpdatePaymentCommand : IRequest<UpdatePaymentResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        
        public Guid? DebtId { get; set; }
        public EPaymentStatus? Status { get; set; }
    }
}