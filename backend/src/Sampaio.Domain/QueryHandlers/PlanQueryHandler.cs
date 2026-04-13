using Sampaio.Domain.Projections;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Queries.Plan;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;

namespace Sampaio.Domain.QueryHandlers
{
    public class PlanQueryHandler : BaseQueryHandler,
        IRequestHandler<GetPlanByIdQuery, PlanVm>,
        IRequestHandler<PlanListQuery, IEnumerable<PlanVm>>
    {
        private readonly IPlanRepository _planRepository;

        public PlanQueryHandler(IDomainNotification notifications,
            IPlanRepository planRepository
            ) : base(notifications)
        {
            _planRepository = planRepository;
        }

        public async Task<PlanVm> Handle(GetPlanByIdQuery query, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.FindAsync(x => x.Id == query.Id);

            if (plan == null)
            {
                Notifications.Handle(PlanCommandMessages.PlanNotFound);
                return null;
            }
            var result = plan.ToVm();

            return result;
        }

        public async Task<IEnumerable<PlanVm>> Handle(PlanListQuery query, CancellationToken cancellationToken)
        {
            var where = _planRepository.Where(query.Filter);

            var result = _planRepository.ListAsNoTracking(where).ToVm();

            return result;
        }
    }
}
