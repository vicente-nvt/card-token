using CardToken.Application;

namespace CardToken.Application
{
    public class ApplicationValidation
    {
        public bool Valid { get; private set; }

        public ApplicationValidation(bool valid)
        {
            Valid = valid;
        }

        public static ApplicationValidation When(bool condition)
        {
            if (condition)
                return new ApplicationValidation(false);

            return new ApplicationValidation(true);
        }

        public ApplicationValidation OrWhen(bool condition)
        {
            if (condition)
                Valid = false;

            return this;
        }

        public void ThenThrows(string message)
        {
            if (!Valid)
                throw new ApplicationException(message);
        }
    }
}