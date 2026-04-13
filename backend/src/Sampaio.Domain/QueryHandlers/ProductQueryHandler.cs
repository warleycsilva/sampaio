using Sampaio.Domain.Projections;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Queries.Product;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.QueryHandlers
{
    public class ProductQueryHandler : BaseQueryHandler,
        IRequestHandler<GetProductByIdQuery, ProductVm>,
        IRequestHandler<ProductListQuery, PagedList<ProductVm>>
    {
        private readonly IProductRepository _productRepository;

        public ProductQueryHandler(IDomainNotification notifications,
            IProductRepository productRepository
            ) : base(notifications)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductVm> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = (await _productRepository.FindAsync(x => x.Id == query.Id))?.ToVm();

            if (product == null)
            {
                Notifications.Handle(ProductCommandMessages.ProductNotFound);
                return null;
            }

            return product;
        }

        public async Task<PagedList<ProductVm>> Handle(ProductListQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Product>()
                .Include(x => x.Commerce.User.Phones)
                .Include(x => x.Commerce.AddressInformation.City.State)
                .Includes;

            var where = _productRepository.Where(query.Filter);

            var count = await _productRepository.CountAsync(where);
            
            var products = _productRepository.ListAsNoTracking(where, query.Filter, includes)
                .OrderBy(p=>p.Name)
                .ToVm();

            return new PagedList<ProductVm>(products, count, query.Filter.PageSize);
        }
    }
}
