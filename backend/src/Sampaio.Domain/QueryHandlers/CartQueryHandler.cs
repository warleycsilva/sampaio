using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.Projections;
using MediatR;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Queries.Cart;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.QueryHandlers
{
    public class CartQueryHandler : BaseQueryHandler,
        IRequestHandler<GetCartByIdQuery, CartVm>,
        IRequestHandler<CartByUserQuery, CartVm>
    {
        private readonly ICartRepository _cartRepository;
        public CartQueryHandler(IUnitOfWork uow, IDomainNotification notifications, ICartRepository cartRepository) : base(notifications, uow)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartVm> Handle(GetCartByIdQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Cart>()
                .Include(x => x.CartItems)
                .Includes;
            
            var cart = await _cartRepository.FindAsync(x => x.Id == query.Id, includes);

            if (cart == null )
            {
                Notifications.Handle(CartCommandMessages.CartNotFound);
                return null;
            }
            
            var result = cart.ToVm();

            return result;
        }

        public async Task<CartVm> Handle(CartByUserQuery query, CancellationToken cancellationToken)
        {
            var includes = new IncludeHelper<Cart>()
                .Include(x => x.CartItems)
                .Include(x => x.CartItems.Select(p=>p.Product))
                .Includes;
            
            var cart = await _cartRepository.FindAsync(x => 
                x.DriverId == query.SessionUser.Id
                && !x.Deleted
                , includes);
            
            if (cart == null )
            {
                cart = Cart.New(null, query.SessionUser.Id);
                await _cartRepository.AddAsync(cart);
                if (!await CommitAsync())
                {
                    Notifications.Handle(ProductCategoryCommandMessages.CreateFailed);
                    return new CartVm();
                }
            }
            var result = cart.ToVm();

            return result;
        }
    }
}