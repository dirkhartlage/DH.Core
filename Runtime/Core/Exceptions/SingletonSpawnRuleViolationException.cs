using System;

namespace DH.Core.Exceptions
{
    public sealed class SingletonSpawnRuleViolationException : Exception
    {
        public SingletonSpawnRuleViolationException()
        {
        }

        public SingletonSpawnRuleViolationException(string message) : base(message)
        {
        }

        public SingletonSpawnRuleViolationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}