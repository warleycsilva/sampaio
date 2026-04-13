using System;
using System.Text.Json.Serialization;
using Sampaio.Shared.Enums;
using Sampaio.Shared.Paging;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Filters
{
    public class DebtFilter : Pagination
    {
        public Guid? DriverId { get; set; }
        public EDebtStatus? Status { get; set; }
        public EDebtType? Type { get; set; }
        
        
        [JsonIgnore]
        public SessionUser SessionUser { get; set; }
    }
}
