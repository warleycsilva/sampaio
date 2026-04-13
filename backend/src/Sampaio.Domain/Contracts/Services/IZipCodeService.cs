using System.Threading.Tasks;
using Sampaio.Domain.ViewModels;
using Sampaio.Domain.ValueObjects;

namespace Sampaio.Domain.Contracts.Services
{
    public interface IZipCodeService
    {
        Task<AddressInformation> GetAdressByZipcode(string cep);
    }
}