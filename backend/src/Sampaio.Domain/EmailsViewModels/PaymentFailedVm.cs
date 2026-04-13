using System.Collections.Generic;

namespace Sampaio.Domain.EmailsViewModels
{
    public class PaymentFailedVm
    {
        public PaymentFailedVm(string id,string clientName, string establishmentName, string email,  string paymentValue, string paymentType, string paymentDatetime)
        {
            Id = id;
            ClientName = clientName;
            EstablishmentName = establishmentName;
            Email = email;
            PaymentValue = paymentValue;
            PaymentType = paymentType;
            PaymentDatetime = paymentDatetime;
        }

        public string Id { get; set; }
        public string ClientName { get; set; }
        
        public string EstablishmentName { get; set; }
        public string Email { get; set; }
        
        public string Subject { get; set; }
        
        public string PaymentValue { get; set; }
        
        public string PaymentType { get; set; }

        public string PaymentDatetime { get; set; }
    }
}