using System;
using System.Linq;

namespace CardToken.Domain
{
    public class Card
    {
        public string Number { get; }
        public string Cvv { get; }
        public DateTime RegistrationDate { get; }
        public string Token { get; private set; }

        public Card(string cardNumber, string cvv)
        {
            Validation.When(string.IsNullOrWhiteSpace(cardNumber)).ThenThrows("Card number is missing");
            Validation.When(cardNumber.Length > 16).ThenThrows("Invalid card number");
            Validation.When(string.IsNullOrWhiteSpace(cvv)).ThenThrows("CVV is missing");
            Validation.When(cvv.Length > 5).ThenThrows("Invalid cvv");

            Number = cardNumber;
            Cvv = cvv;
            RegistrationDate = DateTime.UtcNow;
            CalculateToken();
        }

        private void CalculateToken() {
            var dataForToken = $@"{Number}
                                  {RegistrationDate.Year.ToString("YYYY")}
                                  {RegistrationDate.Month.ToString("mm")}
                                  {RegistrationDate.Day.ToString("DD")}
                                  {RegistrationDate.Hour.ToString("HH")}
                                  {RegistrationDate.Minute.ToString("MM")}";

            var arrayOfString = dataForToken.Split();
            var arrayOfInteger = arrayOfString.Select(item => Int32.Parse(item)).ToArray();
            var absoluteDifference = 5;
            var shortArray = ArrayShortener.GetByAbsoluteDifference(arrayOfInteger, absoluteDifference);
            var numberOfRotations = Int32.Parse(Cvv);
            var token = ArrayRotation.RotateArray(shortArray, numberOfRotations);

            Token = string.Join("", token.Select(item => item.ToString()));
        }
    }
}
