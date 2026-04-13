using System;
using System.Text.Json.Serialization;
using MediatR;
using Sampaio.Domain.ViewModels.Viagens;

namespace Sampaio.Domain.Commands.Viagens
{
    public class SyncStatusPagamentoCommand : IRequest<ViagemPagamentoVm>
    {
        [JsonIgnore]
        public Guid PagamentoId { get; set; }
    }
}