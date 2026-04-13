using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Results.Commerce
{
    public class BaseCommerceResult
    {
        public string Message { get; set; }
        public bool Success { get; set; } = false;
        public CommerceVm Commerce { get; set; }
    }
}