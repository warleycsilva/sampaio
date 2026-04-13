using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.Filters;
using Sampaio.Domain.Projections;
using MediatR;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Queries.Commerce;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.QueryHandlers
{
    public class CommerceQueryHandler : BaseQueryHandler,
        IRequestHandler<CommercePagedListQuery, PagedList<CommerceVm>>,
        IRequestHandler<GetSegmentByUserQuery, CommerceVm>,
        IRequestHandler<ProductsByCommerceQuery, PagedList<ProductVm>>,
        IRequestHandler<GetCommerceByIdQuery, CommerceVm>,
        IRequestHandler<GetCommerceByUserQuery, CommerceVm>

    {
        private readonly ICommerceRepository _commerceRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public CommerceQueryHandler(IDomainNotification notifications
            , ICommerceRepository commerceRepository
            , ILogger logger
            , IProductRepository productRepository
        ) : base(notifications)
        {
            _commerceRepository = commerceRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<PagedList<CommerceVm>> Handle(CommercePagedListQuery query,
            CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Commerce>()
                .Include(x => x.User.Phones)
                .Include(x => x.AddressInformation.City.State)
                .Include(x => x.Segment)
                .Includes;

            var where = _commerceRepository.Where(query.Filter);
            var count = await _commerceRepository.CountAsync(where);
            var commerces = _commerceRepository.ListAsNoTracking(where, includes: includes).ToVm();
            return new PagedList<CommerceVm>(commerces, count, query.Filter.PageSize);
        }

        public async Task<CommerceVm> Handle(GetSegmentByUserQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Commerce>()
                .Include(x => x.User.Phones)
                .Include(x => x.Segment)
                .Include(x => x.Identification)
                .Include(x => x.AddressInformation.City.State)
                .Includes;

            var commerce =
                await _commerceRepository.FindAsync(x => x.Id == query.SessionUser.Id && !x.Deleted, includes);

            if (commerce == null)
            {
                Notifications.Handle("Loja não encontrada ou cadastro bloqueado.");
                return null;
            }

            var result = commerce.ToVm();

            return result;
        }

        public async Task<PagedList<ProductVm>> Handle(ProductsByCommerceQuery query,
            CancellationToken cancellationToken)
        {
            var where = _productRepository.Where(query.Filter);

            var count = await _productRepository.CountAsync(where);

            if (count == 0)
            {
                Notifications.Handle(ProductCommandMessages.ProductNotFound);
                return null;
            }

            var commerce = _productRepository.ListAsNoTracking(where, query.Filter).ToVm();

            return new PagedList<ProductVm>(commerce, count, query.Filter.PageSize);
        }

        public async Task<CommerceVm> Handle(GetCommerceByIdQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Commerce>()
                .Include(x => x.User.Phones)
                .Include(x => x.Segment)
                .Include(x => x.Identification)
                .Include(x => x.AddressInformation.City.State)
                .Includes;

            var commerce = await _commerceRepository.FindAsync(x => x.Id == query.Id, includes);

            if (commerce == null)
            {
                Notifications.Handle(CommerceCommandMessages.CommerceNotFound);
                return null;
            }

            var result = commerce.ToVm();

            return result;
        }

        public async Task<CommerceVm> Handle(GetCommerceByUserQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Commerce>()
                .Include(x => x.User.Phones)
                .Include(x => x.Segment)
                .Include(x => x.Identification)
                .Include(x => x.AddressInformation.City.State)
                .Includes;

            var commerce =
                await _commerceRepository.FindAsync(x => x.Id == query.SessionUser.Id && !x.Deleted, includes);

            if (commerce == null)
            {
                Notifications.Handle(CommerceCommandMessages.CommerceNotFound);
                return null;
            }

            var result = commerce.ToVm();

            return result;
        }
    }
}