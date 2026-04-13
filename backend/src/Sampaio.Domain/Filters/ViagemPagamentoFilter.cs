using System;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class ViagemPagamentoFilter : Pagination
    {
        public Guid? Id { get; set; }
        
        public Guid? ViagemId { get; set; }
        
        public string? IdTransacao { get; set; }
        
        public DateTime? DataDe { get; set; }
        
        public DateTime? DataAte { get; set; }
        
        public EPaymentStatus? Status { get; set; }
        
        public string Email { get; set; }
        
        public long Cpf { get; set; }
    }
}