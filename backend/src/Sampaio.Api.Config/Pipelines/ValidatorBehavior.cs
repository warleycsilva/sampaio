using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Sampaio.Shared.Notifications;

namespace Sampaio.Api.Config.Pipelines
{
    public class ValidatorBehavior<TRequest, Unit> : IPipelineBehavior<TRequest, Unit>
    {
        private readonly IEnumerable<IValidator> _validators;
        private readonly IDomainNotification _notifications;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators,
            IDomainNotification notifications)
        {
            _validators = validators;
            _notifications = notifications;
        }

        public Task<Unit> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<Unit> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(x => x.Errors)
                .Where(f => f != null)
                .ToList();

            return failures.Any() ? Notify(failures) : next();
        }

        private Task<Unit> Notify(IEnumerable<ValidationFailure> failures)
        {
            var result = default(Unit);

            foreach (var failure in failures)
            {
                _notifications.Handle(failure.ErrorCode, failure.ErrorMessage);
            }

            return Task.FromResult(result);
        }
    }
}