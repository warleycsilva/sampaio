using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Results.CartItem
{
    public class RemoveCartItemResult
    {
        public bool Success { get; set; } = false;
        
        public string Message { get; set;}
        
        public CartVm Cart { get; set;}
    }
}