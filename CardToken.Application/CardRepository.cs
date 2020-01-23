using CardToken.Domain;
using System;

namespace CardToken.Application
{
    public interface CardRepository
    {
        void Add(Card card);
        Card GetCardByRegistrationDate(string registrationDateTime);
    }
}
