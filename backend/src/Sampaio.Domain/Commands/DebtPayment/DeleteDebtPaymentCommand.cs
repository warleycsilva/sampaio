using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.DebtPayment;

namespace Sampaio.Domain.Commands.DebtPayment
{
    public class DeleteDebtPaymentCommand : IRequest<DeleteDebtPaymentResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
