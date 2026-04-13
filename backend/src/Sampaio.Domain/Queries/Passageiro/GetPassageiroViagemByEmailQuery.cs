using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels.Viagens;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.Passageiro
{
    public class GetPassageiroViagemByEmailQuery : IRequest<PagedList<ViagemVm>>
    {
        public ViagemFilter Filter { get; set; }
    }
}