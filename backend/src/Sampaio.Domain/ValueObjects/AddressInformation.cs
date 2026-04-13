using System;
using System.Linq;
using Sampaio.Domain.Entities;

namespace Sampaio.Domain.ValueObjects
{
    public class AddressInformation 
    {
        public AddressInformation()
        {
            Address = string.Empty;
            Number = string.Empty;
            Complement = string.Empty;
            Neighborhood = string.Empty;
            ZipCode = string.Empty;
        }
        
        public string Address { get; set; }

        
        public string Number { get; set; }

        
        public string Complement { get; set; }

        
        public string Neighborhood { get; set; }

        
        public string ZipCode { get; set; }

        
        public City City { get; set; }
        
        public Guid CityId { get; set; }
        
        public State State { get; set; }
        
        public string StateId { get; set; }
        
        public string FormattedZipCode
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ZipCode))
                {
                    return string.Empty;
                }
                if (ZipCode.Trim().Length == 8)
                {
                    var value = Convert.ToUInt64(ZipCode);
                    return value.ToString(@"00000\-000");
                }
                return ZipCode;
            }
        }

        
        public string Complete
        {
            get
            {
                var value = new[]
                {
                    Address, Number, Complement, Neighborhood, FormattedZipCode,
                    !string.IsNullOrWhiteSpace(ZipCode) && !string.IsNullOrWhiteSpace(State?.Name) ? $"{City?.Name}-{State?.Name}" :
                    !string.IsNullOrWhiteSpace(ZipCode) ? City?.Name : ""
                };

                return string.Join(", ", value.Where(x => !string.IsNullOrWhiteSpace(x)));
            }
        }

        public void Update(AddressInformation addressInformation)
        {
            Address = addressInformation?.Address ?? string.Empty;
            City = addressInformation?.City ?? City;
            Complement = addressInformation?.Complement ?? string.Empty;
            Neighborhood = addressInformation?.Neighborhood ?? string.Empty; 
            Number = addressInformation?.Number ?? string.Empty;
            State = addressInformation?.State ?? State;
            ZipCode = addressInformation?.ZipCode ?? string.Empty;
        }
    }
}
