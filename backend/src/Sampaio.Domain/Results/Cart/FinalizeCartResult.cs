using System;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Results.Cart
{
    public class FinalizeCartResult
    {
        public bool Success { get; set; } = false;
        
        public string Message { get; set; }
        public SaleVm Sale { get; set; }
    }
}