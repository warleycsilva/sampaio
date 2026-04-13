using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.Debt
{
    public class GetDebtByIdQuery : IRequest<DebtVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
