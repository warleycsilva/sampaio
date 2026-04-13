using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Projections;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Queries.City;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.QueryHandlers
{
    public class CityQueryHandler : BaseQueryHandler,
        IRequestHandler<ListCityByStateQuery,IEnumerable<CityVm>>
    {
        private readonly ICityRepository _cityRepository;

        public CityQueryHandler(IDomainNotification notifications, ICityRepository cityRepository) : base(notifications)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<CityVm>> Handle(ListCityByStateQuery query, CancellationToken cancellationToken)
        {
           var includes = new IncludeHelper<City>().Include(x => x.State).Includes;
           return _cityRepository.ListAsNoTracking(x => x.StateId == query.StateId, includes: includes).OrderBy(x => x.Name).ToVm();
        }
    }
}