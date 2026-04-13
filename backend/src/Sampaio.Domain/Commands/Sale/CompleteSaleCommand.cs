using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Sale;

namespace Sampaio.Domain.Commands.Sale
{
    public class CompleteSaleCommand: IRequest<CompleteSaleResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}