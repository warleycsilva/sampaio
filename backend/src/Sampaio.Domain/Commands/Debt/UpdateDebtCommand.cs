using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.Debt;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Commands.Debt
{
    public class UpdateDebtCommand : IRequest<UpdateDebtResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public decimal? Value { get; set; }
        public Guid? DriverId { get; set; }
        public EDebtStatus? Status { get; set; }
        public EDebtType? Type { get; set; }
    }
}
