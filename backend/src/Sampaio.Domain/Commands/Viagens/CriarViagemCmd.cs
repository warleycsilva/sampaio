using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Commands.Passageiros;
using Sampaio.Domain.Results.Viagens;

namespace Sampaio.Domain.Commands.Viagens
{
    public class CriarViagemCmd: IRequest<ViagemBaseResult>
    {
        [Required]
        public string Origem { get; set; }
        [Required]
        public string Destino { get; set; }
        public string Observacoes { get; set; }
        [Required]
        public DateTime DataPartida { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public int QtdVagas { get; set; }
    }
    public sealed class EditarViagemCmd: CriarViagemCmd
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}