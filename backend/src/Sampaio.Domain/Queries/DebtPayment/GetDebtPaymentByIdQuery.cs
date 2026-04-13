using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.DebtPayment
{
    public class GetDebtPaymentByIdQuery : IRequest<DebtPaymentVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}