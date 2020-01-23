using CardToken.Domain;

namespace CardToken.Application
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
            var card = new Card(cardNumber, cvv);

            _cardRepository.Add(card);

            return MapCardToDTO(card);
        }

        private static CardDTO MapCardToDTO(Card card)
        {
            return new CardDTO
            {
                Token = card.Token,
                RegistrationDate = card.RegistrationDate
            };
        }
    }
}
