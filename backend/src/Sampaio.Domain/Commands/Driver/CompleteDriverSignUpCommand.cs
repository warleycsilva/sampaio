using Sampaio.Domain.ValueObjects;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Sampaio.Domain.Results.Driver;
using Sampaio.Shared.Security;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Commands.Driver
{
    public class CompleteDriverSignUpCommand : IRequest<DriverSignUpResult>
    {
        [Required]
        public Identification Identification { get; set; }

        public List<Phone> Phones { get; set; }

        [JsonIgnore]
        public SessionUser SessionUser { get; set; }

    }
}
