using System;

namespace MyDependencies.Sources.Exceptions
{
    public class NotRegistrationException : NullReferenceException
    {
        public NotRegistrationException()
        {
        }

        public NotRegistrationException(string message) : base(message)
        {
        }
    }
}