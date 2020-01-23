using System;

namespace CardToken.Application.CardValidation
{
    public interface CardValidation
    {
        ValidationDTO Validate(string token, string cvv, string registrationDate);
    }
}
