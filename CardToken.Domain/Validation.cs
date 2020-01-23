namespace CardToken.Domain
{
    public class Validation
    {
        public bool Valid { get; private set; }

        public Validation(bool valid)
        {
            Valid = valid;
        }

        public static Validation When(bool condition)
        {
            if (condition)
                return new Validation(false);

            return new Validation(true);
        }

        public Validation OrWhen(bool condition)
        {
            if (condition)
                Valid = false;

            return this;
        }

        public void ThenThrows(string message)
        {
            if (!Valid)
                throw new DomainException(message);
        }
    }
}