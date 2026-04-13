using System;
using Sampaio.Shared.Security;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Queries.CommerceSegment
{
    public class GetCommerceSegmentByIdQuery : IRequest<CommerceSegmentVm>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}