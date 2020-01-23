using System;

namespace CardToken.Domain
{
    public class Card
    {
        public string Number { get; }
        public string Cvv { get; }
        public DateTime RegistrationData { get; }

        public Card(string cardNumber, string cvv)
        {
            Validation.When(string.IsNullOrWhiteSpace(cardNumber)).ThenThrows("Card number is missing");
            Validation.When(cardNumber.Length > 16).ThenThrows("Invalid card number");
            Validation.When(string.IsNullOrWhiteSpace(cvv)).ThenThrows("CVV is missing");
            Validation.When(cvv.Length > 5).ThenThrows("Invalid cvv");

            Number = cardNumber;
            Cvv = cvv;
            RegistrationData = DateTime.Now;
        }
    }
}
