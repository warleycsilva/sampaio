using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.Projections;
using MediatR;
using Sampaio.Domain.Commands.Cart;
using Sampaio.Domain.Commands.CartItem;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.Cart;
using Sampaio.Domain.Results.CartItem;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.CommandHandlers
{
    public class CartItemCommandHandler : BaseCommandHandler,
        IRequestHandler<CreateCartItemCommand, CreateCartItemResult>,
        IRequestHandler<RemoveCartItemCommand, RemoveCartItemResult>,
        IRequestHandler<AbandonCartCommand, AbandonCartResult>,
        IRequestHandler<FinalizeCartCommand, FinalizeCartResult>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;

        public CartItemCommandHandler(IUnitOfWork uow, IDomainNotification notifications,
            IProductRepository productRepository,
            ICartItemRepository cartItemRepository,
            ISaleRepository saleRepository,
            ICartRepository cartRepository) : base(uow, notifications)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _saleRepository = saleRepository;
        }

        public async Task<CreateCartItemResult> Handle(CreateCartItemCommand command,
            CancellationToken cancellationToken)
        {
            var result = new CreateCartItemResult();

            var includes = new IncludeHelper<Cart>()
                .Include(c => c.CartItems)
                .Include(c => c.CartItems.Select(i => i.Product))
                .Includes;
            var cart = await _cartRepository.FindAsync(x => x.DriverId == command.SessionUser.Id && x.Deleted == false,
                includes);

            var product = await _productRepository.FindAsync(x => x.Id == command.ProductId);

            if (!await ValidateProduct(command.ProductId))
            {
                result.Message = CartItemCommandMessages.ProductNotFound;
                return result;
            }

            if (cart == null)
            {
                cart = Cart.New(product.CommerceId, command.SessionUser.Id);
                await _cartRepository.AddAsync(cart);
                if (!await CommitAsync())
                {
                    result.Message = CommonMessages.ProblemSavindDataFriendly;
                    return result;
                }
            }

            cart.AddCommerceId(product.CommerceId);
            _cartRepository.Modify(cart);
            if (!await CommitAsync())
            {
                result.Message = CartCommandMessages.CartCommerceNotFound;
                return result;
            }

            if (cart.CartItems != null)
            {
                if (cart.CartItems.Any(i => i.Product.CommerceId != product.CommerceId))
                {
                    result.Message = CartItemCommandMessages.CartItemFromDifferenteCommerce;
                    return result;
                }

                if (cart.CartItems.Any(i => i.ProductId == command.ProductId))
                {
                    cart.CartItems.FirstOrDefault(i => i.ProductId == command.ProductId).UpQtd(1);
                }
                else
                {
                    var cartItem = CartItem.AddItem(cart.Id, command.ProductId, command.Quantity);

                    await _cartItemRepository.AddAsync(cartItem);
                    if (!await CommitAsync())
                    {
                        result.Message = CommonMessages.ProblemSavindDataFriendly;
                        return result;
                    }
                }
            }

            cart.UpdateValue(0, 0);
            foreach (var item in cart.CartItems)
            {
                if (!item.Deleted)
                    cart.AddToSubTotal(item.Quantity, item.Product.DiscountPrice,item.Product.Price);
            }

            _cartRepository.Modify(cart);
            if (!await CommitAsync())
            {
                result.Message = CommonMessages.ProblemSavindDataFriendly;
                return result;
            }

            result.Cart = cart.ToVm();
            result.Message = CartItemCommandMessages.CreateSuccessful;
            result.Success = true;
            return result;
        }

        public async Task<RemoveCartItemResult> Handle(RemoveCartItemCommand command,
            CancellationToken cancellationToken)
        {
            var result = new RemoveCartItemResult();
            var includes = new IncludeHelper<Cart>()
                .Include(c => c.CartItems)
                .Include(c => c.CartItems.Select(i => i.Product))
                .Includes;

            var cart = await _cartRepository.FindAsync(x => x.DriverId == command.SessionUser.Id && x.Deleted == false,
                includes);

            if (cart == null)
            {
                result.Message = "Carrinho vazio.";
                return result;
            }

            var cartItem =
                await _cartItemRepository.FindAsync(c =>
                    c.ProductId == command.CartItemId && c.CartId == cart.Id);

            if (cartItem == null || cartItem?.Quantity == 0)
            {
                result.Message = CartItemCommandMessages.CartItemNotFound;
                return result;
            }

            var product = await _productRepository.FindAsync(x => x.Id == cartItem.ProductId);
            if (product == null)
            {
                result.Message = CartItemCommandMessages.ProductNotFound;
                return result;
            }

            if (cartItem.Quantity > 1)
            {
                cartItem.RemoveOne();
            }
            else if (cartItem.Quantity == 1)
            {
                cartItem.Delete();
            }

            if (!await CommitAsync())
            {
                result.Message = CommonMessages.ProblemSavindDataFriendly;
                return result;
            }

            cart.UpdateValue(0,0);
            foreach (var item in cart.CartItems)
            {
                if (!item.Deleted)
                {
                    if (item.Id == cartItem.Id)
                    {
                        cart.AddToSubTotal(cartItem.Quantity, product.DiscountPrice, product.Price);
                    }
                    else
                    {
                        cart.AddToSubTotal(item.Quantity, item.Product.DiscountPrice, product.Price);   
                    }
                }
            }

            if (!await CommitAsync())
            {
                result.Message = CommonMessages.ProblemSavindDataFriendly;
                return result;
            }

            result.Cart = cart.ToVm();
            result.Success = true;
            result.Message = CartItemCommandMessages.DeleteSuccessful;
            return result;
        }

        public async Task<bool> ValidateProduct(Guid? id)
        {
            var product = await _productRepository.FindAsync(x => x.Id == id);
            return product != null;
        }

        public async Task<AbandonCartResult> Handle(AbandonCartCommand command, CancellationToken cancellationToken)
        {
            var result = new AbandonCartResult();
            var includes = new IncludeHelper<Cart>()
                .Include(c => c.CartItems)
                .Include(c => c.CartItems.Select(i => i.Product))
                .Includes;
            var cart = await _cartRepository.FindAsync(c => c.Id == command.Id, includes);
            if (cart == null)
            {
                result.Message = CartCommandMessages.CartNotFound;
                return result;
            }

            cart.AbandonCart();
            if (!await CommitAsync())
            {
                result.Message = CommonMessages.ProblemSavindDataFriendly;
                return result;
            }

            result.Success = true;
            result.Message = CartCommandMessages.AbandonedSuccessful;
            return result;
        }

        public async Task<FinalizeCartResult> Handle(FinalizeCartCommand command, CancellationToken cancellationToken)
        {
            var result = new FinalizeCartResult();

            var includes = new IncludeHelper<Cart>()
                .Include(c => c.CartItems.Select(ci => ci.Product))
                .Includes;

            var cart = await _cartRepository.FindAsync(x => x.Id == command.Id, includes);

            if (cart == null)
            {
                result.Message = CartCommandMessages.CartNotFound;
                return result;
            }

            var sale = cart.Finalize();
            sale.Finalize();

            await _saleRepository.AddAsync(sale);

            if (!await CommitAsync())
            {
                result.Message = CommonMessages.ProblemSavindDataFriendly;
                return result;
            }

            foreach (var item in cart.CartItems)
            {
                sale.AddCartItem(item, item.Product.Name, item.Product.Price, item.Product.DiscountPrice);
            }

            if (!await CommitAsync())
            {
                result.Message = CommonMessages.ProblemSavindDataFriendly;
                return result;
            }

            cart.AbandonCart();
            if (!await CommitAsync())
            {
                result.Message = CommonMessages.ProblemSavindDataFriendly;
                return result;
            }

            result.Sale = sale.ToVm();
            result.Success = true;
            result.Message = CartCommandMessages.FinalizeCart;
            return result;
        }
    }
}