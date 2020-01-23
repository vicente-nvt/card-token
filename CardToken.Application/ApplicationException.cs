using System;

namespace CardToken.Application
{
    [Serializable]
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message) { }
    }
}