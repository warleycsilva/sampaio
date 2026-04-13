using System.Net.Http;
using System.Threading.Tasks;
using Sampaio.Domain.Projections;
using Sampaio.Domain.ViewModels;
using Sampaio.Domain.Contracts.Repositories;
using Sampaio.Domain.Contracts.Services;
using Sampaio.Domain.Entities;
using Sampaio.Domain.Services.Returns;
using Sampaio.Domain.ValueObjects;
using Sampaio.Shared.Utils;

namespace Sampaio.Domain.Services
{
    public class ZipCodeService : IZipCodeService
    {
        private readonly BaseHttpClient _baseHttpClient;
        private readonly ICityRepository _cityRepository;

        public ZipCodeService(BaseHttpClient baseHttpClient, ICityRepository cityRepository)
        {
            _baseHttpClient = baseHttpClient;
            _cityRepository = cityRepository;
        }

        public async Task<AddressInformation> GetAdressByZipcode(string cep)
        {
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, $"https://viacep.com.br/ws/{cep}/json/"))
                {
                    var response = await _baseHttpClient.Client.SendAsync(request);

                    var resultContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var addressResult = JsonUtils.Deserialize<ConsultCepReturn>(resultContent);
                        if (string.IsNullOrEmpty(addressResult.Uf)) return null;
                        var includes = new IncludeHelper<City>().Include(x => x.State).Includes;

                        var city = await _cityRepository.FindAsync(
                            x => x.Name.ToLower() == addressResult.Localidade.ToLower() &&
                                 x.State.Name.ToLower() == addressResult.Uf.ToLower(), includes);
                        return new AddressInformation
                        {
                            CityId = city.Id,
                            StateId = city.StateId,
                            Address = addressResult.Logradouro,
                            Neighborhood = addressResult.Bairro,
                            ZipCode = addressResult.Cep,
                            City = city,
                        };
                    }
                    return null;
                }
            }
        }
    }
}