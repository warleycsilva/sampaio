using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Product;
using Sampaio.Shared.Security;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Commands.Product
{
    public class UpdateProductPhotoCommand : IRequest<UpdateProductPhotoResult>
    {
        [Required]
        public FileInput FileInput { get; set; }
        
        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}