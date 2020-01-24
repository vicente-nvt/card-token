using System;
using System.Text.RegularExpressions;

namespace CardToken.Application.CardValidation
{
    public class CardValidator : CardValidation
    {
        private CardRepository _cardRepository;

        public CardValidator(CardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public ValidationDTO Validate(string token, string cvv, string registrationDateTime)
        {
            CheckInputs(token, cvv, registrationDateTime);

            var registrationDateTimeConverted = Convert.ToDateTime(registrationDateTime);

            if (!CheckRegistrationDateLimit(registrationDateTimeConverted))
                return new ValidationDTO { Validated = false };

            var card = _cardRepository.GetCardByRegistrationDate(registrationDateTime);

            return new ValidationDTO
            {
                Validated = card != null && card.CheckIfTokenIsValid(token, int.Parse(cvv), registrationDateTimeConverted)
            };
        }

        private static void CheckInputs(string token, string cvv, string registrationDateTime)
        {
            ApplicationValidation.When(string.IsNullOrWhiteSpace(token))
                .OrWhen(token != null && Regex.Matches(token, @"[a-zA-Z]").Count > 0)
                .ThenThrows("Invalid token");

            ApplicationValidation.When(string.IsNullOrWhiteSpace(cvv))
                .OrWhen(cvv != null && cvv.Length > 5)
                .OrWhen(cvv != null && Regex.Matches(cvv, @"[a-zA-Z]").Count > 0)
                .ThenThrows("Invalid cvv");

            try
            {
                Convert.ToDateTime(registrationDateTime);
            }
            catch (Exception)
            {
                throw new ApplicationException("Invalid date registration");
            }
        }

        private bool CheckRegistrationDateLimit(DateTime registrationDateTimeConverted)
        {
            var limitOfRegistration = DateTime.UtcNow.AddMinutes(-15);
            return DateTime.Compare(registrationDateTimeConverted, limitOfRegistration) == 1;
        }
    }
}
