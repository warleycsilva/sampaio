using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Domain.Results.Debt;

namespace Sampaio.Domain.Commands.Debt
{
    public class DeleteDebtCommand : IRequest<DeleteDebtResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
