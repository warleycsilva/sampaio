using System;
using System.Collections.Generic;
using System.Text;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.ViewModels
{
    public class DriverVm : BaseVm
    {
        public string CnhNumber { get; set; }

        public Identification Identification { get; set; }

        public bool TermsAreAccepted { get; set; }

        public UserVm User { get; set; }

        public DateTime? BirthDate { get; set; }
        
        public PlanVm? Plan { get; set; }
    }
}
