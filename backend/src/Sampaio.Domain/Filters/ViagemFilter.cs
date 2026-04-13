using System;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class ViagemFilter : Pagination
    {
        public Guid? ViagemId { get; set; }
        
        public DateTime? DataPartida { get; set; }
        
        public string Origem { get; set; }
        
        public string Destino { get; set; }
        public string Email { get; set; }
        public string? Cpf { get; set; }
        public bool? IsActive { get; set; }
    }
}