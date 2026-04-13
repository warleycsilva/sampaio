using System;
using Sampaio.Domain.ViewModels.Viagens;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.Results.Viagens
{
    public class ViagemBaseResult
    {
        public string Message { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; } = false;
        public EPaymentStatus? Status { get; set; }
        public Guid? ViagemId { get; set; }
        public ViagemVm? Viagem { get; set; }
    }
}