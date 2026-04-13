using System.Linq;
using Sampaio.Domain.ValueObjects;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Projections
{
    public static class AddressInformationProjections
    {
        
        public static AddressInformationVm ToVm(this AddressInformation addressInformation) =>
        new AddressInformationVm
        {
            Address = addressInformation.Address,
            Complement = addressInformation.Complement,
            Number = addressInformation.Number,
            Neighborhood = addressInformation.Neighborhood,
            ZipCode = addressInformation.ZipCode,
            City = addressInformation?.City?.ToVm(),
            State = addressInformation?.State?.ToVm()
        };

        public static IQueryable<AddressInformationVm> ToVm(this IQueryable<AddressInformation> query) =>
            query.Select(addressInformation => new AddressInformationVm
            {
                Address = addressInformation.Address,
                Complement = addressInformation.Complement,
                Number = addressInformation.Number,
                Neighborhood = addressInformation.Neighborhood,
                ZipCode = addressInformation.ZipCode,
                City = addressInformation.City.ToVm(),
                State = addressInformation.State.ToVm()
            });
    }
}