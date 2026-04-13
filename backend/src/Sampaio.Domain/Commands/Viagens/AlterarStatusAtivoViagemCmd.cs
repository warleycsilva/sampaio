using System;
using MediatR;
using Sampaio.Domain.Results.Viagens;

namespace Sampaio.Domain.Commands.Viagens
{
    public class AlterarStatusAtivoViagemCmd : IRequest<ViagemBaseResult>
    {
        public Guid Id { get; set; }
    }
}