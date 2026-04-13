using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.Payment
{
    public class GetPaymentByIdQuery : IRequest<PaymentVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}