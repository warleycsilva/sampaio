using System.Threading.Tasks;

namespace Sampaio.Domain.Contracts.Infra
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}