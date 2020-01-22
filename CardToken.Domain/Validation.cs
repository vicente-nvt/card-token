using CardToken.Domain;

namespace CardToken.Domain
{
    public class Validation
    {
        public bool Valid { get; private set; }

        public Validation(bool valid)
        {
            this.Valid = valid;
        }

        public static Validation When(bool condition)
        {
            if (condition)
                return new Validation(false);

            return new Validation(true);
        }

        public void ThenThrows(string message)
        {
            if (!Valid)
                throw new DomainException(message);
        }
    }
}