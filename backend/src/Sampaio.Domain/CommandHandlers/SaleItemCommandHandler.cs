using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Commands.SaleItem;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.SaleItem;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.CommandHandlers
{
    public class SaleItemCommandHandler : BaseCommandHandler,
        IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>,
        IRequestHandler<UpdateSaleItemCommand, UpdateSaleItemResult>,
        IRequestHandler<DeleteSaleItemCommand, DeleteSaleItemResult>
    {
        private readonly ISaleItemRepository _saleItemRepository;
        private readonly IProductRepository _productRepository;
        public  SaleItemCommandHandler(IUnitOfWork uow, IDomainNotification notifications,
            ISaleItemRepository saleItemRepository,
            IProductRepository productRepository
        ) : base(uow, notifications)
        {
            _saleItemRepository = saleItemRepository;
            _productRepository = productRepository;
        }

        public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand command, CancellationToken cancellationToken)
        {
            var result = new CreateSaleItemResult();

            var product = await _productRepository.FindAsync(p => p.Id == command.ProductId);

            if (product == null)
            {
                Notifications.Handle(ProductCommandMessages.ProductNotFound);
                return result;
            }
            
            var saleItem = SaleItem.New(command.SaleId, command.ProductId, command.Quantity, product.Name,product.Price, product.DiscountPrice);

            await _saleItemRepository.AddAsync(saleItem);

            if (!await CommitAsync())
            {
                Notifications.Handle(SaleItemCommandMessages.CreateFailed);
                return result;
            }

            result.SaleItemId = saleItem.Id;
            result.Success = true;
            result.Message = SaleItemCommandMessages.CreateSuccessful;

            return result;
        }

        public async Task<UpdateSaleItemResult> Handle(UpdateSaleItemCommand command, CancellationToken cancellationToken)
        {
            var result = new UpdateSaleItemResult();

            var saleItem = await _saleItemRepository.FindAsync(x => x.Id == command.Id);
            
            if (saleItem == null)
            {
                Notifications.Handle(SaleItemCommandMessages.SaleItemNotFound);
            }

            saleItem.Update(command.SaleId, command.ProductId, command.Quantity);

            _saleItemRepository.Modify(saleItem);

            if (!await CommitAsync())
            {
                Notifications.Handle(SaleItemCommandMessages.UpdateFailed);
                return result;
            }

            result.Success = true;
            result.Message = SaleItemCommandMessages.UpdateSuccessful;

            return result;
        }

        public async Task<DeleteSaleItemResult> Handle(DeleteSaleItemCommand command, CancellationToken cancellationToken)
        {
            var result = new DeleteSaleItemResult();

            var saleItem = await _saleItemRepository.FindAsync(x => x.Id == command.Id);
            
            if (saleItem == null)
            {
                Notifications.Handle(SaleItemCommandMessages.SaleItemNotFound);
            }

            saleItem.Delete();

            _saleItemRepository.Modify(saleItem);

            if (!await CommitAsync())
            {
                Notifications.Handle(SaleItemCommandMessages.DeleteFailed);
                return result;
            }

            result.Success = true;
            result.Message = SaleItemCommandMessages.DeleteSuccessful;

            return result;
        }
    }
}