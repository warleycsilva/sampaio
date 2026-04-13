using System;
using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels.Viagens;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Viagens
{
    public class GetViagemByIdQuery: IRequest<ViagemVm>
    {
        public Guid Id { get; set; }
    }
}