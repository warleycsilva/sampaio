using System;

namespace Sampaio.Domain.Results.CommerceSegment
{
    public class CreateCommerceSegmentResult
    {
        public Guid CommerceSegmentId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}