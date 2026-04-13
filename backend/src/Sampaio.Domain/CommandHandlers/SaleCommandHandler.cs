using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.Commands.Sale;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.Sale;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.CommandHandlers
{
    public class SaleCommandHandler : BaseCommandHandler,
        IRequestHandler<CreateSaleCommand, CreateSaleResult>,
        IRequestHandler<UpdateSaleCommand, UpdateSaleResult>,
        IRequestHandler<DeleteSaleCommand, DeleteSaleResult>,
        IRequestHandler<CompleteSaleCommand, CompleteSaleResult>,
        IRequestHandler<FinalizeSaleCommand, FinalizeSaleResult>

    {
        private readonly ISaleRepository _saleRepository;

        public  SaleCommandHandler(IUnitOfWork uow, IDomainNotification notifications,
            ISaleRepository saleRepository
            ) : base(uow, notifications)
        {
            _saleRepository = saleRepository;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var result = new CreateSaleResult();

            var sale = Sale.New(command.Value, 
                command.DiscountValue, 
                command.CommerceId, command.DriverId);

            await _saleRepository.AddAsync(sale);

            if (!await CommitAsync())
            {
                Notifications.Handle(SaleCommandMessages.CreateFailed);
                return result;
            }

            result.SaleId = sale.Id;
            result.Success = true;
            result.Message = SaleCommandMessages.CreateSuccessful;

            return result;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var result = new UpdateSaleResult();

            var sale = await _saleRepository.FindAsync(x => x.Id == command.Id);
            
            if (sale == null)
            {
                Notifications.Handle(SaleCommandMessages.SaleNotFound);
            }

            sale.Update(command.Value, command.CommerceId, command.DriverId, command.Status, command.DiscountValue);

            _saleRepository.Modify(sale);

            if (!await CommitAsync())
            {
                Notifications.Handle(SaleCommandMessages.UpdateFailed);
                return result;
            }

            result.Success = true;
            result.Message = SaleCommandMessages.UpdateSuccessful;

            return result;

        }

        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var result = new DeleteSaleResult();

            var sale = await _saleRepository.FindAsync(x => x.Id == command.Id);
            
            if (sale == null)
            {
                Notifications.Handle(SaleCommandMessages.SaleNotFound);
            }

            sale.Delete();

            _saleRepository.Modify(sale);

            if (!await CommitAsync())
            {
                Notifications.Handle(SaleCommandMessages.DeleteFailed);
                return result;
            }

            result.Success = true;
            result.Message = SaleCommandMessages.DeleteSuccessful;

            return result;
        }

        public async Task<CompleteSaleResult> Handle(CompleteSaleCommand command, CancellationToken cancellationToken)
        {
            var result = new CompleteSaleResult();      
            var sale = await _saleRepository.FindAsync(x => x.Id == command.Id);

            sale.Complete();

            _saleRepository.Modify(sale);

            if (!await CommitAsync())
            {
                Notifications.Handle(SaleCommandMessages.UpdateFailed);
                return result;
            }

            result.Success = true;
            result.Message = SaleCommandMessages.Complete;

            return result;
        }

        public async Task<FinalizeSaleResult> Handle(FinalizeSaleCommand command, CancellationToken cancellationToken)
        {
            var result = new FinalizeSaleResult();

            var sale = await _saleRepository.FindAsync(x => x.Id == command.Id);
            
            sale.Finalize();
            
            _saleRepository.Modify(sale);

            if (!await CommitAsync())
            {
                Notifications.Handle(SaleCommandMessages.FailToFinalize);
                return result;
            }
            
            result.Success = true;
            result.Message = SaleCommandMessages.Finalized;
            
            return result;
        }
    }
}
