using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Results.Cart
{
    public class AbandonCartResult
    {
        public bool Success { get; set; } = false;
        
        public string Message { get; set;}
    }
}