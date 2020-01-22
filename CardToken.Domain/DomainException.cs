using System;

namespace CardToken.Domain
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base (message) { }
    }
}