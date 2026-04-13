using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Shared.Extensions;
using Microsoft.AspNetCore.StaticFiles;
using Sampaio.Domain.Commands.Product;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.Product;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.CommandHandlers
{
    public class ProductCommandHandler : BaseCommandHandler,
        IRequestHandler<CreateProductCommand, CreateProductResult>,
        IRequestHandler<UpdateProductCommand, UpdateProductResult>,
        IRequestHandler<DeleteProductCommand, DeleteProductResult>,
        IRequestHandler<SelectProductCategoryCommand, SelectProductCategoryResult>,
        IRequestHandler<UpdateProductPhotoCommand, UpdateProductPhotoResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IStorageService _storageService;


        public  ProductCommandHandler(IUnitOfWork uow, IDomainNotification notifications,
            IProductRepository productRepository
            , IProductCategoryRepository productCategoryRepository
            , IStorageService storageService
            ) : base(uow, notifications)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _storageService = storageService;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = new CreateProductResult();
            var productUrl = string.Empty;
            var product = Product.New(command.Name, command.Description, command.Price,command.DiscountPrice, command.SessionUser.Id, command.Type, command.ProductCategoryId, command.ProductUrl);
            
            //Azure
            if (command.FileInput?.HasValue() == true)
            {
                var fileName = $"{product.Id}-{Guid.NewGuid().HasDigits()}{Path.GetExtension(command.FileInput.Name)}";
                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
                productUrl = await _storageService.UploadAsync(command.FileInput.Buffer, fileName, contentType, true);

                if (productUrl.IsNull()) 
                {
                    Notifications.Handle("Erro ao salvar Imagem do produto");
                    return null;
                }
                product.UpdateProductUrl(productUrl);
            }

            await _productRepository.AddAsync(product);

            if (!await CommitAsync())
            {
                Notifications.Handle(ProductCommandMessages.CreateFailed);
                return result;
            }

            result.ProductId = product.Id;
            result.Success = true;
            result.Message = ProductCommandMessages.CreateSuccessful;

            return result;
        }

        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var result = new UpdateProductResult();
            var productUrl = string.Empty;
            var product = await _productRepository.FindAsync(x => x.Id == command.Id);
            
            if (product == null)
            {
                Notifications.Handle(ProductCommandMessages.ProductNotFound);
            }

            product.Update(command.Name, command.Description, command.Price, command.DiscountPrice, command.CommerceId, command.Type, command.ProductCategoryId, command.ProductUrl, command.FileInput);

            //Azure
            if (command.FileInput?.HasValue() == true)
            {
                var fileName = $"{product.Id}-{Guid.NewGuid().HasDigits()}{Path.GetExtension(command.FileInput.Name)}";
                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
                productUrl = await _storageService.UploadAsync(command.FileInput.Buffer, fileName, contentType, true);

                if (productUrl.IsNull()) 
                {
                    Notifications.Handle("Erro ao salvar Imagem do produto");
                    return null;
                }
                product.UpdateProductUrl(productUrl);
            }
            
            _productRepository.Modify(product);
            
            if (!await CommitAsync())
            {
                Notifications.Handle(ProductCommandMessages.PhotoProcessingError);
                return result;
            }
            _productRepository.Modify(product);

            if (!await CommitAsync())
            {
                Notifications.Handle(ProductCommandMessages.UpdateFailed);
                return result;
            }

            result.Success = true;
            result.Message = ProductCommandMessages.UpdateSuccessful;

            return result;
        }

        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var result = new DeleteProductResult();

            var product = await _productRepository.FindAsync(x => x.Id == command.Id);
            
            if (product == null)
            {
                Notifications.Handle(ProductCommandMessages.ProductNotFound);
            }

            product.Delete();

            _productRepository.Modify(product);

            if (!await CommitAsync())
            {
                Notifications.Handle(ProductCommandMessages.DeleteFailed);
                return result;
            }

            result.Success = true;
            result.Message = ProductCommandMessages.DeleteSuccessful;

            return result;
        }

        public async Task<SelectProductCategoryResult> Handle(SelectProductCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = new SelectProductCategoryResult();

            var product = await _productRepository.FindAsync(x => x.Id == command.Id);

            if (product == null)
            {
                Notifications.Handle(ProductCommandMessages.ProductNotFound);
            }
            
            product.SetCategory(command.ProductCategoryId);
            
            _productRepository.Modify(product);

            if (!await CommitAsync())
            {
                Notifications.Handle(CommonMessages.ProblemSavindDataFriendly);
                return result;
            }
            
            result.Success = true;
            result.Message = ProductCommandMessages.SelectCategorySuccessful;

            return result;
            
            
        }

        public async Task<UpdateProductPhotoResult> Handle(UpdateProductPhotoCommand command, CancellationToken cancellationToken)
        {
            var result = new UpdateProductPhotoResult();
            var productUrl = string.Empty;
            var includes = new IncludeHelper<User>()
                .Include(x => x.Commerce)
                .Includes;
            
            var product = await _productRepository.FindAsync(x => x.Id == command.SessionUser.Id,includes);

            if (product == null)
            {
                result.Message = ProductCommandMessages.ProductNotFound;
                return result;
            }
            //Azure
            if (command.FileInput?.HasValue() == true)
            {
                var fileName = $"{product.Id}-{Guid.NewGuid().HasDigits()}{Path.GetExtension(command.FileInput.Name)}";
                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
                productUrl = await _storageService.UploadAsync(command.FileInput.Buffer, fileName, contentType, true);

                if (productUrl.IsNull())
                {
                    Notifications.Handle("Erro ao salvar Imagem do produto");
                    return null;
                }
                product.UpdateProductUrl(productUrl);
            }
            
            _productRepository.Modify(product);
            
            if (!await CommitAsync())
            {
                Notifications.Handle(ProductCommandMessages.PhotoProcessingError);
                return result;
            }
            
            result.Message = ProductCommandMessages.UpdateProductPhotoSuccess;
            result.Success = true;
            return result;
        }
    }
}
