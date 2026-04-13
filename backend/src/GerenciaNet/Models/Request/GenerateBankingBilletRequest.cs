using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace GerenciaNet.Models.Request
{
    public class GenerateBankingBilletRequest
    {
        
         [JsonProperty(PropertyName = "items")]
        public IEnumerable<Item> Items { get; set; } = new List<Item>();
        
         [JsonProperty(PropertyName = "payment")]
        public Payment Payment { get; set; }   
    }

    public class Item
    {
         [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
         [JsonProperty(PropertyName = "value")]
        public int Value { get; set; }
        
         [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }
    }

    public class Payment
    {
        [JsonProperty(PropertyName = "banking_billet")]
        public BankingBillet BankingBillet { get; set; }   
    }

    public class BankingBillet
    {
         [JsonProperty(PropertyName = "expire_at")]
        public string ExpireAt { get; set; }
        
         [JsonProperty(PropertyName = "customer")]
        public Customer Customer { get; set; }
    }

    public class Customer
    {
         [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
         [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        
         [JsonProperty(PropertyName = "cpf")]
        public string Cpf { get; set; }
        
        //YYYY-MM-DD
         [JsonProperty(PropertyName = "birth")]
        public string Birth { get; set; }
        
        //31912345678
         [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; set; }
    }
}