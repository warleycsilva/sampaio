
using System;
using System.Collections.Generic;
using Sampaio.Domain.Entities;
using Sampaio.Shared.Enums;
using Sampaio.Shared.ValueObjects;

namespace Sampaio.Domain.ViewModels
{
    public class EstablishmentVm : BaseVm
    {
        public Identification Identification { get;  set; }

        public AddressInformationVm AddressInformation { get;  set; }

        public bool TermsAreAccepted { get;  set; }

        public UserVm User { get;  set; }

        public string AddressProof { get; set; }

        public string ActivityPhoto { get; set; }

        public string ActivityLink { get; set; }

        public string BankProof { get; set; }

        public string CpfPhoto { get; set; }

        public string SocialContract { get; set; }

        public string CnpjCard { get; set; }

        public string FantasyName { get; set; }

        public string CompanyName { get; set; }

        public string HolderCpf { get; set; }

        public string HolderName { get; set; }

        public bool PaymentTermsAccepted { get; set; }

        public string PaymentTermsAcceptIpAddress { get; set; }

        public DateTime? PaymentTermsAcceptTimestamp { get; set; }

        public string PaymentReceiveOption { get; set; }
    }
}
