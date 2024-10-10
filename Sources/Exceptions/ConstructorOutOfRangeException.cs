using System;

namespace MyDependencies.Sources.Exceptions
{
    public class ConstructorOutOfRangeException : ArgumentOutOfRangeException
    {
        public ConstructorOutOfRangeException(string paramName) 
            : base(paramName)
        {
        }

        public ConstructorOutOfRangeException(string paramName, string message) 
            : base(paramName, message)
        {
        }

        public ConstructorOutOfRangeException()
        {
        }
    }
}