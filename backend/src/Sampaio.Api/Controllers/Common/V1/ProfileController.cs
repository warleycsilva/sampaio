using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampaio.Api.Config;
using Sampaio.Domain.Commands.Commerce;
using Sampaio.Domain.Commands.Profiles;
using Sampaio.Domain.Commands.UserDevice;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Results.Profiles;
using Sampaio.Domain.Results.UserDevice;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Results;
using Sampaio.Shared.Security;
using Swashbuckle.AspNetCore.Annotations;

namespace Sampaio.Api.Controllers.V1
{
    [ApiExplorerSettings(GroupName = "common:v1")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/profile")]
    public class ProfileController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly SessionUser _sessionUser;
        public ProfileController(IMediator mediator,
            ILoggedUser loggedUser,
            IDomainNotification notifications)
            : base(mediator, loggedUser, notifications)
        {
            _mediator = mediator;
            _sessionUser = loggedUser.User;
        }

        [AllowAnonymous]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(EnvelopDataResult<ResetPasswordRequestResult>),
            Description = StatusCodeDescriptions.Ok)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.BadRequest)]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.UnprocessableEntity)]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.InternalServerError)]
        [HttpPost("reset-password-request")]
        public async Task<IActionResult> ResetPasswordRequest([FromBody] ResetPasswordRequestCommand command) =>
            CreateResponse(await Mediator.Send(command, CancellationToken.None));

        [AllowAnonymous]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(EnvelopDataResult<ResetPasswordRequestResult>),
            Description = StatusCodeDescriptions.Ok)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.BadRequest)]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.UnprocessableEntity)]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.InternalServerError)]
        [HttpPost("reset-password-with-token")]
        public async Task<IActionResult> ResetPasswordWithToken([FromBody] ResetPasswordCommand command) =>
            CreateResponse(await Mediator.Send(command, CancellationToken.None));


        [AllowAnonymous]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(EnvelopDataResult<ActiveAccountResult>),
            Description = StatusCodeDescriptions.Ok)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.BadRequest)]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.UnprocessableEntity)]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.InternalServerError)]
        [HttpPost("activate-account")]
        public async Task<IActionResult> ActivateAccount([FromBody] ActiveAccountCommand command) =>
            CreateResponse(await Mediator.Send(command, CancellationToken.None));
        
        [Authorize]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(EnvelopDataResult<ResetPasswordRequestResult>),
            Description = StatusCodeDescriptions.Ok)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.BadRequest)]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.UnprocessableEntity)]
        [SwaggerResponse((int) HttpStatusCode.Unauthorized, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.Unauthorized)]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.InternalServerError)]
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordCommand command)
        {
            command.SessionUser = LoggedUser.User;
            return CreateResponse(await Mediator.Send(command, CancellationToken.None));
        }

        [Authorize]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(EnvelopDataResult<ResetPasswordRequestResult>),
            Description = StatusCodeDescriptions.Ok)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.BadRequest)]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.UnprocessableEntity)]
        [SwaggerResponse((int) HttpStatusCode.Unauthorized, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.Unauthorized)]
        [SwaggerResponse((int) HttpStatusCode.Forbidden, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.Forbidden)]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.InternalServerError)]
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(
            [FromBody] UpdateUserProfileCommand command)
        {
            command.SessionUser = LoggedUser.User;
            var response = await Mediator.Send(command, CancellationToken.None);
            return CreateResponse(response);
        }

        
        [Authorize]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(EnvelopDataResult<AcceptTermsResult>),
            Description = StatusCodeDescriptions.Ok)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.BadRequest)]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.UnprocessableEntity)]
        [SwaggerResponse((int) HttpStatusCode.Unauthorized, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.Unauthorized)]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.InternalServerError)]
        [HttpGet("accept-terms-conditions")]
        [Authorize]
        public async Task<IActionResult> AcceptTerms()
        {
            return CreateResponse(await Mediator.Send(new AcceptTermsCommand {SessionUser = LoggedUser.User},
                CancellationToken.None));
        }
        
        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileCommand command)
        {
            command.SessionUser = _sessionUser;
            return CreateResponse(await _mediator.Send(command, CancellationToken.None));
        }

        [HttpPut("update-avatar")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatar([FromBody] ChangeProfilePhotoCommand command)
        {
            command.SessionUser = _sessionUser;
            return CreateResponse(await _mediator.Send(command, CancellationToken.None));    
        }
        
        [Authorize]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(EnvelopDataResult<UserDeviceBaseResult>),
            Description = StatusCodeDescriptions.Ok)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.BadRequest)]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.UnprocessableEntity)]
        [SwaggerResponse((int) HttpStatusCode.Unauthorized, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.Unauthorized)]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Type = typeof(EnvelopFailResult),
            Description = StatusCodeDescriptions.InternalServerError)]
        [HttpPost("user-device")]
        public async Task<IActionResult> AddUserDevice(
            [FromBody] AddUserDeviceCommand command)
        {
            command.SessionUser = LoggedUser.User;
            return CreateResponse(await Mediator.Send(command, CancellationToken.None));
        }       
        
        [HttpPut("phone")]
        [Authorize]
        public async Task<IActionResult> UpdatePhone([FromBody] UpsertPhoneCommand command)
        {
            command.UserId = _sessionUser.Id;
            return CreateResponse(await _mediator.Send(command, CancellationToken.None));    
        }
        [HttpPut("address")]
        [Authorize]
        public async Task<IActionResult> UpdateAddress([FromBody] UpsertAddressCommand command)
        {
            command.UserId = _sessionUser.Id;
            return CreateResponse(await _mediator.Send(command, CancellationToken.None));    
        }
        [HttpPut("identification")]
        [Authorize]
        public async Task<IActionResult> UpdateIdentification([FromBody] UpsertIdentificationCommand command)
        {
            command.UserId = _sessionUser.Id;
            return CreateResponse(await _mediator.Send(command, CancellationToken.None));    
        }
    }
}