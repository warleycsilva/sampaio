using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sampaio.Domain.Commands.Plan;
using Sampaio.Domain.CommandsMessages;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Results.Plan;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;

namespace Sampaio.Domain.CommandHandlers
{
    public class PlanCommandHandler : BaseCommandHandler,
        IRequestHandler<CreatePlanCommand, CreatePlanResult>,
        IRequestHandler<UpdatePlanCommand, UpdatePlanResult>,
        IRequestHandler<DeletePlanCommand, DeletePlanResult>
    {
        private readonly IPlanRepository _planRepository;

        public PlanCommandHandler(IUnitOfWork uow, IDomainNotification notifications,
            IPlanRepository planRepository
            ) : base(uow, notifications)
        {
            _planRepository = planRepository;
        }

        public async Task<CreatePlanResult> Handle(CreatePlanCommand command, CancellationToken cancellationToken)
        {
            var result = new CreatePlanResult();

            var plan = Plan.New(command.Name, command.Description, command.MonthValue);

            await _planRepository.AddAsync(plan);

            if (!await CommitAsync())
            {
                Notifications.Handle(PlanCommandMessages.CreateFailed);
                return result;
            }

            result.PlanId = plan.Id;
            result.Success = true;
            result.Message = PlanCommandMessages.CreateSuccessful;

            return result;
        }

        public async Task<UpdatePlanResult> Handle(UpdatePlanCommand command, CancellationToken cancellationToken)
        {
            var result = new UpdatePlanResult();

            var plan = await _planRepository.FindAsync(x => x.Id == command.Id);
            
            if (plan == null)
            {
                Notifications.Handle(PlanCommandMessages.PlanNotFound);
            }

            plan.Update(command.Name, command.Description, command.MonthValue);

            _planRepository.Modify(plan);

            if (!await CommitAsync())
            {
                Notifications.Handle(PlanCommandMessages.UpdateFailed);
                return result;
            }

            result.Success = true;
            result.Message = PlanCommandMessages.UpdateSuccessful;

            return result;
        }

        public async Task<DeletePlanResult> Handle(DeletePlanCommand command, CancellationToken cancellationToken)
        {
            var result = new DeletePlanResult();

            var plan = await _planRepository.FindAsync(x => x.Id == command.Id);
            
            if (plan == null)
            {
                Notifications.Handle(PlanCommandMessages.PlanNotFound);
            }

            plan.Delete();

            _planRepository.Modify(plan);

            if (!await CommitAsync())
            {
                Notifications.Handle(PlanCommandMessages.DeleteFailed);
                return result;
            }

            result.Success = true;
            result.Message = PlanCommandMessages.DeleteSuccessful;

            return result;
        }
    }
}
