using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sampaio.Api.Config;
using Sampaio.Domain.Commands.Driver;
using Sampaio.Domain.Commands.ViagemPagamentos;
using Sampaio.Domain.Commands.Viagens;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Filters;
using Sampaio.Domain.Queries.Driver;
using Sampaio.Domain.Queries.Passageiro;
using Sampaio.Domain.Queries.Viagens;
using Sampaio.Domain.Results.Driver;
using Sampaio.Domain.ViewModels;
using Sampaio.Domain.ViewModels.Viagens;
using Sampaio.Shared.Constants;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Results;
using Sampaio.Shared.Security;
using Swashbuckle.AspNetCore.Annotations;

namespace Sampaio.Api.Controllers.Public.V1
{
    [ApiExplorerSettings(GroupName = "public:v1")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/viagens")]
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
  
            filter.DataPartida = filter?.DataPartida is null || filter?.DataPartida < DateTime.UtcNow ? DateTime.UtcNow: filter?.DataPartida;
            filter.IsActive = true;
            var viagens = await _mediator.Send(new GetViagensQuery()
            {
                Filter = filter
            }, CancellationToken.None);
            viagens.Itens = viagens.Itens.Where(v => v.VagasVendidas < v.QtdVagas);
            return CreateResponse(viagens);
        }
        
        //TODO: Última Última prioridade - Adicionar endpoint de reenviar email de compra da viagem por "email" do comprador
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
        
        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(PagedList<ViagemVm>))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetByEmail([FromRoute] string email, [FromQuery] ViagemFilter filter)
        {
            filter.Email = email;
            var query = new GetPassageiroViagemByEmailQuery()
            {
                Filter = filter,
            };
            return CreateResponse(await _mediator.Send(query, CancellationToken.None));
        }
        
        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(PagedList<ViagemPagamentoVm>))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpGet("by-email/{email}/last")]
        public async Task<IActionResult> GetLastByEmail([FromRoute] string email, [FromQuery] ViagemFilter filter)
        {
            filter.Email = email;
            var query = new GetLastPassageiroViagemByEmailQuery()
            {
                Filter = filter,
            };
            return CreateResponse(await _mediator.Send(query, CancellationToken.None));
        }
        
        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(PagedList<ViagemVm>))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpGet("by-cpf/{cpf}")]
        public async Task<IActionResult> GetByCpf([FromRoute] string cpf, [FromQuery] ViagemFilter filter)
        {
            filter.Cpf = cpf;
            var query = new GetPassageiroViagemByEmailQuery()
            {
                Filter = filter,
            };
            return CreateResponse(await _mediator.Send(query, CancellationToken.None));
        }


        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(ViagemPagamentoVm))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpPost("{id:guid}/comprar")]
        public async Task<IActionResult> Get(
            [FromRoute] Guid id,
            [FromBody] CriarViagemPagamentoCmd request)
        {
            request.ViagemId = id;
            return CreateResponse(await _mediator.Send(request, CancellationToken.None));
        }
        
        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(ViagemPagamentoVm))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpGet("pagamento/{id:guid}")]
        public async Task<IActionResult> SyncStatusPagamento([FromRoute] Guid id)
        {
            return CreateResponse(await _mediator.Send(new SyncStatusPagamentoCommand()
            {
                PagamentoId = id
            }, CancellationToken.None));
        }
        
        [SwaggerResponse((int) HttpStatusCode.OK, StatusCodeDescriptions.Ok, typeof(ViagemPagamentoVm))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, StatusCodeDescriptions.BadRequest, typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.UnprocessableEntity, StatusCodeDescriptions.UnprocessableEntity,
            typeof(EnvelopFailResult))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, StatusCodeDescriptions.InternalServerError,
            typeof(EnvelopFailResult))]
        [HttpPut("{id:guid}/estornar")]
        public async Task<IActionResult> Estornar([FromRoute] Guid id)
        {
            return CreateResponse(await _mediator.Send(new EstornarViagemCmd()
            {
                Id = id
            }, CancellationToken.None));
        }
        
    }
}