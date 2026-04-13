using System;

namespace Sampaio.Domain.Results.Viagens
{
    public class ViagemPagamentoBaseResult : ViagemBaseResult
    {
        public string IdTransaction { get; set; }
        public string PixQrCode { get; set; }
    }
}