using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Sale;

namespace Sampaio.Domain.Commands.Sale
{
    public class FinalizeSaleCommand : IRequest<FinalizeSaleResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}