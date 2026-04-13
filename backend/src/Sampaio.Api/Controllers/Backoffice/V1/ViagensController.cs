using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampaio.Api.Config;
using Sampaio.Domain.Commands.Driver;
using Sampaio.Domain.Commands.Viagens;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Filters;
using Sampaio.Domain.Queries.Driver;
using Sampaio.Domain.Queries.Viagens;
using Sampaio.Domain.Results.Driver;
using Sampaio.Domain.Results.Viagens;
using Sampaio.Domain.ViewModels;
using Sampaio.Domain.ViewModels.Viagens;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Results;
using Sampaio.Shared.Security;
using Swashbuckle.AspNetCore.Annotations;

namespace Sampaio.Api.Controllers.Backoffice.V1
{
    [ApiExplorerSettings(GroupName = "backoffice:v1")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/backoffice/viagens")]
    public class ViagensController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly SessionUser _sessionUser;
        public ViagensController(IMediator mediator, ILoggedUser loggedUser, IDomainNotification notifications) : base(mediator, loggedUser, notifications)
        {
            _mediator = mediator;
            _sessionUser = loggedUser.User;
        }
        
        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(PagedList<ViagemVm>))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] ViagemFilter filter)
        {
            filter.DataPartida = filter.DataPartida.HasValue ? filter?.DataPartida.Value.AddHours(12) : null;
            return CreateResponse(await _mediator.Send(new GetViagensQuery()
            {
                Filter = filter
            }, CancellationToken.None));
        }


        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(ViagemVm))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) =>
            CreateResponse(await _mediator.Send(new GetViagemByIdQuery()
            {
                Id = id
            }, CancellationToken.None));
        
        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(ViagemVm))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpPatch("{id:guid}/is-active")]
        public async Task<IActionResult> AlterarStatusAtivoViagem([FromRoute] Guid id) =>
            CreateResponse(await _mediator.Send(new AlterarStatusAtivoViagemCmd()
            {
                Id = id
            }, CancellationToken.None));
        
        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(ViagemBaseResult))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpPost()]
        public async Task<IActionResult> Get([FromBody] CriarViagemCmd request) =>
            CreateResponse(await _mediator.Send(request, CancellationToken.None));
        
        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(ViagemBaseResult))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Get([FromBody] EditarViagemCmd request, [FromRoute] Guid id)
        {
            request.Id = id;
            return CreateResponse(await _mediator.Send(request, CancellationToken.None));
        }
        
        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(ViagemBaseResult))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpPut("{id:guid}/estornar")]
        public async Task<IActionResult> Estornar([FromBody] EstornarViagemCmd request, [FromRoute] Guid id)
        {
            request.Id = id;
            return CreateResponse(await _mediator.Send(request, CancellationToken.None));
        }
        
        
        //TODO: #frontend Backoffice precisa ver assentos ocupados e quantidade de disponíveis - serviço pronto no backend;
        //TODO: #frontend Backoffice precisa ver valor total pago 
        //TODO: #frontend Backoffice precisa ver lista de passageiros e para cada passageiro qual o cpf e email do comprador - Dados do passageiro já enviando Email e cpf;
        
    }
}