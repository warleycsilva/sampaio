using System.ComponentModel.DataAnnotations;
using MediatR;
using Sampaio.Domain.Results.CommerceSegment;

namespace Sampaio.Domain.Commands.CommerceSegment
{
    public class CreateCommerceSegmentCommand : IRequest<CreateCommerceSegmentResult>
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}