using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Commerce;

namespace Sampaio.Domain.Commands.Commerce
{
    public class UpsertPhoneCommand : IRequest<BaseCommerceResult>
    {
        public string Phone { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}