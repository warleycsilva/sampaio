using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampaio.Api.Config;
using Sampaio.Domain.Commands.Auth;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Results.Auth;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Sampaio.Api.Controllers.Backoffice.V1
{
    // [ApiExplorerSettings(GroupName = "backoffice:v1")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/backoffice/auth")]
    public class AuthController: BaseApiController
    {
        public AuthController(IMediator mediator,
            ILoggedUser loggedUser,
            IDomainNotification notifications)
            : base(mediator, loggedUser, notifications)
        {
        }

        [SwaggerResponse((int)HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(AuthorizeBackofficeResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity, typeof(EnvelopFailResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError, typeof(EnvelopFailResult))]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorizeBackofficeCommand command)
        {
            var result = await Mediator.Send(command, CancellationToken.None);

            return Notifications.HasNotifications() ? CreateResponse() : Ok(result);
        }
    }
}
