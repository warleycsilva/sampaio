using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sampaio.Api.Config;
using Sampaio.Domain.Commands.Auth;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Results.Auth;
using Sampaio.Shared.Config;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Sampaio.Api.Controllers.Common.V1
{
    
    [AllowAnonymous]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : BaseApiController
    {
        public AuthController(IMediator mediator,
            ILoggedUser loggedUser,
            IDomainNotification notifications)
            : base(mediator, loggedUser, notifications)
        {
        }

        [SwaggerResponse((int)HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(AuthorizeUserResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity, typeof(EnvelopFailResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError, typeof(EnvelopFailResult))]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorizeUserCommand command)
        {
            var result = await Mediator.Send(command, CancellationToken.None);

            return Notifications.HasNotifications() ? CreateResponse() : Ok(result);
        }
        
        [SwaggerResponse((int)HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(AuthorizeBackofficeResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity, typeof(EnvelopFailResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError, typeof(EnvelopFailResult))]
        [HttpPost("hangfire")]
        public async Task<IActionResult> Post([FromBody] AuthorizeBackofficeCommand command
            , [FromServices] HangfireUserConfig hangfireUserConfig, [FromServices] JwtTokenConfig jwtTokenConfig, [FromServices] AppConfig appConfig)
        {
            if (command.Username != hangfireUserConfig.Username || command.Password != hangfireUserConfig.Password)
            {
                Notifications.Handle("Nome de usuário ou senha inválido(s).");
                return CreateResponse();
            }
        
            var token = jwtTokenConfig.GenerateJwtToken(new[]
            {
                new Claim("username", hangfireUserConfig.Username)
            });
            
            HttpContext.Response.Cookies.Append("hangire.cookie", token,
                new CookieOptions
                {
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.Add(TimeSpan.FromDays(jwtTokenConfig.ExpiresIn)),
                    HttpOnly = true,
                    Secure = appConfig.RequireHttps,
                    Path = "/"
                });

            return CreateResponse();
        }
    }
}
