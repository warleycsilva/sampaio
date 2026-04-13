using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sampaio.Domain.Commands.ProductCategory;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.ProductCategory;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.CommandHandlers
{
    public class ProductCategoryCommandHandler : BaseCommandHandler,
        IRequestHandler<CreateProductCategoryCommand, CreateProductCategoryResult>,
        IRequestHandler<UpdateProductCategoryCommand, UpdateProductCategoryResult>,
        IRequestHandler<DeleteProductCategoryCommand, DeleteProductCategoryResult>
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        
        public ProductCategoryCommandHandler(IUnitOfWork uow, IDomainNotification notifications
        , IProductCategoryRepository productCategoryRepository
        ) : base(uow, notifications)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<CreateProductCategoryResult> Handle(CreateProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = new CreateProductCategoryResult();

            var productCategory = ProductCategory.New(command.Name, command.Description);

            await _productCategoryRepository.AddAsync(productCategory);

            if (!await CommitAsync())
            {
                Notifications.Handle(ProductCategoryCommandMessages.CreateFailed);
                return result;
            }
            
            result.ProductCategoryId = productCategory.Id;
            result.Success = true;
            result.Message = ProductCategoryCommandMessages.CreateSuccessful;
            
            return result;
        }

        public async Task<UpdateProductCategoryResult> Handle(UpdateProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = new UpdateProductCategoryResult();

            var productCategory = await _productCategoryRepository.FindAsync(x => x.Id == command.Id);

            if (productCategory == null)
            {
                Notifications.Handle(ProductCategoryCommandMessages.ProductCategoryNotFound);
                return null;
            }

            productCategory.Update(command.Name, command.Description);

            _productCategoryRepository.Modify(productCategory);

            if (!await CommitAsync())
            {
                Notifications.Handle(ProductCategoryCommandMessages.UpdateFailed);
                return result;
            }
            
            result.Success = true;
            result.Message = ProductCategoryCommandMessages.UpdateSuccessful;
            
            return result;
        }

        public async Task<DeleteProductCategoryResult> Handle(DeleteProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = new DeleteProductCategoryResult();

            var productCategory = await _productCategoryRepository.FindAsync(x => x.Id == command.Id);

            if (productCategory == null)
            {
                Notifications.Handle(ProductCategoryCommandMessages.ProductCategoryNotFound);
                return null;
            }
            
            productCategory.Delete();
            
            _productCategoryRepository.Modify(productCategory);

            if (!await CommitAsync())
            {
                Notifications.Handle(ProductCategoryCommandMessages.DeleteFailed);
                return result;
            }

            result.Success = true;
            result.Message = ProductCategoryCommandMessages.DeleteSuccessful;

            return result;
        }
    }
}