using System;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.CommerceSegment;

namespace Sampaio.Domain.Commands.CommerceSegment
{
    public class DeleteCommerceSegmentCommand : IRequest<DeleteCommerceSegmentResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}