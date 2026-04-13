using Sampaio.Domain.Projections;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Queries.SaleItem;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.QueryHandlers
{
    public class SaleItemQueryHandler : BaseQueryHandler,
        IRequestHandler<GetSaleItemByIdQuery, SaleItemVm>,
        IRequestHandler<SaleItemListQuery, PagedList<SaleItemVm>>
    {
        private readonly ISaleItemRepository _saleItemRepository;
        public SaleItemQueryHandler(IDomainNotification notifications,
            ISaleItemRepository saleItemRepository
            ) : base(notifications)
        {
            _saleItemRepository = saleItemRepository;
        }

        public async Task<SaleItemVm> Handle(GetSaleItemByIdQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<SaleItem>()
                 .Include(x => x.Sale.Commerce.User.Phones)
                 .Include(x => x.Sale.Commerce.AddressInformation.City.State)
                 .Include(x => x.Sale.Driver.User.Phones)
                 .Include(x => x.Product.Commerce.User.Phones)
                 .Include(x => x.Product.Commerce.AddressInformation.City.State)
                 .Includes;

            var saleItem = await _saleItemRepository.FindAsync(x => x.Id == query.Id, includes);

            if (saleItem == null)
            {
                Notifications.Handle(SaleItemCommandMessages.SaleItemNotFound);
                return null;
            }
            
            var result = saleItem.ToVm();

            return result;
        }

        public async Task<PagedList<SaleItemVm>> Handle(SaleItemListQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<SaleItem>()
                 .Include(x => x.Sale.Commerce.User.Phones)
                 .Include(x => x.Sale.Commerce.AddressInformation.City.State)
                 .Include(x => x.Sale.Driver.User.Phones)
                 .Include(x => x.Product.Commerce.User.Phones)
                 .Include(x => x.Product.Commerce.AddressInformation.City.State)
                 .Includes;

            var where = _saleItemRepository.Where(query.Filter);

            var count = await _saleItemRepository.CountAsync(where);
            
            if (count == 0)
            {
                Notifications.Handle(SaleItemCommandMessages.SaleItemNotFound);
                return null;
            }

            var saleItems = _saleItemRepository.ListAsNoTracking(where, query.Filter, includes).ToVm();

            return new PagedList<SaleItemVm>(saleItems, count, query.Filter.PageSize);
        }
    }
}
