using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Commerce;
using Sampaio.Domain.ValueObjects;
using Sampaio.Shared.Security;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Commands.Commerce
{
    public class CompleteCommerceSignUpCommand : IRequest<CommerceSignupResult>
    {
        [Required]
        public AddressInformation AddressInformation { get;  set; }
        
        [Required]
        public Identification Identification { get;  set; }
        
        public List<Phone> Phones { get; set; }
        
        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}