using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Commands.Passageiros;
using Sampaio.Domain.Results.Viagens;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Commands.Viagens
{
    public class EstornarViagemCmd: IRequest<ViagemBaseResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        // [JsonIgnore] 
        // public EPaymentStatus Status { get; set; } = EPaymentStatus.Refunded;
    }
}