using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.CartItem;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Commands.CartItem
{
    public class CreateCartItemCommand : IRequest<CreateCartItemResult>
    {
        [Required]
        public Guid ProductId { get; set; }
        
        [Required]
        public int Quantity { get; set; }

        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}