using System;
using Sampaio.Shared.Paging;

namespace Sampaio.Domain.Filters
{
    public class PassageiroFilter : Pagination
    {
        public Guid? PassageiroId { get; set; }
        
        public Guid? ViagemId { get; set; }
        
        public string Nome { get; set; }
        
        public string Documento { get; set; }
        
        public string Telefone { get; set; }
        
        public bool? Comprador { get; set; }
        
        public int? Assento { get; set; }
    }
}