using CardToken.Application;
using CardToken.Domain;
using System.Collections.Generic;

namespace CardToken.Infra
{
    public class CardMemoryRepository : CardRepository
    {
        public Dictionary<string, Card> CardSet { get; }

        public CardMemoryRepository()
        {
            CardSet = new Dictionary<string, Card>();
        }
        
        public void Add(Card card)
        {
            CardSet.Add(card.Token, card);
        }
    }
}
