using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.Projections;
using Sampaio.Shared.Paging;
using MediatR;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Queries.ProductCategory;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;

namespace Sampaio.Domain.QueryHandlers
{
    public class ProductCategoryQueryHandler : BaseQueryHandler,
        IRequestHandler<GetProductCategoryByIdQuery, ProductCategoryVm>,
        IRequestHandler<ProductCategoryListQuery, IEnumerable<ProductCategoryVm>>
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        
        public ProductCategoryQueryHandler(IDomainNotification notifications
        , IProductCategoryRepository productCategoryRepository) : base(notifications)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductCategoryVm> Handle(GetProductCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var productCategory = await _productCategoryRepository.FindAsync(x => x.Id == query.Id);

            var result = productCategory.ToVm();

            return result;
        }

        public async Task<IEnumerable<ProductCategoryVm>> Handle(ProductCategoryListQuery query, CancellationToken cancellationToken)
        {
            var where = _productCategoryRepository.Where(query.Filter);

            var result = _productCategoryRepository.ListAsNoTracking(where).OrderBy(x => x.CreatedAt).ToVm();

            return result;
        }
    }
}