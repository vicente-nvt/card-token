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
        public DateTime RegistrationDateTime { get; }
        public string Token { get; private set; }
        public int AbsoluteDifference => 5;

        public Card(string cardNumber, string cvv)
        {
            DomainValidation.When(string.IsNullOrWhiteSpace(cardNumber))
                .OrWhen(cardNumber != null && cardNumber.Length > 16)
                .OrWhen(cardNumber != null && Regex.Matches(cardNumber, @"[a-zA-Z]").Count > 0)
                .ThenThrows("Invalid card number");

            DomainValidation.When(string.IsNullOrWhiteSpace(cvv))
                .OrWhen(cvv != null && cvv.Length > 5)
                .OrWhen(cvv != null && Regex.Matches(cvv, @"[a-zA-Z]").Count > 0)
                .ThenThrows("Invalid cvv");

            Number = cardNumber;
            Cvv = cvv;
            RegistrationDateTime = DateTime.UtcNow;
            FillCardToken();
        }

        private void FillCardToken() {
            Token = CalculateToken(Number, RegistrationDateTime, int.Parse(Cvv));
        }

        private string CalculateToken(string number, DateTime registrationDateTime, int cvv)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(number);
            stringBuilder.Append(registrationDateTime.Year.ToString());
            stringBuilder.Append(registrationDateTime.Month.ToString("d2"));
            stringBuilder.Append(registrationDateTime.Day.ToString("d2"));
            stringBuilder.Append(registrationDateTime.Hour.ToString("d2"));
            stringBuilder.Append(registrationDateTime.Minute.ToString("d2"));
            var dataForToken = stringBuilder.ToString();

            int numberOfRotations = cvv;

            return TokenGenerator.Generate(dataForToken, numberOfRotations, AbsoluteDifference);
        }

        public bool CheckIfTokenIsValid(string token, int cvv, DateTime registrationDateTime)
        {
            var tokenGenerated = CalculateToken(Number, registrationDateTime, cvv);

            return tokenGenerated.Equals(token);
        }
    }
}