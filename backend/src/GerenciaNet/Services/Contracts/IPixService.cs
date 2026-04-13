using System.Threading.Tasks;
using GerenciaNet.Models.Request;
using GerenciaNet.Models.Response;

namespace GerenciaNet.Services.Contracts
{
    public interface IPixService
    {
        Task<PixResponse> CreatePixRequest(CobRequest request);
        Task<CobInfoResponse> CheckCob(CobInfoRequest request);
    }
}