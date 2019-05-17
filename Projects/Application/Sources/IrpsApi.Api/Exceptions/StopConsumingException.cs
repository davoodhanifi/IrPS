using System;

namespace IrpsApi.Api.Exceptions
{
    public class StopConsumingException : Exception
    {
        public StopConsumingException(string message) : base(message)
        {
        }
        public StopConsumingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}