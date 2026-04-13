using System.Collections.Generic;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.ViewModels
{
    public class CommerceVm : BaseVm
    {
        public string Name { get; set; }
        
        public Identification Identification { get;  set; }

        public AddressInformationVm AddressInformation { get;  set; }

        public bool TermsAreAccepted { get;  set; }

        public UserVm User { get; set; } = new UserVm();
        
        public CommerceSegmentVm? CommerceSegment { get;  set; }
        
        public IEnumerable<ProductVm>? Products { get; set; } = new List<ProductVm>();
        
        public bool? HasParking { get; set; }

        public bool? Homologated { get; set; } = false;
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}