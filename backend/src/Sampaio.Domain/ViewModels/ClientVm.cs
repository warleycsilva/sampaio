using System;
using Sampaio.Domain.ValueObjects;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.ViewModels
{
    public class ClientVm : BaseVm
    {
        public UserVm User { get; set; }
        
        public AddressInformationVm AddressInformation { get; set; }
        
        public Identification Identification { get; set; }
        
        public DateTime? Birthdate { get; set; }
        
        public string CnhNumber { get; set; }
        
        public bool IsTaxiDriver { get; set; }
        
        public DateTime? RatrExpirationDate { get; set; }
        
        public string Ratr { get; set; }
        
        public string Inss { get; set; }
        
        public bool TermsAreAccepted { get; set; }
    }
}