using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampaio.Domain.Filters;
using Sampaio.Domain.Queries.Establishment;
using Sampaio.Api.Config;
using Sampaio.Domain.Commands.BackofficeUser;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Security;

namespace Sampaio.Api.Controllers.Backoffice.V1
{ 
    // [ApiExplorerSettings(GroupName = "backoffice:v1")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/backoffice/")]
    public class BackofficeUserController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly SessionUser _sessionUser;
        public BackofficeUserController(IMediator mediator, ILoggedUser loggedUser, IDomainNotification notifications) : base(mediator, loggedUser, notifications)
        {
            _mediator = mediator;
            _sessionUser = loggedUser.User;
        }
    
        
    }
}