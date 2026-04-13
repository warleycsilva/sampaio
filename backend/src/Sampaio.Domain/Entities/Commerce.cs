using System;
using System.Collections.Generic;
using Sampaio.Domain.Commands.Commerce;
using Sampaio.Domain.ValueObjects;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.Entities
{
    public class Commerce : BaseEntity
    {
        protected Commerce()
        {
        }
        
        public User User { get; private set; }
        
        public Identification Identification { get; private set; }

        public AddressInformation AddressInformation { get; private set; }

        public string Name { get; private set; }
        public CommerceSegment Segment { get; private set; }
        public Guid? SegmentId { get; private set; }
        public bool? HasParking { get; private set; }

        public bool? Homologated { get; private set; } = false;
        

        public string Latitude { get; private set; }
        public string Longitude { get; private set; }
        public ICollection<Product> Products { get; private set; } = new List<Product>();
        public ICollection<Sale> Sales { get; private set; } = new List<Sale>();
        public IEnumerable<Cart> Carts { get; set; } = new List<Cart>();

        public static Commerce New()
        {
            return new Commerce
            {
                CreatedAt = DateTime.UtcNow,
            };
        }

        public Commerce Update(Identification? identification = null, AddressInformation? addressInformation = null)
        {
            Identification = identification ?? Identification;
            AddressInformation = addressInformation ?? AddressInformation;
            return this;
        }
        public Commerce UpdateIdentification(Identification identification)
        {
            Identification = identification;
            return this;
        }
        public Commerce UpdateAddress(_AddressInformation addressInformation)
        {
            AddressInformation = addressInformation;
            Latitude = addressInformation.Lat;
            Longitude = addressInformation.Long;
            return this;
        }
        
        public Commerce UpdateLocation(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            return this;
        }

        public void SetSegment(Guid segmentId)
        {
            SegmentId = segmentId;
        }
    }
}