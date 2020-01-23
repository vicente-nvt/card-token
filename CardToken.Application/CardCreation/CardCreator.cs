using CardToken.Common;
using CardToken.Domain;

namespace CardToken.Application.CardCreation
{
    public class CardCreator : CardCreation
    {
        private CardRepository _cardRepository;

        public CardCreator(CardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public CardDTO CreateNewCard(string cardNumber, string cvv)
        {
            Card card;
            try
            {
                card = new Card(cardNumber, cvv);
            }
            catch (DomainException e)
            {
                throw new ApplicationException(e.Message);
            }

            _cardRepository.Add(card);

            return MapCardToDTO(card);
        }

        private static CardDTO MapCardToDTO(Card card)
        {
            return new CardDTO
            {
                Token = card.Token,
                RegistrationDate = card.RegistrationDateTime.ToStringWithMiliseconds()
            };
        }
    }
}
