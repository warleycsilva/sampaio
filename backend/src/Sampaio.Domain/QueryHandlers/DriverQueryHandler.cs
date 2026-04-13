using Sampaio.Domain.Projections;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Queries.Debt;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Queries.Driver;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.QueryHandlers
{
    public class DriverQueryHandler : BaseQueryHandler,
        IRequestHandler<DriverPagedListQuery, PagedList<DriverVm>>,
        IRequestHandler<GetPlanByUserQuery, DriverVm>

    {
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger _logger;

        public DriverQueryHandler(IDomainNotification notifications,
            IDriverRepository driverRepository,
            ILogger logger
            ) : base(notifications)
        {
            _driverRepository = driverRepository;
            _logger = logger;
        }

        public async Task<PagedList<DriverVm>> Handle(DriverPagedListQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Driver>()
                .Include(x => x.User.Phones)
                .Includes;

            var where = _driverRepository.Where(query.Filter);
            var count = await _driverRepository.CountAsync(where);

            var drivers = _driverRepository.ListAsNoTracking(where, query.Filter, includes).ToVm();

            return new PagedList<DriverVm>(drivers, count, query.Filter.PageSize);
        }

        public async Task<DriverVm> Handle(GetPlanByUserQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Driver>()
                .Include(x => x.User.Driver.Plan)
                .Include(x => x.User.Phones)
                .Includes;

            var driver = await _driverRepository.FindAsync(x => x.Id == query.SessionUser.Id && !x.Deleted, includes);

            if (driver == null)
            {
                _logger.Info($"Falha ao encontrar motorista\nQUERY: {JsonUtils.Serialize(query)}");
                return driver?.ToVm() ?? new DriverVm();
            }
            var result = driver.ToVm();

            return result;
        }
    }
}
