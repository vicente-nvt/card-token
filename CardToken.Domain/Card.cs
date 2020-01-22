using System;

namespace CardToken.Domain
{
    public class Card
    {
        public string Number { get; }
        public string Cvv { get; }
        public DateTime RegistrationData { get; }

        public Card(string number, string cvv)
        {
            Validation.When(string.IsNullOrWhiteSpace(number)).ThenThrows("Card number is missing");
            Validation.When(string.IsNullOrWhiteSpace(cvv)).ThenThrows("CVV is missing");

            Number = number;
            Cvv = cvv;
            RegistrationData = DateTime.Now;
        }
    }
}
