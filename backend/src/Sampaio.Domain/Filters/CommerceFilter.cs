using System;
using System.Text.Json.Serialization;
using Sampaio.Shared.Security;
using Sampaio.Domain.ValueObjects;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class CommerceFilter : Pagination
    {
        public string Name { get; set; }

        public string DocumentNumber { get; set; }
        
        public Guid? CommerceSegmentId { get; set; }
        
        public AddressInformation AddressInformation { get; set; }

        public EUserType? UserType { get; set; } = EUserType.Driver;

    }
}