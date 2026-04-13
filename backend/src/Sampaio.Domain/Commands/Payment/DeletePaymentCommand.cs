using System;
using System.ComponentModel.DataAnnotations;
using Sampaio.Shared.Enums;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Payment;

namespace Sampaio.Domain.Commands.Payment
{
    public class DeletePaymentCommand : IRequest<DeletePaymentResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}