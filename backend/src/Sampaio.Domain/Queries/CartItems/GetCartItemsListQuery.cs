using MediatR;
using Sampaio.Domain.Filters;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Queries.CartItems
{
    public class GetCartItemsListQuery : IRequest<PagedList<CartItemVm>>
    {
        public CartItemsFilter Filter { get; set; }
    }
}