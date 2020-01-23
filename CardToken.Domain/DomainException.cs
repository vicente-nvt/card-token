using System;

namespace CardToken.Domain
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException(string message) : base (message) { }
    }
}