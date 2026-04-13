using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.CommerceSegment;

namespace Sampaio.Domain.Commands.CommerceSegment
{
    public class UpdateCommerceSegmentCommand : IRequest<UpdateCommerceSegmentResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}