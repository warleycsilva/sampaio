using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Results;

namespace Sampaio.Api.Config
{
    public class BaseApiController : Controller
    {
        protected readonly IMediator Mediator;
        protected readonly ILoggedUser LoggedUser;
        protected readonly IDomainNotification Notifications;

        public BaseApiController(IMediator mediator,
            ILoggedUser loggedUser,
            IDomainNotification notifications)
        {
            Mediator = mediator;
            LoggedUser = loggedUser;
            Notifications = notifications;
        }

        [NonAction]
        protected IActionResult CreateResponse()
        {
            if (!Notifications.HasNotifications()) return Ok(EnvelopResult.Ok());
            return BadRequest(EnvelopResult.Fail(Notifications.Notify()));
        }

        [NonAction]
        protected IActionResult CreateResponse<T>(T data = default(T))
        {
            if (!Notifications.HasNotifications()) return Ok(EnvelopDataResult<T>.Ok(data));
            return BadRequest(EnvelopResult.Fail(Notifications.Notify()));
        }
        
        [NonAction]
        protected IActionResult CreatedResponse(string url = "")
        {
            if (!Notifications.HasNotifications()) return Created(url, EnvelopResult.Ok());
            return BadRequest(EnvelopResult.Fail(Notifications.Notify()));
        }

        [NonAction]
        protected IActionResult CreatedResponse<T>(T data = default(T), string url = "")
        {
            if (!Notifications.HasNotifications()) return Created(url, EnvelopDataResult<T>.Ok(data));
            return BadRequest(EnvelopResult.Fail(Notifications.Notify()));
        }

        [NonAction]
        protected IActionResult NotFoundResponse()
        {
            if (Notifications.HasNotifications())
            {
                Notifications.Dispose();
            }

            Notifications.Handle(CommonMessages.NotFound);
            return new NotFoundObjectResult(EnvelopResult.Fail(Notifications.Notify()));
        }

        [NonAction]
        protected IActionResult UnprocessableResponse()
        {
            if (Notifications.HasNotifications())
            {
                Notifications.Dispose();
            }
            
            Notifications.Handle(CommonMessages.UnprocessableEntity);

            return UnprocessableEntity(EnvelopResult.Fail(Notifications.Notify()));
        }

        [NonAction]
        protected IActionResult UnauthorizedResponse()
        {
            if (Notifications.HasNotifications())
            {
                Notifications.Dispose();
            }
            
            Notifications.Handle(CommonMessages.Unauthorized);
            
            return new ObjectResult(EnvelopResult.Fail(Notifications.Notify()))
            {
                StatusCode = 401
            };
        }

        [NonAction]
        protected IActionResult ForbiddenResponse()
        {
            if (Notifications.HasNotifications())
            {
                Notifications.Dispose();
            }
            
            Notifications.Handle(CommonMessages.Forbidden);
            
            return new ObjectResult(EnvelopResult.Fail(Notifications.Notify()))
            {
                StatusCode = 403
            };
        }
        

        protected override void Dispose(bool disposing)
        {
            Notifications.Dispose();
        }
    }
}
