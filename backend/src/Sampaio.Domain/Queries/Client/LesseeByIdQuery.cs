using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Queries.Client
{
    public class ClientByIdQuery : IRequest<ClientVm>
    {
        public Guid Id { get; set; }
        [JsonIgnore] public SessionUser SessionUser { get; set; }
    }
}