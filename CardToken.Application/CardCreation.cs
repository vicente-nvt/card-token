namespace CardToken.Application
{
    public interface CardCreation
    {
        CardDTO CreateNewCard(string cardNumber, string cvv);
    }
}
