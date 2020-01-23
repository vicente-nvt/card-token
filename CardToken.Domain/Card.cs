using CardToken.Domain.TokenGeneration;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CardToken.Domain
{
    public class Card
    {
        public string Number { get; }
        public string Cvv { get; }
        public DateTime RegistrationDate { get; }
        public string Token { get; private set; }
        public int AbsoluteDifference => 5;

        public Card(string cardNumber, string cvv)
        {
            Validation.When(string.IsNullOrWhiteSpace(cardNumber))
                .OrWhen(cardNumber != null && cardNumber.Length > 16)
                .OrWhen(cardNumber != null && Regex.Matches(cardNumber, @"[a-zA-Z]").Count > 0)
                .ThenThrows("Invalid card number");

            Validation.When(string.IsNullOrWhiteSpace(cvv))
                .OrWhen(cvv != null && cvv.Length > 5)
                .OrWhen(cvv != null && Regex.Matches(cvv, @"[a-zA-Z]").Count > 0)
                .ThenThrows("Invalid cvv");

            Number = cardNumber;
            Cvv = cvv;
            RegistrationDate = DateTime.UtcNow;
            CalculateToken();
        }

        private void CalculateToken() {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(Number);
            stringBuilder.Append(RegistrationDate.Year.ToString());
            stringBuilder.Append(RegistrationDate.Month.ToString("d2"));
            stringBuilder.Append(RegistrationDate.Day.ToString("d2"));
            stringBuilder.Append(RegistrationDate.Hour.ToString("d2"));
            stringBuilder.Append(RegistrationDate.Minute.ToString("d2"));
            var dataForToken = stringBuilder.ToString();

            int numberOfRotations = int.Parse(Cvv);
            Token = TokenGenerator.Generate(dataForToken, numberOfRotations, AbsoluteDifference);
        }
    }
}