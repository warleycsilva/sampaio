using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampaio.Shared.Config;
using Sampaio.Shared.Extensions;
using Sampaio.Api.Config;
using Sampaio.Domain.Commands.Signup;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Results.Signup;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Sampaio.Api.Controllers.Signup.V1
{
    [ApiExplorerSettings(GroupName = "signup:v1")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1")]
    [Route("api/signup/v1/signup")]
    public class SignupController : BaseApiController
    {
        public SignupController(IMediator mediator,
            ILoggedUser loggedUser,
            IDomainNotification notifications)
            : base(mediator, loggedUser, notifications)
        {
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, StatusCodeDescriptions.Created, typeof(EnvelopDataResult<SignupResult>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(EnvelopFailResult), Description = StatusCodeDescriptions.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, Type = typeof(EnvelopFailResult), Description = StatusCodeDescriptions.UnprocessableEntity)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(EnvelopFailResult), Description = StatusCodeDescriptions.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] SignupCommand command)
        {
            if (command == null) return UnprocessableResponse();

            var result = await Mediator.Send(command, CancellationToken.None);
           
            if (Notifications.HasNotifications()) return CreateResponse();

            if (result == null || result?.Success == false)
            {
                Notifications.Handle($"{result?.Message}".HandleNullOrEmpty(CommonMessages.ProblemSavindDataFriendly));
                return CreateResponse();
            }
            
            return CreatedResponse(result);
        }
    }
}
