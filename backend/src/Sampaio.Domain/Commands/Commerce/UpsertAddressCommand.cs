using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Newtonsoft.Json;
using Sampaio.Domain.Results.Commerce;
using Sampaio.Domain.ValueObjects;

namespace Sampaio.Domain.Commands.Commerce
{
    public class UpsertAddressCommand : IRequest<BaseCommerceResult>
    {
        public UpsertAddressCommand()
        {
            Address = new _AddressInformation();
        }
        [Required]
        public _AddressInformation Address { get;  set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
    }

    public class _AddressInformation : AddressInformation
    {
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}