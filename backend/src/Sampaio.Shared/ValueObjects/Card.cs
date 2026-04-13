using Sampaio.Shared.Enums;

namespace Sampaio.Shared.ValueObjects {
    public class Card {
        public Card () { }
        public string Number { get; set; }
        public string Holder { get; set; }
        public string ExpirationDate { get; set; }

        public string SecurityCode { get; set; }
        public string CardBrand { get; set; }
        public ECardType Type { get; set; }
    }
}