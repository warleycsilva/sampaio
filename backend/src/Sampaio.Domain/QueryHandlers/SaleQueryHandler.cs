using Sampaio.Domain.Projections;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Queries.Sale;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.QueryHandlers
{
    public class SaleQueryHandler : BaseQueryHandler,
        IRequestHandler<GetSaleByIdQuery, SaleVm>,
        IRequestHandler<SaleListQuery, PagedList<SaleVm>>,
        IRequestHandler<DriverExtractByUserQuery, DriverExtractVm>
    {
        private readonly ISaleRepository _saleRepository;
        public SaleQueryHandler(IDomainNotification notifications,
            ISaleRepository saleRepository
            ) : base(notifications)
        {
            _saleRepository = saleRepository;
        }

        public async Task<SaleVm> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Sale>()
                .Include(x => x.Commerce.User.Phones)
                .Include(x => x.Commerce.AddressInformation.City.State)
                .Include(x => x.Driver.User.Phones)
                .Includes;

            var sale = await _saleRepository.FindAsync(x => x.Id == query.Id, includes);

            if (sale == null )
            {
                Notifications.Handle(SaleCommandMessages.SaleNotFound);
                return null;
            }
            
            var result = sale.ToVm();

            return result;
        }

        public async Task<PagedList<SaleVm>> Handle(SaleListQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Sale>()
                .Include(x => x.Commerce.User.Phones)
                .Include(x => x.Commerce.AddressInformation.City.State)
                .Include(x => x.Driver.User.Phones)
                .Includes;

            var where = _saleRepository.Where(query.Filter);

            var count = await _saleRepository.CountAsync(where);
            
            var sales = _saleRepository.ListAsNoTracking(where, query.Filter, includes).OrderByDescending(d=>d.CreatedAt).ToVm();

            return new PagedList<SaleVm>(sales, count, query.Filter.PageSize);
        }

        public async Task<DriverExtractVm> Handle(DriverExtractByUserQuery query, CancellationToken cancellationToken)
        {
            var result = new DriverExtractVm();
            

            try
            {
                var includes = new IncludeHelper<Sale>()
                    .Include(x => x.Commerce.User.Phones)
                    .Include(x => x.Commerce.AddressInformation.City.State)
                    .Include(x => x.Driver.User.Phones)
                    .Includes;
                
                var where = _saleRepository.Where(query.Filter);

                var sales = _saleRepository.ListAsNoTracking(where, query.Filter, includes).OrderByDescending(d=>d.CreatedAt).ToVm();

                result.Sales = sales.ToList();

                result.Total = sales.Sum(x => x.DiscountValue.Value).ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));

                result.MonthTotal = sales.Where(x => x.CreatedAt>DateTime.UtcNow.AddDays(-30)).Sum(x => x.DiscountValue.Value).ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));

            }
            catch
            {
                return result;
            }
            return result;

        }
    }
}
