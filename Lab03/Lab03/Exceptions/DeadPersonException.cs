using System;

namespace Lab03.Exceptions
{
    internal class DeadPersonException : Exception
    {
        public DeadPersonException()
        {
        }

        public DeadPersonException(string message) : base(message)
        {
        }
    }
}
