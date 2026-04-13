using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.Projections;
using Sampaio.Domain.Queries.Commerce;
using Sampaio.Shared.Paging;
using MediatR;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Queries.CommerceSegment;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;

namespace Sampaio.Domain.QueryHandlers
{
    public class CommerceSegmentQueryHandler : BaseQueryHandler,
        IRequestHandler<CommerceSegmentListQuery, IEnumerable<CommerceSegmentVm>>,
        IRequestHandler<GetCommerceSegmentByIdQuery, CommerceSegmentVm>
    {
        private readonly ICommerceSegmentRepository _commerceSegmentRepository;

        public CommerceSegmentQueryHandler(IDomainNotification notifications,
            ICommerceSegmentRepository commerceSegmentRepository
            ) : base(notifications)
        {
            _commerceSegmentRepository = commerceSegmentRepository;
        }

        public async Task<IEnumerable<CommerceSegmentVm>> Handle(CommerceSegmentListQuery query, CancellationToken cancellationToken)
        {
            var where = _commerceSegmentRepository.Where(query.Filter);
            
            var result = _commerceSegmentRepository.ListAsNoTracking(where).ToVm();

            return result;
        }

        public async Task<CommerceSegmentVm> Handle(GetCommerceSegmentByIdQuery query, CancellationToken cancellationToken)
        {
            var commerceSegment = await _commerceSegmentRepository.FindAsync(x => x.Id == query.Id);
            
            if (commerceSegment == null)
            {
                Notifications.Handle(CommerceSegmentCommandMessages.CommerceSegmentNotFound);
                return null;
            }

            var result = commerceSegment.ToVm();

            return result;
        }
    }
}