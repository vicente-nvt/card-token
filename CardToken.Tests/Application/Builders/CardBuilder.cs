using CardToken.Domain;

namespace CardToken.Tests.Application.Builders
{
    public class CardBuilder
    {
        public string CardNumber { get; private set; }
        public string Cvv { get; private set; }

        public static CardBuilder New()
        {
            return new CardBuilder();
        }

        public CardBuilder WithNumber(string cardNumber)
        {
            CardNumber = cardNumber;
            return this;
        }

        public CardBuilder WithCvv(string cvv)
        {
            Cvv = cvv;
            return this;
        }

        public Card Build()
        {
            return new Card(CardNumber, Cvv);
        }
    }
}
