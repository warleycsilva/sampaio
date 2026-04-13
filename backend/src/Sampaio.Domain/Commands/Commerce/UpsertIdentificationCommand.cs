using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Commerce;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Commands.Commerce
{
    public class UpsertIdentificationCommand : IRequest<BaseCommerceResult>
    {
        [Required]
        public Identification Identification { get;  set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}