using System;

namespace IrpsApi.Api.Exceptions
{
    public class InvalidMessageException : Exception
    {
        public InvalidMessageException(string message) : base(message)
        {
        }

        public InvalidMessageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}