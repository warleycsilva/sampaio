using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Commerce;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Commands.Commerce
{
    public class UpsertAvatarCommand : IRequest<BaseCommerceResult>
    {
        public FileInput File { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}