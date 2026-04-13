using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.DebtPayment;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Commands.DebtPayment
{
    public class UpdateDebtPaymentCommand : IRequest<UpdateDebtPaymentResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public Guid DebtId { get; set; }
        public EDebtPaymentStatus? Status { get; set; }
        public string? ExternalCode { get; set; }
    }
}
