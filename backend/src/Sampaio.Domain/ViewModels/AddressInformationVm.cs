namespace Sampaio.Domain.ViewModels
{
    public class AddressInformationVm
    {
        public string Address { get; set; }

        
        public string Number { get; set; }

        
        public string Complement { get; set; }

        
        public string Neighborhood { get; set; }

        
        public string ZipCode { get; set; }

        
        public CityVm City { get; set; }
        
        
        public StateVm State { get; set; }
        
    }
}