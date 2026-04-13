using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sampaio.Api.Config;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.Contracts.Services;
using Sampaio.Domain.Queries.City;
using Sampaio.Domain.Queries.State;
using Sampaio.Shared.Notifications;

namespace Sampaio.Api.Controllers.Common.V1
{
    [ApiExplorerSettings(GroupName = "common:v1")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/states")]
    public class StatesController: BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IZipCodeService _zipCodeService;
        public StatesController(IMediator mediator, ILoggedUser loggedUser, IDomainNotification notifications, IZipCodeService zipCodeService) : base(mediator, loggedUser, notifications)
        {
            _mediator = mediator;
            _zipCodeService = zipCodeService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetStates() =>
            CreateResponse(await _mediator.Send(new ListStateQuery(), CancellationToken.None));

        [HttpGet("{id}/cities")]
        public async Task<IActionResult> GetCitiesByState([FromRoute] string id) =>
            CreateResponse(await _mediator.Send(new ListCityByStateQuery {StateId = id}));

        [HttpGet("cep/{cep}")]
        public async Task<IActionResult> GetAddressByCep([FromRoute] string cep) =>
            CreateResponse(await _zipCodeService.GetAdressByZipcode(cep));
    }
}