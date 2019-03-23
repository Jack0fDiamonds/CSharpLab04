using System;

namespace Lab03.Exceptions
{
    internal class UnbornPersonException : Exception
    {
        public UnbornPersonException()
        {
        }

        public UnbornPersonException(string message) : base(message)
        {
        }
    }
}
