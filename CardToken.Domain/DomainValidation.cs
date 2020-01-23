namespace CardToken.Domain
{
    public class DomainValidation
    {
        public bool Valid { get; private set; }

        public DomainValidation(bool valid)
        {
            Valid = valid;
        }

        public static DomainValidation When(bool condition)
        {
            if (condition)
                return new DomainValidation(false);

            return new DomainValidation(true);
        }

        public DomainValidation OrWhen(bool condition)
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