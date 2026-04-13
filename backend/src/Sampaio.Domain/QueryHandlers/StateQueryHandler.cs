using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Projections;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Queries.State;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;

namespace Sampaio.Domain.QueryHandlers
{
    public class StateQueryHandler : BaseQueryHandler,
        IRequestHandler<ListStateQuery,IEnumerable<StateVm>>
    {
        private readonly IStateRepository _stateRepository;

        public StateQueryHandler(IDomainNotification notifications, IStateRepository stateRepository) : base(notifications)
        {
            _stateRepository = stateRepository;
        }


        public async Task<IEnumerable<StateVm>> Handle(ListStateQuery query, CancellationToken cancellationToken) =>
            _stateRepository.ListAsNoTracking().OrderBy(x => x.Name).ToVm();
    }
}