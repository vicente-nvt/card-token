namespace CardToken.Application.CardCreation
{
    public interface CardCreation
    {
        CardDTO CreateNewCard(string cardNumber, string cvv);
    }
}
