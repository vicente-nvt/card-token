using CardToken.Domain;

namespace CardToken.Application
{
    public interface CardRepository
    {
        void Add(Card card);
    }
}
