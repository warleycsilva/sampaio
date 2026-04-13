using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Commerce;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Commands.Commerce
{
    public class SelectCommerceSegmentCommand : IRequest<SelectCommerceSegmentResult>
    {
        [Required]
        public Guid CommerceSegmentId { get; set; }
        
        [JsonIgnore]
        public SessionUser SessionUser { get;  set; }
    }
}