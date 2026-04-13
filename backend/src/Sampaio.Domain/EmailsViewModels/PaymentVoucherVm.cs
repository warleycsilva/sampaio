using System;
using System.Collections.Generic;

namespace Sampaio.Domain.EmailsViewModels
{
    public class PaymentVoucherVm
    {
        public PaymentVoucherVm(string id,string clientName, string establishmentName, string email,  string paymentValue, string paymentType, DateTime paymentDatetime, List<string[]> movements)
        {
            Id = id;
            ClientName = clientName;
            EstablishmentName = establishmentName;
            Email = email;
            PaymentValue = paymentValue;
            PaymentType = paymentType;
            PaymentDatetime = paymentDatetime;
            Movements = movements;
        }

        public string Id { get; set; }
        public string ClientName { get; set; }
        
        public string EstablishmentName { get; set; }
        public string Email { get; set; }
        
        public string Subject { get; set; }
        
        public string PaymentValue { get; set; }
        
        public string PaymentType { get; set; }

        public DateTime PaymentDatetime { get; set; }
        public List<string[]> Movements { get; set; }
    }
}