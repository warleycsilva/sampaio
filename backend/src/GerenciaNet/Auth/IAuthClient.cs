using System.Threading.Tasks;
using Sampaio.Shared.Config;

namespace GerenciaNet.Auth
{
    public interface IAuthClient
    {
        Task<string> CreateAccessTokenAsync(GerenciaNetConfig config);
    }
}