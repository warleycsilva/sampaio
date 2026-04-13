using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels.Viagens;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Passageiro
{
    public class GetLastPassageiroViagemByEmailQuery : IRequest<ViagemPagamentoVm>
    {
        public ViagemFilter Filter { get; set; }
    }
}