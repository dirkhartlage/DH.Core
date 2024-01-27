using System;

namespace DH.Core.Exceptions
{
    public class InitializationAbortedException : Exception
    {
        public InitializationAbortedException()
        {
        }

        public InitializationAbortedException(string message) : base(message)
        {
        }

        public InitializationAbortedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}