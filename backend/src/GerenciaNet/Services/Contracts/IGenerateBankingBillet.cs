using System.Threading.Tasks;
using GerenciaNet.Models.Request;

namespace GerenciaNet.Services.Contracts
{
    public interface IGenerateBankingBilletService
    {
        Task<dynamic> GenerateBankingBillet(GenerateBankingBilletRequest request);
    }
}