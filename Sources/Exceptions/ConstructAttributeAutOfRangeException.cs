using System;

namespace MyDependencies.Sources.Exceptions
{
    public class ConstructAttributeAutOfRangeException : ArgumentOutOfRangeException
    {
        public ConstructAttributeAutOfRangeException(string paramName) 
            : base(paramName)
        {
        }

        public ConstructAttributeAutOfRangeException(string paramName, string message) 
            : base(paramName, message)
        {
        }

        public ConstructAttributeAutOfRangeException()
        {
        }
    }
}