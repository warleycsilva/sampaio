using System;

namespace Sampaio.Domain.ViewModels
{
    public class BaseVm
    {
        public Guid? Id { get; set; }

        public DateTime? CreatedAt { get; set; }
       
        public bool? Deleted { get; set; }
        
        public object Clone() => this.MemberwiseClone();
    }
}